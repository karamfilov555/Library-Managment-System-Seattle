using LMS.Data;
using LMS.Models;
using LMS.Services;
using LMS.Services.Contracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Linq;

namespace LMS.Tests.HistoryServicesTests
{
    [TestClass]
    public class ReturnBook_Should
    {
        [TestMethod]
        public void SetIsReturnStatusToTrue_IfBookExistInCurrentUserHistory_AndIsNotReturned()
        {
            var options = TestUtilities.GetOptions(nameof(SetIsReturnStatusToTrue_IfBookExistInCurrentUserHistory_AndIsNotReturned));
            var mockLoginAuthenticator = new Mock<ILoginAuthenticator>();
            mockLoginAuthenticator.Setup(l => l.LoggedUser()).Returns(new User { Id = 3, Username = "user" });
            var mockRecordFines = new Mock<IRecordFinesServices>().Object;
            var mockBookServices = new Mock<IBookServices>().Object;

            using (var arrangeContext = new LMSContext(options))
            {
                arrangeContext.Users.Add(new User { Id = 3, Username = "user" });
                arrangeContext.SaveChanges();
                arrangeContext.Books.Add(new Book
                {
                    Id = 3,
                    Title = "title",
                    IsCheckedOut = true
                });
                arrangeContext.SaveChanges();
                arrangeContext.HistoryRegistries.Add(new HistoryRegistry
                {
                    UserId = 3,
                    BookId = 3,
                    IsReturned = false
                });
                arrangeContext.SaveChanges();
            }
            using (var assertContext = new LMSContext(options))
            {
                var sut = new HistoryServices(assertContext, mockLoginAuthenticator.Object, mockRecordFines, mockBookServices);
                var historyRegistry = assertContext.HistoryRegistries.First();
                sut.ReturnBook("title");
                Assert.AreEqual(true, historyRegistry.IsReturned);
            }
        }
        [TestMethod]
        public void ThrowArgumentExeption_IfLoggedUser_HaveNoBooksCheckedOut()
        {
            var options = TestUtilities.GetOptions(nameof(ThrowArgumentExeption_IfLoggedUser_HaveNoBooksCheckedOut));
            var mockLoginAuthenticator = new Mock<ILoginAuthenticator>();
            mockLoginAuthenticator.Setup(l => l.LoggedUser()).Returns(new User { Id = 3, Username = "user" });
            var mockRecordFines = new Mock<IRecordFinesServices>().Object;
            var mockBookServices = new Mock<IBookServices>().Object;

            using (var arrangeContext = new LMSContext(options))
            {
                arrangeContext.Users.Add(new User { Id = 3, Username = "user" });
                arrangeContext.SaveChanges();
            }
            using (var assertContext = new LMSContext(options))
            {
                var sut = new HistoryServices(assertContext, mockLoginAuthenticator.Object, mockRecordFines, mockBookServices);
                
                Assert.ThrowsException<ArgumentException>(
                    ()=> sut.ReturnBook("title")); 
            }
        }
        [TestMethod]
        public void ThrowCorrectMsg_IfLoggedUser_HaveNoBooksCheckedOut()
        {
            var options = TestUtilities.GetOptions(nameof(ThrowCorrectMsg_IfLoggedUser_HaveNoBooksCheckedOut));
            var mockLoginAuthenticator = new Mock<ILoginAuthenticator>();
            mockLoginAuthenticator.Setup(l => l.LoggedUser()).Returns(new User { Id = 3, Username = "user" });
            var mockRecordFines = new Mock<IRecordFinesServices>().Object;
            var mockBookServices = new Mock<IBookServices>().Object;

            using (var arrangeContext = new LMSContext(options))
            {
                arrangeContext.Users.Add(new User { Id = 3, Username = "user" });
                arrangeContext.SaveChanges();
            }
            using (var assertContext = new LMSContext(options))
            {
                var sut = new HistoryServices(assertContext, mockLoginAuthenticator.Object, mockRecordFines, mockBookServices);

                var exp = Assert.ThrowsException<ArgumentException>(
                    () => sut.ReturnBook("title"));
                Assert.AreEqual("You have no books to return!", exp.Message);
            }
        }
        [TestMethod]
        public void ThrowArgumentException_IfLoggedUser_DoesntHaveThisBookToReturn()
        {
            var options = TestUtilities.GetOptions(nameof(ThrowArgumentException_IfLoggedUser_DoesntHaveThisBookToReturn));
            var mockLoginAuthenticator = new Mock<ILoginAuthenticator>();
            mockLoginAuthenticator.Setup(l => l.LoggedUser()).Returns(new User { Id = 3, Username = "user" });
            var mockRecordFines = new Mock<IRecordFinesServices>().Object;
            var mockBookServices = new Mock<IBookServices>().Object;

            using (var arrangeContext = new LMSContext(options))
            {
                arrangeContext.Users.Add(new User { Id = 3, Username = "user" });
                arrangeContext.SaveChanges();
                arrangeContext.Books.Add(new Book {Id = 3, Title = "title",IsCheckedOut = true});
                arrangeContext.SaveChanges();
                arrangeContext.Books.Add(new Book { Id = 4, Title = "other", IsCheckedOut = true });
                arrangeContext.SaveChanges();
                arrangeContext.HistoryRegistries.Add(new HistoryRegistry {UserId = 3,BookId = 3,IsReturned=false });
                arrangeContext.SaveChanges();
            }
            using (var assertContext = new LMSContext(options))
            {
                var sut = new HistoryServices(assertContext, mockLoginAuthenticator.Object, mockRecordFines, mockBookServices);

                Assert.ThrowsException<ArgumentException>(
                    () => sut.ReturnBook("other"));
            }
        }
        [TestMethod]
        public void ThrowCorrectMsg_IfLoggedUser_DoesntHaveThisBookToReturn()
        {
            var options = TestUtilities.GetOptions(nameof(ThrowCorrectMsg_IfLoggedUser_DoesntHaveThisBookToReturn));
            var mockLoginAuthenticator = new Mock<ILoginAuthenticator>();
            mockLoginAuthenticator.Setup(l => l.LoggedUser()).Returns(new User { Id = 3, Username = "user" });
            var mockRecordFines = new Mock<IRecordFinesServices>().Object;
            var mockBookServices = new Mock<IBookServices>().Object;

            using (var arrangeContext = new LMSContext(options))
            {
                arrangeContext.Users.Add(new User { Id = 3, Username = "user" });
                arrangeContext.SaveChanges();
                arrangeContext.Books.Add(new Book { Id = 3, Title = "title", IsCheckedOut = true });
                arrangeContext.SaveChanges();
                arrangeContext.Books.Add(new Book { Id = 4, Title = "other", IsCheckedOut = true });
                arrangeContext.SaveChanges();
                arrangeContext.HistoryRegistries.Add(new HistoryRegistry { UserId = 3, BookId = 3, IsReturned = false });
                arrangeContext.SaveChanges();
            }
            using (var assertContext = new LMSContext(options))
            {
                var sut = new HistoryServices(assertContext, mockLoginAuthenticator.Object, mockRecordFines, mockBookServices);

                var exp = Assert.ThrowsException<ArgumentException>(
                    () => sut.ReturnBook("other"));
                Assert.AreEqual($"There are no book with title: \"other\" in your checkout history!", exp.Message);
            }
        }
    }
}
