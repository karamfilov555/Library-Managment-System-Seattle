using LMS.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace LMS.Services.Contracts
{
    public interface IHistoryServices
    {
        void AddHistoryToDb(HistoryRegistry history);
    }
}
