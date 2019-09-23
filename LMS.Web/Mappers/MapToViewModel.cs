using LMS.Models;
using LMS.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LMS.Web.Mappers
{
    public static class MapToViewModel
    {
        public static UserViewModel MapToUserViewModel(this User user)
        {
            var viewModel = new UserViewModel();
            viewModel.Id = user.Id;
            viewModel.Username = user.UserName;
            return viewModel;
        }
        public static BookViewModel MapToBookViewModel(this Book book)
        {
            var viewModel = new BookViewModel();
            viewModel.Id = book.Id;
            viewModel.Title = book.Title;
            viewModel.AuthorName = book.Author.Name;
            viewModel.Country = book.Country;
            viewModel.Pages = book.Pages;
            viewModel.Year = book.Year;
            viewModel.Language = book.Language;
            viewModel.Copies = book.Copies;
            viewModel.SubjectCategoryName = book.SubjectCategory.Name;
            viewModel.CoverImageUrl = book.CoverImageUrl;
            
            return viewModel;
        }
      
    }
}
