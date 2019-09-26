using LMS.Data;
using LMS.Models;
using LMS.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Services
{
    public class HistoryService : IHistoryService
    {
        private readonly IUserService _userService;
        private readonly LMSContext _context;

        public HistoryService(IUserService userService, LMSContext context)
        {
            _userService = userService;
            _context = context;
        }
        public async Task<HistoryRegistry> CheckoutBook(string bookId , string userId)
        {
            //var user = await _userService.GetCurrentUserAsync();
            var historyRegistry = new HistoryRegistry()
            {
                BookId = bookId,
                UserId = userId,
                CheckOutDate = DateTime.Now.ToShortDateString(),
                ReturnDate = DateTime.Now.AddDays(10)
            };
            _context.HistoryRegistries.Add(historyRegistry);
            await _context.SaveChangesAsync();
            return historyRegistry;
        }
    }
}
