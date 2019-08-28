using System;
using System.Collections.Generic;
using System.Text;

namespace LMS.Services.Contracts
{
    public interface IDataBaseLoader
    {
        void SeedDataBase();
        void LoadUsers();
    }
}
