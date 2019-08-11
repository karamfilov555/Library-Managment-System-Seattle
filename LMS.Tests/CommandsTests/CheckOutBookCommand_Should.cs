using LMS.Contracts;
using LMS.Core.Commands;
using LMS.Core.Contracts;
using LMS.Models.Enums;
using LMS.Models.ModelsContracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;

namespace LMS.Tests.CommandsTests
{
    [TestClass]
    public class CheckOutBookCommand_Should
    {
        private const string username = "username";
        private const string password = "password";
        private const string title = "title";
        private const string author = "author";
        private const int pages = 200;
        private const int year = 2019;
        private const string country = "Bulgaria";
        private const string language = "bulgarian";
        private const string subject = "Drama";
        private const string isbn = "978-1-940313-09-165351035";
        private const string returnDate = "14-04-2019";

        [TestMethod]
        public void CheckTheHistoryOfCurrentUser()
        {
            //Arrange
            IList<string> parameters = new List<string> { "title" };
            var textMngrMocked = new Mock<ITextManager>();
            var factoryMocked = new Mock<IModelsFactory>();
            var historyMocked = new Mock<IHistoryServices>();
            var messagesMocked = new Mock<IGlobalMessages>();
            var bookServicesMocked = new Mock<IBookServices>();
            var registryMocked = new Mock<IHistoryRegistry>();

            textMngrMocked.Setup(t => t.GetParams(parameters)).Returns("title");

            var bookMocked = new Mock<IBook>();
            bookMocked.Setup(t => t.Title).Returns("title");
            bookMocked.Setup(t => t.Author).Returns("author");
            bookMocked.Setup(t => t.Pages).Returns(200);
            bookMocked.Setup(t => t.Year).Returns(year);
            bookMocked.Setup(t => t.Country).Returns(country);
            bookMocked.Setup(t => t.Language).Returns(language);
            bookMocked.Setup(t => t.Subject).Returns(SubjectCategory.Action);
            bookMocked.Setup(t => t.ISBN).Returns(isbn);

            bookServicesMocked.Setup(f => f.FindBookInDb("title")).Returns(bookMocked.Object);
            historyMocked.Setup(h => h.CheckBooksOfCurrentUser());

            var registry = factoryMocked.Setup(f => f.CreateRegistry(bookMocked.Object.Title, bookMocked.Object.Author, bookMocked.Object.Pages, bookMocked.Object.Year, bookMocked.Object.Country, bookMocked.Object.Language, bookMocked.Object.Subject.ToString(), bookMocked.Object.ISBN)).Returns(registryMocked.Object);
            //Act
            var sut = new CheckOutBookCommand(textMngrMocked.Object, historyMocked.Object, factoryMocked.Object, messagesMocked.Object, bookServicesMocked.Object);
            sut.Execute(parameters);
            //Verify
            historyMocked.Verify(ch => ch.CheckBooksOfCurrentUser(), Times.Once);
        }
        [TestMethod]
        public void CreateHistoryRegistry_WithBookToCheckOutParameters()
        {
            //Arrange
            IList<string> parameters = new List<string> { "title" };
            var textMngrMocked = new Mock<ITextManager>();
            var factoryMocked = new Mock<IModelsFactory>();
            var historyMocked = new Mock<IHistoryServices>();
            var messagesMocked = new Mock<IGlobalMessages>();
            var bookServicesMocked = new Mock<IBookServices>();
            var registryMocked = new Mock<IHistoryRegistry>();

            textMngrMocked.Setup(t => t.GetParams(parameters)).Returns("title");

            var bookMocked = new Mock<IBook>();
            bookMocked.Setup(t => t.Title).Returns("title");
            bookMocked.Setup(t => t.Author).Returns("author");
            bookMocked.Setup(t => t.Pages).Returns(200);
            bookMocked.Setup(t => t.Year).Returns(year);
            bookMocked.Setup(t => t.Country).Returns(country);
            bookMocked.Setup(t => t.Language).Returns(language);
            bookMocked.Setup(t => t.Subject).Returns(SubjectCategory.Action);
            bookMocked.Setup(t => t.ISBN).Returns(isbn);

            bookServicesMocked.Setup(f => f.FindBookInDb("title")).Returns(bookMocked.Object);
            historyMocked.Setup(h => h.CheckBooksOfCurrentUser());

            var registry = factoryMocked.Setup(f => f.CreateRegistry(bookMocked.Object.Title, bookMocked.Object.Author, bookMocked.Object.Pages, bookMocked.Object.Year, bookMocked.Object.Country, bookMocked.Object.Language, bookMocked.Object.Subject.ToString(), bookMocked.Object.ISBN)).Returns(registryMocked.Object);
            //Act
            var sut = new CheckOutBookCommand(textMngrMocked.Object, historyMocked.Object, factoryMocked.Object, messagesMocked.Object, bookServicesMocked.Object);
            sut.Execute(parameters);
            //Verify
            factoryMocked.Verify(ch => ch.CreateRegistry(bookMocked.Object.Title, bookMocked.Object.Author, bookMocked.Object.Pages, bookMocked.Object.Year, bookMocked.Object.Country, bookMocked.Object.Language, bookMocked.Object.Subject.ToString(), bookMocked.Object.ISBN), Times.Once);
        }
        [TestMethod]
        public void Call_AddRegistryToDbMethod()
        {
            //Arrange
            IList<string> parameters = new List<string> { "title" };
            var textMngrMocked = new Mock<ITextManager>();
            var factoryMocked = new Mock<IModelsFactory>();
            var historyMocked = new Mock<IHistoryServices>();
            var messagesMocked = new Mock<IGlobalMessages>();
            var bookServicesMocked = new Mock<IBookServices>();
            var registryMocked = new Mock<IHistoryRegistry>();

            textMngrMocked.Setup(t => t.GetParams(parameters)).Returns("title");

            var bookMocked = new Mock<IBook>();
            bookMocked.Setup(t => t.Title).Returns("title");
            bookMocked.Setup(t => t.Author).Returns("author");
            bookMocked.Setup(t => t.Pages).Returns(200);
            bookMocked.Setup(t => t.Year).Returns(year);
            bookMocked.Setup(t => t.Country).Returns(country);
            bookMocked.Setup(t => t.Language).Returns(language);
            bookMocked.Setup(t => t.Subject).Returns(SubjectCategory.Action);
            bookMocked.Setup(t => t.ISBN).Returns(isbn);

            bookServicesMocked.Setup(f => f.FindBookInDb("title")).Returns(bookMocked.Object);
            historyMocked.Setup(h => h.CheckBooksOfCurrentUser());

            var registry = factoryMocked.Setup(f => f.CreateRegistry(bookMocked.Object.Title, bookMocked.Object.Author, bookMocked.Object.Pages, bookMocked.Object.Year, bookMocked.Object.Country, bookMocked.Object.Language, bookMocked.Object.Subject.ToString(), bookMocked.Object.ISBN)).Returns(registryMocked.Object);

            historyMocked.Setup(a=>a.AddRegistryToHistoryDb(registryMocked.Object));
            //Act
            var sut = new CheckOutBookCommand(textMngrMocked.Object, historyMocked.Object, factoryMocked.Object, messagesMocked.Object, bookServicesMocked.Object);

            sut.Execute(parameters);
            //Verify
            historyMocked.Verify(a => a.AddRegistryToHistoryDb(registryMocked.Object),Times.Once);
        }
        [TestMethod]
        public void Call_RemoveFromDbBookServicesMethod()
        {
            //Arrange
            IList<string> parameters = new List<string> { "title" };
            var textMngrMocked = new Mock<ITextManager>();
            var factoryMocked = new Mock<IModelsFactory>();
            var historyMocked = new Mock<IHistoryServices>();
            var messagesMocked = new Mock<IGlobalMessages>();
            var bookServicesMocked = new Mock<IBookServices>();
            var registryMocked = new Mock<IHistoryRegistry>();

            textMngrMocked.Setup(t => t.GetParams(parameters)).Returns("title");

            var bookMocked = new Mock<IBook>();
            bookMocked.Setup(t => t.Title).Returns("title");
            bookMocked.Setup(t => t.Author).Returns("author");
            bookMocked.Setup(t => t.Pages).Returns(200);
            bookMocked.Setup(t => t.Year).Returns(year);
            bookMocked.Setup(t => t.Country).Returns(country);
            bookMocked.Setup(t => t.Language).Returns(language);
            bookMocked.Setup(t => t.Subject).Returns(SubjectCategory.Action);
            bookMocked.Setup(t => t.ISBN).Returns(isbn);

            bookServicesMocked.Setup(f => f.FindBookInDb("title")).Returns(bookMocked.Object);
            historyMocked.Setup(h => h.CheckBooksOfCurrentUser());

            var registry = factoryMocked.Setup(f => f.CreateRegistry(bookMocked.Object.Title, bookMocked.Object.Author, bookMocked.Object.Pages, bookMocked.Object.Year, bookMocked.Object.Country, bookMocked.Object.Language, bookMocked.Object.Subject.ToString(), bookMocked.Object.ISBN)).Returns(registryMocked.Object);

            historyMocked.Setup(a => a.AddRegistryToHistoryDb(registryMocked.Object));
            bookServicesMocked.Setup(a => a.RemoveFromDb(bookMocked.Object));
            //Act
            var sut = new CheckOutBookCommand(textMngrMocked.Object, historyMocked.Object, factoryMocked.Object, messagesMocked.Object, bookServicesMocked.Object);
            sut.Execute(parameters);
            //Verify
            bookServicesMocked.Verify(b => b.RemoveFromDb(bookMocked.Object), Times.Once);
        }
        [TestMethod]
        public void Call_BookCheckOutMessage_WithRegistryInfo()
        {
            //Arrange
            IList<string> parameters = new List<string> { "title" };
            var textMngrMocked = new Mock<ITextManager>();
            var factoryMocked = new Mock<IModelsFactory>();
            var historyMocked = new Mock<IHistoryServices>();
            var messagesMocked = new Mock<IGlobalMessages>();
            var bookServicesMocked = new Mock<IBookServices>();
            var registryMocked = new Mock<IHistoryRegistry>();

            textMngrMocked.Setup(t => t.GetParams(parameters)).Returns("title");

            var bookMocked = new Mock<IBook>();
            bookMocked.Setup(t => t.Title).Returns("title");
            bookMocked.Setup(t => t.Author).Returns("author");
            bookMocked.Setup(t => t.Pages).Returns(200);
            bookMocked.Setup(t => t.Year).Returns(year);
            bookMocked.Setup(t => t.Country).Returns(country);
            bookMocked.Setup(t => t.Language).Returns(language);
            bookMocked.Setup(t => t.Subject).Returns(SubjectCategory.Action);
            bookMocked.Setup(t => t.ISBN).Returns(isbn);

            bookServicesMocked.Setup(f => f.FindBookInDb("title")).Returns(bookMocked.Object);
            historyMocked.Setup(h => h.CheckBooksOfCurrentUser());

            var registry = factoryMocked.Setup(f => f.CreateRegistry(bookMocked.Object.Title, bookMocked.Object.Author, bookMocked.Object.Pages, bookMocked.Object.Year, bookMocked.Object.Country, bookMocked.Object.Language, bookMocked.Object.Subject.ToString(), bookMocked.Object.ISBN)).Returns(registryMocked.Object);

            historyMocked.Setup(a => a.AddRegistryToHistoryDb(registryMocked.Object));
            bookServicesMocked.Setup(a => a.RemoveFromDb(bookMocked.Object));

            messagesMocked.Setup(b => b.BookCheckedOutMessage
            (registryMocked.Object.RegistryInfo()));
            //Act
            var sut = new CheckOutBookCommand(textMngrMocked.Object, historyMocked.Object, factoryMocked.Object, messagesMocked.Object, bookServicesMocked.Object);
            sut.Execute(parameters);
            //Verify
            messagesMocked.Verify(
                b => b.BookCheckedOutMessage(registryMocked.Object.RegistryInfo()), Times.Once);
        }
    }
}