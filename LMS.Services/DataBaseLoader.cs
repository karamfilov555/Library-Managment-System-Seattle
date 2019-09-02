using LMS.Data;
using LMS.Models;
using LMS.Models.Models;
using LMS.Services.Contracts;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.IO;
using System.Text;

namespace LMS.Services
{
    public class DataBaseLoader : IDataBaseLoader
    {
        private readonly ISqlCommandExecutor _commandExecutor;
        private readonly IUserServices _userServices;
        private readonly IRoleServices _roleServices;
        private readonly IIsbnServices _isbnServices;
        private readonly IAuthorServices _authorServices;
        private readonly IBookServices _bookServices;
        private readonly ISubjectServices _subjectServices;
        private readonly IJsonServices _jsonServices;
        private readonly IRecordFinesServices _recordServices;
        private const string usersDirectory = @"..\..\..\..\LMS.Data\Jsons\Users.json";
        private const string rolesDirectory = @"..\..\..\..\LMS.Data\Jsons\Roles.json";
        private const string finesDirectory = @"..\..\..\..\LMS.Data\Jsons\RecordFines.json";
        private const string authorsDirectory = @"..\..\..\..\LMS.Data\Jsons\Authors.json";
        private const string isbnsDirectory = @"..\..\..\..\LMS.Data\Jsons\Isbns.json";
        private const string booksDirectory = @"..\..\..\..\LMS.Data\Jsons\Books.json";
        private const string subjectCategoriesDirectory = @"..\..\..\..\LMS.Data\Jsons\SubjectCategories.json";
        
        public DataBaseLoader(IUserServices userServices,
                              IRoleServices roleServices,
                              IJsonServices jsonServices,
                              IRecordFinesServices recordServices,
                              ISqlCommandExecutor commandExecutor,
                              IAuthorServices authorServices,
                              IIsbnServices isbnServices,
                              IBookServices bookServices,
                              ISubjectServices subjectServices)
        {
            _commandExecutor = commandExecutor;
            _userServices = userServices;
            _authorServices = authorServices;
            _roleServices = roleServices;
            _jsonServices = jsonServices;
            _recordServices = recordServices;
            _isbnServices = isbnServices;
            _bookServices = bookServices;
            _subjectServices = subjectServices;
        }
        public void SeedDataBase()
        {
            LoadRoles();
            LoadRecordFines();
            LoadUsers();
            LoadAuthors();
            LoadIsbns();
            LoadBooks();
            LoadSubjectCategories();
        }
        public void LoadUsers()
        {
            var users = _jsonServices.ExtractTypesFromJson<User>(usersDirectory);
            var sqlCommand = new StringBuilder();

            foreach (var user in users)
            {
                if (!_userServices.CheckIfUserExist(user.Username))
                {
                    sqlCommand.AppendLine($@"
                    
                    SELECT *  FROM dbo.Users u
                                WHERE u.Id = {user.Id}
                        BEGIN
                        INSERT INTO dbo.Users
                             (Username, Password, RoleId, RecordFinesId) 
                        VALUES ('{user.Username}', '{user.Password}','{user.RoleId}','{user.RecordFinesId}')
                    END");
                }
            }
            _commandExecutor.ExecuteCommand(sqlCommand);
        }
        public void LoadRoles()
        {
            var roles = _jsonServices.ExtractTypesFromJson<Role>(rolesDirectory);
            var sqlCommand = new StringBuilder();

            foreach (var role in roles)
            {
                if (!_roleServices.CheckIfRoleExist(role.Name))
                {
                    sqlCommand.AppendLine($@"
                    
                      SELECT * FROM dbo.Roles r
                             WHERE r.Id = {role.Id}
                        BEGIN
                        INSERT INTO dbo.Roles
                             (Name) 
                        VALUES ('{role.Name}')
                      END ");
                }
            }
            _commandExecutor.ExecuteCommand(sqlCommand);
        }
        public void LoadRecordFines()
        {
            var records = _jsonServices.ExtractTypesFromJson<RecordFines>(finesDirectory);
            var sqlCommand = new StringBuilder();

            foreach (var record in records)
            {
                if (!_recordServices.CheckRecordFines())
                {
                    sqlCommand.AppendLine($@"
                    
                      SELECT * FROM dbo.RecordFines r
                             WHERE r.Id = {record.Id}
                        BEGIN
                        INSERT INTO dbo.RecordFines
                             (FineAmount) 
                        VALUES ('{record.FineAmount}')
                      END ");
                }
            }
            _commandExecutor.ExecuteCommand(sqlCommand);
        }
        public void LoadAuthors()
        {
            var authors = _jsonServices.ExtractTypesFromJson<Author>(authorsDirectory);
            var sqlCommand = new StringBuilder();

            foreach (var author in authors)
            {
                if (!_authorServices.CheckIfAuthorExist(author.Name))
                {
                    sqlCommand.AppendLine($@"
                    
                    SELECT *  FROM dbo.Authors u
                                WHERE u.Id = {author.Id}
                        BEGIN
                        INSERT INTO dbo.Authors
                             (Name) 
                        VALUES ('{author.Name}')
                    END");
                }
            }
            _commandExecutor.ExecuteCommand(sqlCommand);
        }
        public void LoadIsbns()
        {
            var isbns = _jsonServices.ExtractTypesFromJson<Isbn>(isbnsDirectory);
            var sqlCommand = new StringBuilder();

            foreach (var isbn in isbns)
            {
                if (!_isbnServices.CheckIfIsbnExist(isbn.ISBN))
                {
                    sqlCommand.AppendLine($@"
                    
                    SELECT *  FROM dbo.Isbns u
                                WHERE u.Id = {isbn.Id}
                        BEGIN
                        INSERT INTO dbo.Isbns
                             (ISBN) 
                        VALUES ('{isbn.ISBN}')
                    END");
                }
            }
            _commandExecutor.ExecuteCommand(sqlCommand);
        }
        public void LoadBooks()
        {
            var books = _jsonServices.ExtractTypesFromJson<Book>(booksDirectory);
            var sqlCommand = new StringBuilder();

            foreach (var book in books)
            {
                if (!_bookServices.CheckIfBookExist(book.Title))
                {
                    sqlCommand.AppendLine($@"
                    
                    SELECT *  FROM dbo.Books u
                                WHERE u.Id = {book.Id}
                        BEGIN
                        INSERT INTO dbo.Books
                             (Title, AuthorId, Pages, Year, Country, Language, IsbnId, IsReserved, IsCheckedOut) 
                        VALUES ('{book.Title}','{book.AuthorId}','{book.Pages}','{book.Year}','{book.Country}',
                                '{book.Language}','{book.IsbnId}','{book.IsReserved}','{book.IsCheckedOut}')
                    END");
                }
            }
            _commandExecutor.ExecuteCommand(sqlCommand);
        }
        public void LoadSubjectCategories()
        {
            var subjects = _jsonServices.ExtractTypesFromJson<SubjectCategory>(subjectCategoriesDirectory);
            var sqlCommand = new StringBuilder();

            foreach (var subj in subjects)
            {
                if (!_subjectServices.CheckIfSubjectExist(subj.SubjectName))
                {
                    sqlCommand.AppendLine($@"
                    
                    SELECT *  FROM dbo.SubjectCategories u
                                WHERE u.Id = {subj.Id}
                        BEGIN
                        INSERT INTO dbo.SubjectCategories
                             (SubjectName) 
                        VALUES ('{subj.SubjectName}')
                    END");
                }
            }
            _commandExecutor.ExecuteCommand(sqlCommand);
        }
        
    }
}
