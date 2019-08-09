using LMS.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace LMS.Services
{
    public class DataBaseLoader : IDataBaseLoader
    {
        private readonly IBooksDataBase _booksDataBase;
        private readonly IAdminsDataBase _adminsDataBase;
        private readonly IUsersDataBase _usersDataBase;
        private readonly IHistoryDataBase _historyDataBase;
        public DataBaseLoader(IBooksDataBase booksDataBase,
                              IAdminsDataBase adminsDataBase,
                              IUsersDataBase usersDataBase,
                              IHistoryDataBase historyDataBase)
        {
            _booksDataBase = booksDataBase;
            _usersDataBase = usersDataBase;
            _adminsDataBase = adminsDataBase;
            _historyDataBase = historyDataBase;
        }
        public void FillDataBase()
        {
            _booksDataBase.LoadBooksFromJson();
            _adminsDataBase.LoadAdminsFromJson();
            _usersDataBase.LoadUsersFromJson();
            _historyDataBase.LoadHistoryFromJson();
        }
    }
}
