using LMS.Data;
using LMS.Models;
using LMS.Services;
using LMS.Services.Contracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LMS.Tests.BookServicesTests
{
    [TestClass]
    public class SetReserveBookStatus_Should
    {
        [TestMethod]
        public void ChangeBook_ReservationStatus_ToFalse_IfBookIsReserved()
        {
            var title = "title";
            var author = "author";
            var options = TestUtilities.GetOptions(nameof(ChangeBook_ReservationStatus_ToFalse_IfBookIsReserved));
            var mockLoginAuthenticator = new Mock<ILoginAuthenticator>().Object;

            using (var arrangeContext = new LMSContext(options))
            {
                arrangeContext.Authors.Add(new Author { Id = 5, Name = author });
                arrangeContext.SaveChanges();
                arrangeContext.Books.Add(new Book()
                {
                    Title = title,
                    AuthorId = 5,
                    IsReserved = true
                }); 
                arrangeContext.SaveChanges();
            }
            using (var actContext = new LMSContext(options))
            {
                var sut = new BookServices(actContext, mockLoginAuthenticator);
                var book = actContext.Books.First(b => b.Title == title);
                sut.SetReserveBookStatusFalse(book);
                Assert.AreEqual(false, book.IsReserved);
            }
        }
    }
}
