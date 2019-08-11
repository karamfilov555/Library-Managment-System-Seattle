using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Moq;
using LMS.JasonDB.Contracts;
using LMS.Contracts;
using LMS.Services;
using LMS.Core.Contracts;
using LMS.Core.Commands;
using LMS.Models.ModelsContracts;
using System.Collections;
using System.Collections.Generic;

namespace LMS.Tests.ModelsTests.ServicesTests.BooksDatabaseTest
{
    [TestClass]
    public class BooksDatabase_Should
    {
        IList<string> parameters = new List<string>();
        [TestMethod]
        public void Constructor_Should_MakeInstance()
        {
            var json = new Mock<IJson>();
            var validator = new Mock<IValidator>();
            var messages = new Mock<IGlobalMessages>();
            var sut = new BookServices(json.Object, validator.Object, messages.Object);

            Assert.IsInstanceOfType(sut, typeof(IBookServices));
        }
        [TestMethod]
        public void AddBookCommand_Should_AddBookToDb()
        {
            //var validator = new Mock<IValidator>();
            //var messages = new Mock<IGlobalMessages>();
            //var factory = new Mock<IModelsFactory>();
            //var bookDb = new Mock<IBooksDataBase>();
            //var reader = new Mock<IInputReader>();
            //var authenticator = new Mock<ILoginAuthenticator>();
            //var writer = new Mock<IOutputWriter>();
            

            //var sut = new AddBookCommand(validator.Object, messages.Object, factory.Object,reader.Object,authenticator.Object,writer.Object,bookDb.Object);
            ////reader.Setup(r=>r.ReadLine().)
            //var book = new Mock<IBook>();
            //factory.Setup(f => f.CreateBook("title", "author", 253, 2000, "Bulg", "bulg", "Drama")).Returns(book.Object);
            //book.Setup(b => b.PrintBookInfo());//.Returns("Expected");
            //bookDb.Setup(x => x.AddBookToDb(book.Object));

            //sut.Execute(parameters);
            //bookDb.Verify(m => m.AddBookToDb(book.Object), Times.Once);

        }
    }
}
