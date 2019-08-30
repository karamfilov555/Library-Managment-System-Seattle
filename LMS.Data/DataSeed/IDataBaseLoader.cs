using System;
using System.Collections.Generic;
using System.Text;

namespace LMS.Data.DataSeed
{
    public interface IDataBaseLoader
    {
        void SeedDataBase();
        void LoadUsers();
    }
}
