using LMS.Contracts;
using LMS.Core.Factories;
using LMS.Generators.Contracts;
using LMS.Models;
using LMS.Models.ModelsContracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace LMS.Tests.LMS.CoreTests
{
    [TestClass]
    public class Factory_Should
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
        [TestMethod]
        public void CreateInstanceOfBook_WhenValidValuesPassed()
        {
            //Arrange
            var authenticatorMocked = new Mock<ILoginAuthenticator>();
            var generatorMocked = new Mock<IIsbnGenerator>();
            generatorMocked.Setup(x => x.GenerateISBN()).Returns("dddddd");
            var factory = new ModelsFactory(authenticatorMocked.Object,generatorMocked.Object);
            //Act
            var sut = factory.CreateBook(title, author, pages, year, country, language, subject,isbn);
            //Assert
            Assert.IsInstanceOfType(sut, typeof(IBook));
        }
        [TestMethod]
        public void CreateInstanceOfUser_WhenValidValuesPassed()
        {
            //Arrange
            var authenticatorMocked = new Mock<ILoginAuthenticator>();
            var generatorMocked = new Mock<IIsbnGenerator>();
            var factory = new ModelsFactory(authenticatorMocked.Object, generatorMocked.Object);
            //Act
            var sut = factory.CreateUser(username,password);
            //Assert
            Assert.IsInstanceOfType(sut, typeof(IUser));
        }
        [TestMethod]
        public void CreateInstanceOfHistoryRegistry_WhenValidValuesPassed()
        {
            //Arrange
            var authenticatorMocked = new Mock<ILoginAuthenticator>();
            authenticatorMocked.Setup(x => x.GetCurrentUserName()).Returns("Cool");
            var generatorMocked = new Mock<IIsbnGenerator>();
            var factory = new ModelsFactory(authenticatorMocked.Object, generatorMocked.Object);
            //Act
            var sut = factory.CreateRegistry(title, author, pages, year, country, language, subject, isbn);
            //Assert
            Assert.IsInstanceOfType(sut, typeof(IHistoryRegistry));
        }
        [TestMethod]
        public void ConstructorShould_CreateInstanceOfModelsFactory()
        {
            //Arrange
            var authenticatorMocked = new Mock<ILoginAuthenticator>();
            var generatorMocked = new Mock<IIsbnGenerator>();
            var factory = new ModelsFactory(authenticatorMocked.Object, generatorMocked.Object);
            //Act & Assert
            Assert.IsInstanceOfType(factory, typeof(IModelsFactory));
        }
    }
}
