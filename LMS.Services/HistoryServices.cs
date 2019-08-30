using LMS.Data;
using LMS.Models;
using LMS.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace LMS.Services
{
    public class HistoryServices : IHistoryServices
    {
        private readonly LMSContext _context;

        public HistoryServices(LMSContext context)
        {
            _context = context;
        }
        public void AddHistoryToDb(HistoryRegistry history)
        {
            _context.HistoryRegistries.Add(history);
            _context.SaveChanges();
        }

    }
}
