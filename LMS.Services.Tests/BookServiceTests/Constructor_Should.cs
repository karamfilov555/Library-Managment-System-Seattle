using LMS.Data;
using LMS.Services.Contracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace LMS.Services.Tests.BookServiceTests
{
    [TestClass]
    public class Constructor_Should
    {
        [TestMethod]
        public void Constructor_CreateInstance()
        {
            // Assert
            var options = TestUtilities.GetOptions(MethodBase.GetCurrentMethod().Name);
            var mockAuthorService = new Mock<IAuthorService>();
            var mockSubjectService = new Mock<ISubjectCategoryService>();

            using (var assertContext = new LMSContext(options))
            {
                // Act
                var sut = new BookService(assertContext, mockAuthorService.Object, mockSubjectService.Object);

                // Assert
                Assert.IsNotNull(sut);
            }
        }

        [TestMethod]
        public void Throw_WhenContextIsNull()
        {
            // Assert
            var options = TestUtilities.GetOptions(MethodBase.GetCurrentMethod().Name);
            var mockAuthorService = new Mock<IAuthorService>();
            var mockSubjectService = new Mock<ISubjectCategoryService>();

            using (var assertContext = new LMSContext(options))
            {
                // Act && Assert
                Assert.ThrowsException<ArgumentNullException>(() => new BookService(null,mockAuthorService.Object,mockSubjectService.Object));
            }
        }

        [TestMethod]
        public void Throw_WhenAuthorServiceIsNull()
        {
            // Assert
            var options = TestUtilities.GetOptions(MethodBase.GetCurrentMethod().Name);
            var mockAuthorService = new Mock<IAuthorService>();
            var mockSubjectService = new Mock<ISubjectCategoryService>();

            using (var assertContext = new LMSContext(options))
            {
                // Act && Assert
                Assert.ThrowsException<ArgumentNullException>(() => new BookService(assertContext, null, mockSubjectService.Object));
            }
        }
        
        [TestMethod]
        public void Throw_WhenSubjectServiceIsNull()
        {
            // Assert
            var options = TestUtilities.GetOptions(MethodBase.GetCurrentMethod().Name);
            var mockAuthorService = new Mock<IAuthorService>();
            var mockSubjectService = new Mock<ISubjectCategoryService>();

            using (var assertContext = new LMSContext(options))
            {
                // Act && Assert
                Assert.ThrowsException<ArgumentNullException>(() => new BookService(assertContext, mockAuthorService.Object, null));
            }
        }
    }
}
