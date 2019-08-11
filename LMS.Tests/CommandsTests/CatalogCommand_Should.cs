using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Moq;
using LMS.Contracts;
using LMS.Core.Commands;
using System.Collections.Generic;

namespace LMS.Tests.CommandsTests
{
    [TestClass]
    public class CatalogCommand_Should
    {
        [TestMethod]
        public void ReturnAllExistingBooksToString()
        {
            //Arange
            IList<string> parameters = new List<string>();
            var bookDb = new Mock<IBookServices>();
            bookDb.Setup(b => b.AllExistingBooksToString());
            var sut = new CatalogCommand(bookDb.Object);
            //Act
            sut.Execute(parameters);
            //Assert
            bookDb.Verify(b => b.AllExistingBooksToString(), Times.Once);
        }
    }
}
