using LMS.Data;
using LMS.Models.Models;
using LMS.Services;
using LMS.Services.ModelProviders.Contracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace LMS.Tests.IsbnServicesTests
{
    [TestClass]
    public class ProvideIsbn_Should
    {
        [TestMethod]
        public void ReturnInstanceOfTypeIsbn()
        {
            var options = TestUtilities.GetOptions(nameof(ReturnInstanceOfTypeIsbn));
            var mockIsbnFactory = new Mock<IIsbnFactory>();
            mockIsbnFactory.Setup(i => i.CreateIsbn()).Returns(new Isbn {Id = 1,ISBN ="isbn" });

            using (var assertContext = new LMSContext(options))
            {
                var sut = new IsbnServices(assertContext, mockIsbnFactory.Object);
                var isbn = sut.ProvideIsbn();
                Assert.IsInstanceOfType(isbn, typeof(Isbn));
            }
        }
        
    }
}
