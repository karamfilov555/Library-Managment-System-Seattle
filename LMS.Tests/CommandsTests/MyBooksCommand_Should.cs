using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Moq;
using LMS.Contracts;
using LMS.Core.Commands;
using System.Collections.Generic;
using LMS.Models.ModelsContracts;

namespace LMS.Tests.CommandsTests
{
    [TestClass]
    public class MyBooksCommand_Should
    {
        [TestMethod]
        public void Invoke_GetHistoryOfCurrentUserMethod()
        {
            IList<string> parameters = new List<string>();
            var history = new Mock<IHistoryServices>();
            history.Setup(h => h.GetHistoryOfCurrentUser());
            var sut = new MyBooksCommand(history.Object);
            sut.Execute(parameters);
            history.Verify(h => h.GetHistoryOfCurrentUser(), Times.Once);

        }
    }
}
