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
    }
}
