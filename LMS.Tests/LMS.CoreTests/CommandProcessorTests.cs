using LMS.Contracts;
using LMS.Core.CommandContracts;
using LMS.Core.Contracts;
using LMS.Core.LmsCommandProcessor;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LMS.Tests.LMS.CoreTests
{
    [TestClass]
    public class CommandProcessorTests
    {
        IEnumerable<string> parameters = new List<string> { "Todor", "3333" };
        IList<string> fullParams = new List<string> { "login","Todor", "3333" };
        [TestMethod]
        public void Constructor_ShouldMakeAnInstanceWhenValidValuesPassed()
        {
            var factoryMocked = new Mock<ICommandFactory>();
            var textManagerMocked = new Mock<ITextManager>();
            var sut = new CommandProcessor(factoryMocked.Object,textManagerMocked.Object);
            Assert.IsInstanceOfType(sut, typeof(ICommandProcessor));
        }
        [TestMethod]
        public void ProcessCommand_ShouldExecuteCommandWhenValidCommandPassed()
        {
            //var factoryMocked = new Mock<ICommandFactory>();
            //var textManagerMocked = new Mock<ITextManager>();
            //var command = new Mock<ICommand>();

            //var sut = new CommandProcessor(factoryMocked.Object, textManagerMocked.Object);
            //command.Setup(x => x.Execute(fullParams));
            //textManagerMocked.Setup(x => x.GetCommandParams("login Todor 3333"))
            //    .Returns(parameters);
            //textManagerMocked.Setup(x => x.ExtractCommandName("login Todor 3333"))
            //    .Returns("login");
            //factoryMocked.Setup(x => x.FindCommand("login")).Returns(command.Object);
            
            //var output = command.Object.Execute(parameters.ToList());
            //var result = sut.ProcessCommand("login Todor 3333");
            //Assert.AreEqual("Successfully Login! Hello, Todor", result);
        }
    }
}
