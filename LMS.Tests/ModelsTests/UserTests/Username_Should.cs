using LMS.Models;
using LMS.Models.ModelsContracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace LMS.Tests.UserTests
{
    [TestClass]
    public class Username_Should
    {
        private const string username = "username";
        private const string author = "author";
        [TestMethod]
        public void ThrowArgumentException_WhenUsernameShorterThanMinValuePassed()
        {
            // tuk trqbva da izberem mejdu unit testove i private set
            //var user = new User("cool", "tool");
            //Assert.ThrowsException<ArgumentException>(
            //   () => user.Username = "1");
            
        }
    }
}
