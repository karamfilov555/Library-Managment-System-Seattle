using LMS.Models;
using LMS.Models.ModelsContracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace LMS.Tests.ModelsTests.HistoryRegistryTest
{
    [TestClass]
    public class Constructor_Should
    {
        private const string title = "Blindness";
        private const string ISBN = "978-1-940313-09-0083";
        private const string username = "Test";
        private const string returnDate = "14-Aug-19";

        [TestMethod]
        public void MakeInstanceOfHistoryRegistry_WhenCorrectValuesPassed()
        {
            var sut = new HistoryRegistry(title, ISBN, username, returnDate);

            Assert.IsInstanceOfType(sut,typeof(IHistoryRegistry));
        }
        [TestMethod]
        public void PassCorrectlyAssingedTitle()
        {
            var sut = new HistoryRegistry(title, ISBN, username, returnDate);
            Assert.AreEqual(title, sut.Title);
        }
        [TestMethod]
        public void PassCorrectlyAssingedISBN()
        {
            var sut = new HistoryRegistry(title, ISBN, username, returnDate);
            Assert.AreEqual(ISBN, sut.ISBN);
        }
        [TestMethod]
        public void PassCorrectlyAssingedUsername()
        {
            var sut = new HistoryRegistry(title, ISBN, username, returnDate);
            Assert.AreEqual(username, sut.Username);
        }
        [TestMethod]
        public void PassCorrectlyAssingedReturnDate()
        {
            var sut = new HistoryRegistry(title, ISBN, username, returnDate);
            Assert.AreEqual(returnDate, sut.ReturnDate);
        }
    }
}
