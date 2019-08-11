using LMS.Models;
using LMS.Models.ModelsContracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace LMS.Tests.ModelsTests.HistoryRegistryTests
{
    [TestClass]
    public class Constructor_Should
    {
        private const string title = "title";
        private const string username = "user";
        private const string returnDate = "date";
        
        private const string isbn = "978-1-940313-09-165351035";
        [TestMethod]
        public void MakeInstanceOfBookClass_WhenCorrectValuesPassed()
        {
            var sut = new HistoryRegistry(title,username,returnDate,isbn);
            Assert.IsInstanceOfType(sut, typeof(IHistoryRegistry));
        }
    }
}
