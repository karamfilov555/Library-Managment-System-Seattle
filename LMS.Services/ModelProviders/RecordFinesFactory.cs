using LMS.Models;
using LMS.Services.ModelProviders.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace LMS.Services.ModelProviders
{
    public class RecordFinesFactory : IRecordFinesFactory
    {
        public RecordFinesFactory()
        {

        }
        public RecordFines CreateRecordFines()
        {
            var record = new RecordFines(0.00M);
            return record;
        }
    }
}
