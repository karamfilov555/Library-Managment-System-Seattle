using LMS.Models;
using LMS.Models.Enums;
using LMS.Models.ModelsContracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace LMS.Tests.BookTests
{
    [TestClass]
    public class PrintBookInfoMethod_Should
    {
        private const string title = "title";
        private const string author = "author";
        private const int pages = 200;
        private const int year = 2019;
        private const string country = "Bulgaria";
        private const string language = "bulgarian";
        private const SubjectCategory subject = SubjectCategory.Historical;
        private const string isbn = "978-1-940313-09-165351035";

        [TestMethod]
        public void ReturnPassedData()
        {
            //var bookMocked = new Mock<IBook>();
            //bookMocked.Setup(x => x.PrintBookInfo());
            //bookMocked.Print
            //bookMocked.Verify(b => b.PrintBookInfo(), Times.Once);
        }
    }
}
