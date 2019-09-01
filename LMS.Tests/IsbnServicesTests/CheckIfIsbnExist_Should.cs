using LMS.Data;
using LMS.Models.Models;
using LMS.Services;
using LMS.Services.ModelProviders.Contracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;


namespace LMS.Tests.IsbnServicesTests
{
    [TestClass]
    public class CheckIfIsbnExist_Should
    {
        [TestMethod]
        public void ReturnTrue_WhenStringValuePassed_AndDbContainsThisValueAsIsbnCode()
        {
            var options = TestUtilities.GetOptions(nameof(ReturnTrue_WhenStringValuePassed_AndDbContainsThisValueAsIsbnCode));
            var mockIsbnFactory = new Mock<IIsbnFactory>();

            using (var arrangeContext = new LMSContext(options))
            {
                arrangeContext.Isbns.Add(new Isbn { Id = 1, ISBN = "isbn" });
                arrangeContext.SaveChanges();
            }
            using (var assertContext = new LMSContext(options))
            {
                var sut = new IsbnServices(assertContext, mockIsbnFactory.Object);
                var isbn = sut.CheckIfIsbnExist("isbn");
                Assert.AreEqual(true, isbn);
            }
        }
        [TestMethod]
        public void ReturnFalse_WhenStringValuePassed_AndDbDoesNotContainsThisValueAsIsbnCode()
        {
            var options = TestUtilities.GetOptions(nameof(ReturnFalse_WhenStringValuePassed_AndDbDoesNotContainsThisValueAsIsbnCode));
            var mockIsbnFactory = new Mock<IIsbnFactory>();

            
            using (var assertContext = new LMSContext(options))
            {
                var sut = new IsbnServices(assertContext, mockIsbnFactory.Object);
                var isbn = sut.CheckIfIsbnExist("isbn");
                Assert.AreEqual(false, isbn);
            }
        }
    }
}
