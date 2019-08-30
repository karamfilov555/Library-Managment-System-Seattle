using LMS.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace LMS.Services.Contracts
{
    public interface IRecordFinesServices
    {
        RecordFines ProvideRecord();
        bool CheckRecordFines();
        void AddFineToUser(User user);
    }
}
