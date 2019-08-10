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
            IList<string> parameters = new List<string>();
            var validator = new Mock<IValidator>();
            var bookDb = new Mock<IBooksDataBase>();

            bookDb.Setup(b => b.AllExistingBooksToString());
            var sut = new CatalogCommand(bookDb.Object, validator.Object);
            sut.Execute(parameters);
            bookDb.Verify(b => b.AllExistingBooksToString(), Times.Once);
        }
        [TestMethod]
        public void Parameters_Should_PassThruValidator()
        {
            IList<string> parameters = new List<string> { "neshto" };
            IList<string> validParameters = new List<string>();
            var validator = new Mock<IValidator>();
            var bookDb = new Mock<IBooksDataBase>();

            var sut = new CatalogCommand(bookDb.Object, validator.Object);
            validator.Setup(v => v.IsParametersCountIsValid(validParameters, 0));
            sut.Execute(validParameters);
            validator.Verify(v => v.IsParametersCountIsValid(validParameters, 0), Times.Once);
        }
    }
}
