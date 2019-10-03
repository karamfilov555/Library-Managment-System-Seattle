using System;
using System.Collections.Generic;
using System.Linq;
using LMS.Models;
using LMS.Web.Models;

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
            viewModel.IsLocked = book.IsLocked;

            return viewModel;
        }
        public static EditViewModel MapToEditViewModel(this Book book)
        {
            var viewModel = new EditViewModel();
            viewModel.Id = book.Id;
            viewModel.Title = book.Title;
            viewModel.AuthorName = book.Author.Name;
            viewModel.Country = book.Country;
            viewModel.Pages = book.Pages;
            viewModel.Year = book.Year;
            viewModel.Language = book.Language;
            viewModel.CoverImageUrl = book.CoverImageUrl;

            return viewModel;
        }
        public static IReadOnlyCollection<CheckoutBookViewModel> MapToCheckOutViewModel(this IDictionary<Book, DateTime> checkOuts)
        {
            var checkOutsOfUserVm = new List<CheckoutBookViewModel>();

            foreach (var checkoutPair in checkOuts)
            {
                //всеки път new , защото е референтен тип и се променят старите иначе.. :)
            var viewModel = new CheckoutBookViewModel();
                viewModel.ReturnDate = checkoutPair.Value;
                viewModel.Id = checkoutPair.Key.Id;
                viewModel.Title = checkoutPair.Key.Title;
                viewModel.AuthorName = checkoutPair.Key.Author.Name;
                viewModel.Country = checkoutPair.Key.Country;
                viewModel.Pages = checkoutPair.Key.Pages;
                viewModel.Year = checkoutPair.Key.Year;
                viewModel.Language = checkoutPair.Key.Language;
                viewModel.Copies = checkoutPair.Key.Copies;
                viewModel.SubjectCategoryName = checkoutPair.Key.SubjectCategory.Name;
                viewModel.CoverImageUrl = checkoutPair.Key.CoverImageUrl;
                checkOutsOfUserVm.Add(viewModel);
            }
            return checkOutsOfUserVm;
        }
        public static BookListViewModel MapToListItemBookViewModel(this Book book)
        {
            var viewModel = new BookListViewModel();
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
            viewModel.IsLocked = book.IsLocked;
            viewModel.Rating = book.BookRating?.Reviews?.Where(r => r.Id == book.BookRating.Id)?.Sum(r => r.Grade) /
                book.BookRating?.Reviews?.Where(r => r.Id == book.BookRating.Id)?.Count() ?? 0;
            //viewModel.Rating = book.BookRating?.Reviews?.Average(r => r.Grade) ?? 0;

            var a = book.BookRating?.Reviews?.Where(x=>x.BookRatingId == book.BookRatingId)?.Select(x => x.Grade)?.Average();
            viewModel.Rating = a;
            return viewModel;
        }
        public static NotificationViewModel MapToNotificationViewModel(this Notification notification)
        {
            var viewModel = new NotificationViewModel();
            viewModel.Id = notification.Id;
            viewModel.UserId = notification.UserId;
            viewModel.EventDate = notification.EventDate;
            viewModel.Description = notification.Description;
            viewModel.Username = notification.Username;
            viewModel.IsSeen = notification.IsSeen;
            return viewModel;
        }
        public static ReviewViewModel MapToReviewViewModel(this Book book , string userId)
        {
            var viewModel = new ReviewViewModel();
            viewModel.Id = book.Id;
            viewModel.CoverImageUrl = book.CoverImageUrl;
            viewModel.UserId = userId;
            viewModel.Title = book.Title;
            viewModel.AuthorName = book.Author.Name;
            viewModel.Pages = book.Pages;
            viewModel.Country = book.Country;
            viewModel.Language = book.Language;
            viewModel.SubjectCategoryName = book.SubjectCategory.Name;
            viewModel.Copies = book.Copies;
            viewModel.Year = book.Year;
            viewModel.Grade = book.BookRating?.Rating;
            return viewModel;
        }
    }
}
