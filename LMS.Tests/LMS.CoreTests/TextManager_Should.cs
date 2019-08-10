using LMS.Core.Contracts;
using LMS.Core.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace LMS.Tests.LMS.CoreTests
{
    [TestClass]
    public class TextManager_Should
    {
        IList<string> parameteres = new List<string> { "login", "parola","riba","me4"};
        
        [TestMethod]
        public void Constructor_ShouldMakeInstanceOfTextManager()
        {
            var sut = new TextManager();
            Assert.IsInstanceOfType(sut, typeof(ITextManager));
        }
        [TestMethod]
        public void ExtractCommandNameMethod_ShouldReturnCommandName()
        {
            var sut = new TextManager();
            var commandName = sut.ExtractCommandName("login lek den");
            Assert.AreEqual("login", commandName);
        }
        [TestMethod]
        public void GetParamsMethod_ShouldReturnCommandParametersToString()
        {
            var sut = new TextManager();
            var actual = sut.GetParams(parameteres);
            var expected = "login parola riba me4";
            Assert.AreEqual(expected, actual);
        }
    }
}
