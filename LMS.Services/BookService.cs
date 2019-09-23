using LMS.Data;
using LMS.DTOs;
using LMS.Models;
using LMS.Services.Contracts;
using Microsoft.EntityFrameworkCore;
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
               .Include(b=>b.SubjectCategory)
               .FirstOrDefaultAsync(m => m.Id == id);
            return book;
        }
    }
}
