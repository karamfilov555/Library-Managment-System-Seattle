using LMS.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace LMS.Contracts.DataBaseContracts
{
    //Open/Close example 
    public interface IAdminDataBase
    {
        IList<User> ReadAdmins();
        void WriteAdmins(string jsonToOutput);
        void RemoveAdminFromJsonDb(string userName);
    }
}
