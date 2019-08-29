using LMS.Models.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace LMS.Services.Contracts
{
    public interface IIsbnServices
    {
        Isbn ProvideIsbn();
        bool CheckIfIsbnExist(string isbn);
    }
}
