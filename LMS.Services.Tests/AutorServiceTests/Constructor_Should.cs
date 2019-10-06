using LMS.Data;
using LMS.Services.Contracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace LMS.Services.Tests.AutorServiceTests
{
    [TestClass]
    public class Constructor_Should
    {
        [TestMethod]
        public void Constructor_CreatesInstance()
        {
            // Arrange
            var options = TestUtilities.GetOptions(MethodBase.GetCurrentMethod().Name);

            using (var assertContext = new LMSContext(options))
            {
                // Act 
                var sut = new AuthorService(assertContext);

                // Assert
                Assert.IsNotNull(sut);
            }
        }

        [TestMethod]
        public void Throw_WhenTheContextIsNull()
        {
            // Arrange
            var options = TestUtilities.GetOptions(MethodBase.GetCurrentMethod().Name);

            using (var assertContext = new LMSContext(options))
            {
                // Act && Assert
                Assert.ThrowsException<ArgumentNullException>(() => new AuthorService(null));
            }
        }
    }
}
