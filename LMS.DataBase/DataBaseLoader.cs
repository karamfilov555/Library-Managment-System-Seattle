using LMS.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace LMS.Services
{
    public class DataBaseLoader : IDataBaseLoader
    {
        private readonly IBookServices _bookServices;
        private readonly IAdminServices _adminServices;
        private readonly IUsersServices _usersServices;
        private readonly IHistoryServices _historyServices;
        public DataBaseLoader(IBookServices bookServices,
                              IAdminServices adminServices,
                              IUsersServices usersServices,
                              IHistoryServices historyServices)
        {
            _bookServices = bookServices;
            _usersServices = usersServices;
            _adminServices = adminServices;
            _historyServices = historyServices;
        }
        public void FillDataBase()
        {
            _bookServices.LoadBooksFromJson();
            _adminServices.LoadAdminsFromJson();
            _usersServices.LoadUsersFromJson();
            _historyServices.LoadHistoryFromJson();
        }
    }
}
