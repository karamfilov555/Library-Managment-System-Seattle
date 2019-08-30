using LMS.Data;
using LMS.Models;
using LMS.Services.Contracts;
using LMS.Services.ModelProviders.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LMS.Services
{
    public class RecordFinesServices : IRecordFinesServices
    {
        private readonly IRecordFinesFactory _recordFactory;
        private readonly LMSContext _context;
        public RecordFinesServices(IRecordFinesFactory recordFactory,
                                   LMSContext context)
        {
            _recordFactory = recordFactory;
            _context = context;
        }
        public RecordFines ProvideRecord()
        {
            return _recordFactory.CreateRecordFines();
        }
        public bool CheckRecordFines()
        {
            if (_context.RecordFines.Count() != 0)
                return true;
            return false;
        }
        public void AddFineToUser(User user,int days)
        {
            var fines = _context.RecordFines
                 .Where(u => u.Id == user.RecordFinesId);
            foreach (var fine in fines)
                fine.FineAmount += 5.00M * days;
            _context.SaveChanges();
        }
        public string GetUserTotalFineAmount(User user)
        {
            var total = _context.Users.Where(u => u.RecordFinesId == user.RecordFinesId)
                .Select(u => u.RecordFines.FineAmount);

            return $"Your fine balance: {total.First()}$!";
        }
    }
}
