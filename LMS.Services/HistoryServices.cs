using LMS.Data;
using LMS.Models;
using LMS.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LMS.Services
{
    public class HistoryServices : IHistoryServices
    {
        private readonly LMSContext _context;
        private readonly ILoginAuthenticator _loginAuthenticator;
        private readonly IRecordFinesServices _fineServices;
        private readonly IBookServices _bookServices;

        public HistoryServices(LMSContext context,
                               ILoginAuthenticator loginAuthenticator,
                               IRecordFinesServices fineServices,
                               IBookServices bookServices)
        {
            _context = context;
            _loginAuthenticator = loginAuthenticator;
            _fineServices = fineServices;
            _bookServices = bookServices;
        }
        public void AddHistoryToDb(HistoryRegistry history)
        {
            if (_context.HistoryRegistries.Any(hr => hr.UserId == history.User.Id && hr.BookId == history.Book.Id))
            {
                var historyRegistry = _context.HistoryRegistries.First(hr => hr.UserId == history.User.Id && hr.BookId == history.Book.Id);
                historyRegistry.IsReturned = false;
                _context.SaveChanges();
            }
            else
            {
                _context.HistoryRegistries.Add(history);
                _context.SaveChanges();
            }
        }
        public void ReturnBook(string title)
        {
            var user = _loginAuthenticator.LoggedUser();
            var book = FindBookInUserHistory(user, title);
            CheckReturnDate(user, book);
            SetReturnStatus(user, book);
            _bookServices.SetReturnBookStatus(book);
            _context.SaveChanges();
        }
        public Book FindBookInUserHistory(User user, string title)
        {
            var historyRegistry = _context.HistoryRegistries
                .FirstOrDefault
                (hr => hr.UserId == user.Id && hr.Book.Title == title);
            var book = _context.Books.FirstOrDefault
                (b => b.Id == historyRegistry.BookId);
            CheckIfBookExistInCurrentUserHistory(book, user);
            return book;
        }
        public void CheckIfBookExistInCurrentUserHistory(Book book, User user)
        {
            if (!_context.HistoryRegistries.Any(hr => hr.UserId == user.Id && hr.BookId == book.Id && hr.IsReturned == false))
                throw new ArgumentException
                    ($"There is no book with title \"{book.Title}\" in your account!");
        }
        public void SetReturnStatus(User user, Book book)
        {
            var registries = _context.HistoryRegistries
                .Where(u => u.UserId == user.Id && u.BookId == book.Id);

            foreach (var reg in registries)
                reg.IsReturned = true;

            _context.SaveChanges();
        }
        public void CheckForMaxCheckOutAmount()
        {
            var user = _loginAuthenticator.LoggedUser();
            var allNotReturnedBooksOfCurrentUser = _context.HistoryRegistries.Where(hr => hr.UserId == user.Id && hr.IsReturned == false);
            if (allNotReturnedBooksOfCurrentUser.Count() >= 5)
                throw new ArgumentException("You have reached the maximum check-out amount of 5 books!");

        }
        public void CheckReturnDate(User user, Book book)
        {
            if (_context.HistoryRegistries
                .Any
                (u => u.UserId == user.Id && u.BookId == book.Id && u.ReturnDate < DateTime.Now))
            {
                var dateNow = DateTime.Now;
                int daysDelay;
                var date = _context.HistoryRegistries
                    .Where(u => u.UserId == user.Id && u.BookId == book.Id)
                    .Select(u => u.ReturnDate);

                foreach (var item in date)
                {
                    daysDelay = dateNow.Subtract(item).Days;
                    _fineServices.AddFineToUser(user, daysDelay);
                }
            }
        }
        public string AllUserBooksToString()
        {
            var allHistory = _context.HistoryRegistries.ToList();
            var user = _loginAuthenticator.LoggedUser();
            var strBulider = new StringBuilder();
            strBulider.AppendLine($"========{user.Username},this is your list of checked-out books========{Environment.NewLine}");
            int counter = 1;
            foreach (var history in allHistory)
            {
                if (history.UserId != user.Id || history.IsReturned == true)
                    continue;
                strBulider.AppendLine($"-------Book #{counter}------{Environment.NewLine}Title : {_context.Books.First(i => i.Id == history.BookId).Title}{Environment.NewLine}Author : {_context.Authors.First(a => a.Id == history.Book.AuthorId).Name}{Environment.NewLine}Return Date : {history.ReturnDate}");

            }
            return strBulider.ToString();
        }
    }
}
