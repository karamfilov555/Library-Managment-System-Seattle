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
    }
}
