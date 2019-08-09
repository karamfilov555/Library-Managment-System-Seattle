using LMS.Models;
using LMS.Models.Enums;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace LMS.Tests.BookTests
{
    [TestClass]
    public class Constructor_Should
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
        public void MakeInstanceOfBookClass_WhenCorrectValuesPassed()
        {
            var sut = new Book(title,author,pages,year,country,language,subject,isbn);
            Assert.IsInstanceOfType(sut, typeof(Book));
        }
        [TestMethod]
        public void SetTitle_WhenCorrectValuePassed()
        {
            var sut = new Book(title, author, pages, year, country, language, subject, isbn);
            Assert.AreEqual(title, sut.Title);
        }
        [TestMethod]
        public void SetAuthor_WhenCorrectValuePassed()
        {
            var sut = new Book(title, author, pages, year, country, language, subject, isbn);
            Assert.AreEqual(author, sut.Author);
        }
        [TestMethod]
        public void SetPages_WhenCorrectValuePassed()
        {
            var sut = new Book(title, author, pages, year, country, language, subject, isbn);
            Assert.AreEqual(pages, sut.Pages);
        }
        [TestMethod]
        public void SetYear_WhenCorrectValuePassed()
        {
            var sut = new Book(title, author, pages, year, country, language, subject, isbn);
            Assert.AreEqual(year, sut.Year);
        }
        [TestMethod]
        public void SetCountry_WhenCorrectValuePassed()
        {
            var sut = new Book(title, author, pages, year, country, language, subject, isbn);
            Assert.AreEqual(country, sut.Country);
        }
        [TestMethod]
        public void SetLanguage_WhenCorrectValuePassed()
        {
            var sut = new Book(title, author, pages, year, country, language, subject, isbn);
            Assert.AreEqual(language, sut.Language);
        }
        [TestMethod]
        public void SetSubject_WhenCorrectValuePassed()
        {
            var sut = new Book(title, author, pages, year, country, language, subject, isbn);
            Assert.AreEqual(subject, sut.Subject);
        }
        [TestMethod]
        public void SetISBN_WhenCorrectValuePassed()
        {
            var sut = new Book(title, author, pages, year, country, language, subject, isbn);
            Assert.AreEqual(isbn, sut.ISBN);
        }
        [TestMethod]
        public void ThrowArgumentException_WhenTitleIsShorterThanMinValue()
        {
            Assert.ThrowsException<ArgumentException>(
                () => new Book("1", author, pages, year, country, language, subject, isbn));
        }
        [TestMethod]
        public void ThrowArgumentException_WhenTitleIsLongerThanMaxValue()
        {
            Assert.ThrowsException<ArgumentException>(
                () => new Book(new string(' ',101), author, pages, year, country, language, subject, isbn));
        }
        [TestMethod]
        public void ThrowArgumentException_WhenTitleIsNull()
        {
            Assert.ThrowsException<ArgumentException>(
                () => new Book(null, author, pages, year, country, language, subject, isbn));
        }
        [TestMethod]
        public void ThrowCorrectArgumentExceptionMessage_WhenTitleIsNull()
        {
            var sut = Assert.ThrowsException<ArgumentException>(
                () => new Book(null, author, pages, year, country, language, subject, isbn));
            Assert.AreEqual("Book's title must be between 2 and 100 symbols!", sut.Message);
        }
        [TestMethod]
        public void ThrowCorrectArgumentExceptionMessage_WhenTitleIsLongerThanMaxValue()
        {
            var sut = Assert.ThrowsException<ArgumentException>(
                () => new Book(new string('t',101), author, pages, year, country, language, subject, isbn));
            Assert.AreEqual("Book's title must be between 2 and 100 symbols!", sut.Message);
        }
        [TestMethod]
        public void ThrowCorrectArgumentExceptionMessage_WhenTitleIsShorterThanMinValue()
        {
            var sut = Assert.ThrowsException<ArgumentException>(
                () => new Book(new string('t', 1), author, pages, year, country, language, subject, isbn));
            Assert.AreEqual("Book's title must be between 2 and 100 symbols!", sut.Message);
        }
        [TestMethod]
        public void ThrowArgumentException_WhenAuthorIsShorterThanMinValue()
        {
            Assert.ThrowsException<ArgumentException>(
                () => new Book(title, "12", pages, year, country, language, subject, isbn));
        }
        [TestMethod]
        public void ThrowArgumentException_WhenAuthorIsLongerThanMaxValue()
        {
            Assert.ThrowsException<ArgumentException>(
                () => new Book(title, new string(' ', 101), pages, year, country, language, subject, isbn));
        }
        [TestMethod]
        public void ThrowArgumentException_WhenAuthorIsNull()
        {
            Assert.ThrowsException<ArgumentException>(
                () => new Book(title, null, pages, year, country, language, subject, isbn));
        }
        [TestMethod]
        public void ThrowCorrectArgumentExceptionMessage_WhenAuthorIsNull()
        {
            var sut = Assert.ThrowsException<ArgumentException>(
                () => new Book(title, null, pages, year, country, language, subject, isbn));
            Assert.AreEqual("Book's author name must be between 3 and 50 symbols!", sut.Message);
        }
        [TestMethod]
        public void ThrowCorrectArgumentExceptionMessage_WhenAuthorIsLongerThanMaxValue()
        {
            var sut = Assert.ThrowsException<ArgumentException>(
                () => new Book(title, new string('t', 51), pages, year, country, language, subject, isbn));
            Assert.AreEqual("Book's author name must be between 3 and 50 symbols!", sut.Message);
        }
        [TestMethod]
        public void ThrowCorrectArgumentExceptionMessage_WhenAuthorIsShorterThanMinValue()
        {
            var sut = Assert.ThrowsException<ArgumentException>(
                () => new Book(title, "1", pages, year, country, language, subject, isbn));
            Assert.AreEqual("Book's author name must be between 3 and 50 symbols!", sut.Message);
        }
        [TestMethod]
        public void ThrowArgumentException_WhenPagesAreSmallerThanMinValue()
        {
            Assert.ThrowsException<ArgumentException>(
                () => new Book(title, author, 0, year, country, language, subject, isbn));
        }
        [TestMethod]
        public void ThrowArgumentException_WhenPagesAreBiggerThanMaxValue()
        {
            Assert.ThrowsException<ArgumentException>(
                () => new Book(title,author,100001, year, country, language, subject, isbn));
        }
        [TestMethod]
        public void ThrowCorrectArgumentExceptionMessage_WhenPagesAreBiggerThanMaxValue()
        {
            var sut = Assert.ThrowsException<ArgumentException>(
                () => new Book(title, author, 100001, year, country, language, subject, isbn));
            Assert.AreEqual("Book's pages must be between 1 and 100000!", sut.Message);
        }
        [TestMethod]
        public void ThrowCorrectArgumentExceptionMessage_WhenPagesAreSmallerThanMinValue()
        {
            var sut = Assert.ThrowsException<ArgumentException>(
                () => new Book(title, author, 0, year, country, language, subject, isbn));
            Assert.AreEqual("Book's pages must be between 1 and 100000!", sut.Message);
        }
        [TestMethod]
        public void ThrowArgumentException_WhenYearIsSmallerThanMinValue()
        {
            Assert.ThrowsException<ArgumentException>(
                () => new Book(title, author, pages, -10001, country, language, subject, isbn));
        }
        [TestMethod]
        public void ThrowArgumentException_WhenYearIsBiggerThanMaxValue()
        {
            Assert.ThrowsException<ArgumentException>(
                () => new Book(title, author, pages, 2029, country, language, subject, isbn));
        }
        [TestMethod]
        public void ThrowCorrectArgumentExceptionMessage_WhenYearIsBiggerThanMaxValue()
        {
            var sut = Assert.ThrowsException<ArgumentException>(
                () => new Book(title, author, pages, 2029, country, language, subject, isbn));
            Assert.AreEqual("Publication year can be greater than present year!", sut.Message);
        }
        [TestMethod]
        public void ThrowCorrectArgumentExceptionMessage_WhenYearIsSmallerThanMinValue()
        {
            var sut = Assert.ThrowsException<ArgumentException>(
                () => new Book(title, author, pages, -10001, country, language, subject, isbn));
            Assert.AreEqual("It has been a long long time ago...be more modern", sut.Message);
        }
        [TestMethod]
        public void ThrowArgumentException_WhenCountryIsShorterThanMinValue()
        {
            Assert.ThrowsException<ArgumentException>(
                () => new Book(title, author, pages, year, "12", language, subject, isbn));
        }
        [TestMethod]
        public void ThrowArgumentException_WhenCountryIsLongerThanMaxValue()
        {
            Assert.ThrowsException<ArgumentException>(
                () => new Book(title,author, pages, year, new string('d',51), language, subject, isbn));
        }
        [TestMethod]
        public void ThrowArgumentException_WhenCountryIsNull()
        {
            Assert.ThrowsException<ArgumentException>(
                () => new Book(title, author, pages, year, null, language, subject, isbn));
        }
        [TestMethod]
        public void ThrowCorrectArgumentExceptionMessage_WhenCountryIsNull()
        {
            var sut = Assert.ThrowsException<ArgumentException>(
                () => new Book(title, author, pages, year, null, language, subject, isbn));
            Assert.AreEqual("Country name must be between 3 and 50 symbols!", sut.Message);
        }
        [TestMethod]
        public void ThrowCorrectArgumentExceptionMessage_WhenCountryIsLongerThanMaxValue()
        {
            var sut = Assert.ThrowsException<ArgumentException>(
                () => new Book(title, author, pages, year, new string('t', 51), language, subject, isbn));
            Assert.AreEqual("Country name must be between 3 and 50 symbols!", sut.Message);
        }
        [TestMethod]
        public void ThrowCorrectArgumentExceptionMessage_WhenCountryIsShorterThanMinValue()
        {
            var sut = Assert.ThrowsException<ArgumentException>(
                () => new Book(title, author, pages, year, "12", language, subject, isbn));
            Assert.AreEqual("Country name must be between 3 and 50 symbols!", sut.Message);
        }
        ///
        [TestMethod]
        public void ThrowArgumentException_WhenLanguageIsShorterThanMinValue()
        {
            Assert.ThrowsException<ArgumentException>(
                () => new Book(title, author, pages, year, country, "12", subject, isbn));
        }
        [TestMethod]
        public void ThrowArgumentException_WhenLanguageIsLongerThanMaxValue()
        {
            Assert.ThrowsException<ArgumentException>(
                () => new Book
                (title, author, pages, year, country, new string('d',51), subject, isbn));
        }
        [TestMethod]
        public void ThrowArgumentException_WhenLanguageIsNull()
        {
            Assert.ThrowsException<ArgumentException>(
                () => new Book(title, author, pages, year, country, null, subject, isbn));
        }
        [TestMethod]
        public void ThrowCorrectArgumentExceptionMessage_WhenLanguageIsNull()
        {
            var sut = Assert.ThrowsException<ArgumentException>(
                () => new Book(title, author, pages, year, country, null, subject, isbn));
            Assert.AreEqual("Language name must be between 3 and 50 symbols!", sut.Message);
        }
        [TestMethod]
        public void ThrowCorrectArgumentExceptionMessage_WhenLanguageIsLongerThanMaxValue()
        {
            var sut = Assert.ThrowsException<ArgumentException>(
                () => new Book(title, author, pages, year,country , new string('t', 51), subject, isbn));
            Assert.AreEqual("Language name must be between 3 and 50 symbols!", sut.Message);
        }
        [TestMethod]
        public void ThrowCorrectArgumentExceptionMessage_WhenLanguageIsShorterThanMinValue()
        {
            var sut = Assert.ThrowsException<ArgumentException>(
                () => new Book(title, author, pages, year, country, "12", subject, isbn));
            Assert.AreEqual("Language name must be between 3 and 50 symbols!", sut.Message);
        }
        [TestMethod]
        public void ThrowArgumentException_WhenIsbnIsNull()
        {
            Assert.ThrowsException<ArgumentException>(
                () => new Book(title, author, pages, year, country, language, subject, null));
        }
        [TestMethod]
        public void ThrowCorrectArgumentExceptionMessage_WhenIsbnIsNull()
        {
            var sut = Assert.ThrowsException<ArgumentException>(
                () => new Book(title, author, pages, year, country, language, subject, null));
            Assert.AreEqual("ISBN cannot be null!", sut.Message);
        }
    }
}
