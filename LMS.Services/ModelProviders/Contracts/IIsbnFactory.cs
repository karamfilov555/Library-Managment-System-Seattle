using LMS.Models.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace LMS.Services.ModelProviders.Contracts
{
    public interface IIsbnFactory
    {
        Isbn CreateIsbn();
    }
}
