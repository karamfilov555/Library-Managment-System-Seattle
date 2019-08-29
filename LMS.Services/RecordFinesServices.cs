using LMS.Models;
using LMS.Services.Contracts;
using LMS.Services.ModelProviders.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace LMS.Services
{
    public class RecordFinesServices : IRecordFinesServices
    {
        private readonly IRecordFinesFactory _recordFactory;
        public RecordFinesServices(IRecordFinesFactory recordFactory)
        {
            _recordFactory = recordFactory;
        }
        public RecordFines ProvideRecord()
        {
            return _recordFactory.CreateRecordFines();
        }
    }
}
