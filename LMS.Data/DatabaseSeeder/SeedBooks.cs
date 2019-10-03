using LMS.Data.JsonManager;
using LMS.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LMS.Data.DatabaseSeeder
{
    public static class SeedBooks
    {
        private const string booksDirectory = @"..\LMS.Data\JsonRaw\Books.json";
        private const string authorsDirectory = @"..\LMS.Data\JsonRaw\Authors.json";
        private const string subjectsDirectory = @"..\LMS.Data\JsonRaw\SubjectCategories.json";
        public static void SeedDatabaseBooks(this IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var _context = serviceScope.ServiceProvider.GetService<LMSContext>();
                var _jsonManager = serviceScope.ServiceProvider.GetService<IJsonManager>();
                var count = _context.Books.Count();
                if (count == 0)
                {
                    _context.Database.Migrate();

                    var authors = _jsonManager.ExtractTypesFromJson<Author>(authorsDirectory);
                    var subjects = _jsonManager.ExtractTypesFromJson<SubjectCategory>(subjectsDirectory);
                    var books = _jsonManager.ExtractTypesFromJson<Book>(booksDirectory);
                    //var books = _jsonManager.ExtractTypesFromJson<Book>(booksDirectory);
                    _context.Authors.AddRange(authors);
                    _context.SubjectCategories.AddRange(subjects);
                    _context.Books.AddRange(books);
                    _context.SaveChanges();
                }
            }
        }

    }
}
