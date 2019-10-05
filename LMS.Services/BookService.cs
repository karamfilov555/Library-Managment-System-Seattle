using LMS.Data;
using LMS.DTOs;
using LMS.Models;
using LMS.Services.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace LMS.Services
{
    public class BookService : IBookService
    {
        private readonly LMSContext _context;
        private readonly IAuthorService _authorService;
        private readonly ISubjectCategoryService _subjectService;

        public BookService(LMSContext context,
                           IAuthorService authorService,
                           ISubjectCategoryService subject)
        {
            _context = context;
            _authorService = authorService;
            _subjectService = subject;
        }

        private async Task<Book> AddBook(Book book)
        {
            await _context.Books.AddAsync(book);
            await _context.SaveChangesAsync();
            return book;
        }
        public async Task<Book> ProvideBookAsync(BookDTO bookDto)
        {
            var book = new Book();
            var author = await _authorService.ProvideAuthorAsync(bookDto.AuthorName);
            var subject = await _subjectService.ProvideSubjectAsync(bookDto.SubjectCategoryName);
            for (int i = 0; i < bookDto.Copies; i++)
            {
                book = new Book
                {
                    Title = bookDto.Title,
                    Author = author,
                    SubjectCategory = subject,
                    Year = bookDto.Year,
                    Pages = bookDto.Pages,
                    Country = bookDto.Country,
                    CoverImageUrl = bookDto.CoverImageUrl,
                    Language = bookDto.Language,
                    Copies = bookDto.Copies,
                };
                await AddBook(book);
            }
            return book;
        }
        public async Task<Book> FindByIdAsync(string id)
        {
            var book = await _context.Books
               .Include(b => b.Author)
               .Include(b => b.BookRating)
               .ThenInclude(br => br.Reviews)
               .Include(b => b.SubjectCategory)
               .FirstOrDefaultAsync(m => m.Id == id);
            return book;
        }
        public async Task LockBook(string Id)
        {
            var sameBooks = await GetAllSameBooks(Id);

            foreach (var item in sameBooks)
            {
                item.IsLocked = true;
            }

            await _context.SaveChangesAsync();
        }
        public async Task UnlockBook(string Id)
        {
            var sameBooks = await GetAllSameBooks(Id);

            foreach (var item in sameBooks)
            {
                item.IsLocked = false;
            }

            await _context.SaveChangesAsync();
        }
        public async Task<Book> FindFreeBookByIdAsync(string id)
        {
            var book = await _context.Books
               .Include(b => b.Author)
               .Include(b => b.BookRating)
               .ThenInclude(br => br.Reviews)
               .Include(b => b.SubjectCategory)
               .Where(b => b.IsCheckedOut == false)
               .FirstOrDefaultAsync(m => m.Id == id);
            return book;
        }
        private async Task<ICollection<Book>> GetAllBooksAsync()
            => await _context.Books
              .Include(b => b.Author)
              .Include(b => b.BookRating)
              .ThenInclude(br => br.Reviews)
              .Include(b => b.SubjectCategory)
              .ToListAsync();
        private async Task<ICollection<Book>> GetAllFreeBooksAsync()
            => await _context.Books
              .Include(b => b.Author)
              .Include(b => b.BookRating)
                .ThenInclude(br => br.Reviews)
              .Include(b => b.SubjectCategory)
              .Where(b => b.IsCheckedOut == false)
              .ToListAsync();
        private async Task<ICollection<Book>> GetAllUnavailableBooksAsync()
            => await _context.Books
              .Include(b => b.Author)
              .Include(b => b.BookRating)
              .ThenInclude(br => br.Reviews)
              .Include(b => b.SubjectCategory)
              .Where(b => b.Copies == 0)
              .ToListAsync();
        public async Task<ICollection<Book>> GetAllBooksWithoutRepetitionsAsync()
        {
            var allBooksFreeBooks = await GetAllFreeBooksAsync();
            var uniqueBooks = new List<Book>();
            foreach (var item in allBooksFreeBooks)
            {
                if (!uniqueBooks.Any(b => b.Title.Equals(item.Title) && b.Author.Equals(item.Author) && b.Language.Equals(item.Language) && b.IsCheckedOut == false))
                    uniqueBooks.Add(item);
            }
            return uniqueBooks;
        }
        public async Task<ICollection<Book>> GetAllBooksForAdminWithoutRepetitionsAsync()
        {
            var allBooksBooks = await GetAllBooksAsync();
            var uniqueBooks = new List<Book>();
            foreach (var item in allBooksBooks)
            {
                if (!uniqueBooks.Any(b => b.Title.Equals(item.Title) && b.Author.Equals(item.Author) && b.Language.Equals(item.Language)))
                    uniqueBooks.Add(item);
            }
            return uniqueBooks;
        }
        public async Task<string> GetBookTitleAsync(string bookId)
        {
            var book = await FindByIdAsync(bookId);
            return book.Title;
        }
        public async Task<ICollection<Book>> GetAllSameBooks(string Id)
        {
            var book = await FindByIdAsync(Id);
            return await _context.Books.Where(b => b.Title == book.Title && b.Author == book.Author && b.Language == book.Language).ToListAsync();
        }

        public async Task<ICollection<Book>> GetUnavailableBooksWithoutRepetitions()
        {
            var allUnavailableBooks = await GetAllUnavailableBooksAsync();
            var uniqueBooksUnavailable = new List<Book>();
            foreach (var item in allUnavailableBooks)
            {
                if (!uniqueBooksUnavailable.Any(b => b.Title.Equals(item.Title) && b.Author.Equals(item.Author) && b.Language.Equals(item.Language)))
                    uniqueBooksUnavailable.Add(item);
            }
            return uniqueBooksUnavailable;
        }
        //da vrushta kolekciq ot vsi4ki knigi na HPage!!!
        public async Task<IList<Book>> GetBooksForHomePage()
        {
            // list .... posle
            var books = await _context.Books
               .Include(b => b.Author)
              .Include(b => b.BookRating)
              .ThenInclude(br => br.Reviews)
              .Include(b => b.SubjectCategory)
              .Where(b => b.Id == "2" || b.Id == "5" || b.Id == "3" || b.Id == "4" || b.Id == "6" ||
              b.Id == "9" || b.Id == "7" || b.Id == "11").ToListAsync();
            return books;
        }
        public async Task DeleteBook(string bookId)
        {
            var book = await _context.Books.FindAsync(bookId);
            _context.Books.Remove(book);
            var sameBooksLikeDeleted = _context.Books.Where(b => b.Title == book.Title && b.Language == book.Language && b.Year == book.Year && b.Copies > 0);
            await sameBooksLikeDeleted.ForEachAsync(b => b.Copies--);
            await _context.SaveChangesAsync();
        }
        public async Task<ICollection<Book>> GetFilteredResults(string title, string author, string subject, int year, bool inclusive)
        {
            var results = new List<Book>();
            //ako edin parametur e null
            if (subject == null && title != null && author != null && year != 0)
            {
                if (inclusive == true)
                {
                    results = await _context.Books.Include(b => b.Author).Include(b => b.SubjectCategory).Where(b => b.Title.ToLower().Contains(title.ToLower()) && b.Author.Name.ToLower().Contains(author.ToLower()) && b.Year == year).ToListAsync();
                }
                else
                {
                    results = await _context.Books.Include(b => b.Author).Include(b => b.SubjectCategory).Where(b => b.Title.ToLower().Contains(title.ToLower()) || b.Author.Name.ToLower().Contains(author.ToLower()) || b.Year == year).ToListAsync();
                }
            }
            else if (subject != null && title == null && author != null && year != 0)
            {
                if (inclusive == true)
                {
                    results = await _context.Books.Include(b => b.Author).Include(b => b.SubjectCategory).Where(b => b.SubjectCategory.Name.ToLower().Contains(subject.ToLower()) && b.Author.Name.ToLower().Contains(author.ToLower()) && b.Year == year).ToListAsync();
                }
                else
                {
                    results = await _context.Books.Include(b => b.Author).Include(b => b.SubjectCategory).Where(b => b.SubjectCategory.Name.ToLower().Contains(subject.ToLower()) || b.Author.Name.ToLower().Contains(author.ToLower()) || b.Year == year).ToListAsync();
                }
            }
            else if (subject != null && title != null && author == null && year != 0)
            {
                if (inclusive == true)
                {
                    results = await _context.Books.Include(b => b.Author).Include(b => b.SubjectCategory).Where(b => b.Title.ToLower().Contains(title.ToLower()) && b.SubjectCategory.Name.ToLower().Contains(subject.ToLower()) && b.Year == year).ToListAsync();
                }
                else
                {
                    results = await _context.Books.Include(b => b.Author).Include(b => b.SubjectCategory).Where(b => b.Title.ToLower().Contains(title.ToLower()) || b.SubjectCategory.Name.ToLower().Contains(subject.ToLower()) || b.Year == year).ToListAsync();
                }
            }
            else if (subject != null && title != null && author != null && year == 0)
            {
                if (inclusive == true)
                {
                    results = await _context.Books.Include(b => b.Author).Include(b => b.SubjectCategory).Where(b => b.Title.ToLower().Contains(title.ToLower()) && b.Author.Name.ToLower().Contains(author.ToLower()) && b.SubjectCategory.Name.ToLower().Contains(subject.ToLower())).ToListAsync();
                }
                else
                {
                    results = await _context.Books.Include(b => b.Author).Include(b => b.SubjectCategory).Where(b => b.Title.ToLower().Contains(title.ToLower()) || b.Author.Name.ToLower().Contains(author.ToLower()) || b.SubjectCategory.Name.ToLower().Contains(subject.ToLower())).ToListAsync();
                }
            }
            //ako 2 parametura sa null
            else if (subject == null && title == null && author != null && year != 0)
            {
                if (inclusive == true)
                {
                    results = await _context.Books.Include(b => b.Author).Include(b => b.SubjectCategory).Where(b => b.Author.Name.ToLower().Contains(author.ToLower()) && b.Year == year).ToListAsync();
                }
                else
                {
                    results = await _context.Books.Include(b => b.Author).Include(b => b.SubjectCategory).Where(b => b.Author.Name.ToLower().Contains(author.ToLower()) || b.Year == year).ToListAsync();
                }
            }
            else if (subject == null && title != null && author == null && year != 0)
            {
                if (inclusive == true)
                {
                    results = await _context.Books.Include(b => b.Author).Include(b => b.SubjectCategory).Where(b => b.Title.ToLower().Contains(title.ToLower()) && b.Year == year).ToListAsync();
                }
                else
                {
                    results = await _context.Books.Include(b => b.Author).Include(b => b.SubjectCategory).Where(b => b.Title.ToLower().Contains(title.ToLower()) || b.Year == year).ToListAsync();
                }
            }
            else if (subject == null && title != null && author != null && year == 0)
            {
                if (inclusive == true)
                {
                    results = await _context.Books.Include(b => b.Author).Include(b => b.SubjectCategory).Where(b => b.Title.ToLower().Contains(title.ToLower()) && b.Author.Name.ToLower().Contains(author.ToLower())).ToListAsync();
                }
                else
                {
                    results = await _context.Books.Include(b => b.Author).Include(b => b.SubjectCategory).Where(b => b.Title.ToLower().Contains(title.ToLower()) || b.Author.Name.ToLower().Contains(author.ToLower())).ToListAsync();
                }
            }
            else if (subject != null && title == null && author == null && year != 0)
            {
                if (inclusive == true)
                {
                    results = await _context.Books.Include(b => b.Author).Include(b => b.SubjectCategory).Where(b => b.SubjectCategory.Name.ToLower().Contains(subject.ToLower()) && b.Year == year).ToListAsync();
                }
                else
                {
                    results = await _context.Books.Include(b => b.Author).Include(b => b.SubjectCategory).Where(b => b.SubjectCategory.Name.ToLower().Contains(subject.ToLower()) || b.Year == year).ToListAsync();
                }
            }
            else if (subject != null && title == null && author != null && year == 0)
            {
                if (inclusive == true)
                {
                    results = await _context.Books.Include(b => b.Author).Include(b => b.SubjectCategory).Where(b => b.Author.Name.ToLower().Contains(author.ToLower()) && b.SubjectCategory.Name.ToLower().Contains(subject.ToLower())).ToListAsync();
                }
                else
                {
                    results = await _context.Books.Include(b => b.Author).Include(b => b.SubjectCategory).Where(b => b.Author.Name.ToLower().Contains(author.ToLower()) || b.SubjectCategory.Name.ToLower().Contains(subject.ToLower())).ToListAsync();
                }
            }
            else if (subject != null && title != null && author == null && year == 0)
            {
                if (inclusive == true)
                {
                    results = await _context.Books.Include(b => b.Author).Include(b => b.SubjectCategory).Where(b => b.SubjectCategory.Name.ToLower().Contains(subject.ToLower()) && b.Title.ToLower().Contains(title.ToLower())).ToListAsync();
                }
                else
                {
                    results = await _context.Books.Include(b => b.Author).Include(b => b.SubjectCategory).Where(b => b.SubjectCategory.Name.ToLower().Contains(subject.ToLower()) || b.Title.ToLower().Contains(title.ToLower())).ToListAsync();
                }
            }
            //----------------//-----------------//----------------------//-----------------//
            else if (subject != null && title == null && author == null && year == 0)
            {
                results = await _context.Books.Include(b => b.Author).Include(b => b.SubjectCategory).Where(b => b.SubjectCategory.Name.ToLower().Contains(subject.ToLower())).ToListAsync();
            }
            else if (subject == null && title != null && author == null && year == 0)
            {
                results = await _context.Books.Include(b => b.Author).Include(b => b.SubjectCategory)
                    .Where(b => b.Title.ToLower()
                    .Contains(title.ToLower()))
                    .ToListAsync();
            }
            else if (subject == null && title == null && author != null && year == 0)
            {
                results = await _context.Books.Include(b => b.Author).Include(b => b.SubjectCategory)
                    .Where(b => b.Author.Name.ToLower()
                    .Contains(author.ToLower()))
                    .ToListAsync();
            }
            else if (subject == null && title == null && author == null && year != 0)
            {
                results = await _context.Books.Include(b => b.Author).Include(b => b.SubjectCategory)
                    .Where(b => b.Year == year)
                    .ToListAsync();
            }


            return results;
        }
    }
}
