using System;
using System.Collections.Generic;
using System.Text;

namespace LMS.Contracts
{
    public interface IHistoryDataBase
    {
        void CheckBooksOfCurrentUser();
        void LoadHistoryFromJson();
    }
}
