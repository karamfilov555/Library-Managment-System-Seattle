using LMS.Contracts;
using LMS.Core.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace LMS.Tests.LMS.CoreTests
{
    [TestClass]
    public class Validator_
    {
        List<string> parameters = new List<string> { "dddd", "dasdada" };
        List<string> pramateresWithOneValue = new List<string> { "dddd"};

        [TestMethod]
        public void Constructor_ShouldMakeInstanceOfValidator()
        {
            var sut = new Validator();
            Assert.IsInstanceOfType(sut, typeof(IValidator));
        }
        [TestMethod]
        public void CancelMembershipCountValidation_ShouldThrowArgumentExWhenParamsAreInvalid()
        {
            var sut = new Validator();

            Assert.ThrowsException<ArgumentException>(
                () => sut.CancelMembershipCountValidation(parameters));
        }
        [TestMethod]
        public void CancelMembershipCountValidation_ShouldThrowCorrectArgumentExMessage()
        {
            var sut = new Validator();
            var expectedMsg = "To Cancel your membership you should enter valid password!";
            var ex = Assert.ThrowsException<ArgumentException>(
                () => sut.CancelMembershipCountValidation(parameters));
            Assert.AreEqual(expectedMsg, ex.Message);
        }
        [TestMethod]
        public void CancelMembershipCountValidation_ShouldNotThrowWhenValidValuesPassed()
        {
            var sut = new Validator();
            sut.CancelMembershipCountValidation(pramateresWithOneValue);
            var expectedCount = 2;
            Assert.AreEqual(expectedCount, parameters.Count);
        }
        [TestMethod]
        public void LoginParametersCountValidation_ShouldThrowArgumentExWhenParamsAreInvalid()
        {
            var sut = new Validator();

            Assert.ThrowsException<ArgumentException>(
                () => sut.LoginParametersCountValidation(pramateresWithOneValue));
        }
        [TestMethod]
        public void LoginParametersCountValidation_ShouldThrowCorrectArgumentExMessage()
        {
            var sut = new Validator();

            var ex = Assert.ThrowsException<ArgumentException>(
                () => sut.LoginParametersCountValidation(pramateresWithOneValue));
            var expectedMsg = "To Login into the System you should enter Username and Password!";
            Assert.AreEqual(expectedMsg, ex.Message);
        }
        [TestMethod]
        public void LoginParametersCountValidation_ShouldNotThrowWhenValidValuesPassed()
        {
            var sut = new Validator();
            sut.LoginParametersCountValidation(parameters);
            var expectedCount = 2;
            Assert.AreEqual(expectedCount, parameters.Count);
        }
        [TestMethod]
        public void RegisterParametersCountValidation_ShouldNotThrowWhenValidValuesPassed()
        {
            var sut = new Validator();
            sut.RegisterParametersCountValidation(parameters);
            var expectedCount = 2;
            Assert.AreEqual(expectedCount, parameters.Count);
        }
        [TestMethod]
        public void 
            RegisterParametersCountValidation_ShouldThrowArgumentExWhenInvalidValuesPassed()
        {
            var sut = new Validator();

            Assert.ThrowsException<ArgumentException>(
                ()=> sut.RegisterParametersCountValidation(pramateresWithOneValue));
        }
        [TestMethod]
        public void
            RegisterParametersCountValidation_ShouldThrowCorrectExMsgWhenInvalidValuesPassed()
        {
            var sut = new Validator();

            var ex =  Assert.ThrowsException<ArgumentException>(
                () => sut.RegisterParametersCountValidation(pramateresWithOneValue));
            var expectedMsg = "To Register into the System you should enter Username and Password!";

            Assert.AreEqual(expectedMsg, ex.Message);
        }
        [TestMethod]
        public void TryParseToIntMethod_ShouldThrowArgumentExWhenInvalidValuePassed()
        {
            var sut = new Validator();
            Assert.ThrowsException<ArgumentException>(
                () => sut.TryParseToInt("Chislo"));
        }
        [TestMethod]
        public void TryParseToIntMethod_ShouldThrowCorrectExMsgWhenInvalidValuePassed()
        {
            var sut = new Validator();
            var ex = Assert.ThrowsException<ArgumentException>(
                () => sut.TryParseToInt("Chislo"));
            var expectedMsg = "Please, enter valid Number!";

            Assert.AreEqual(expectedMsg, ex.Message);
        }
        [TestMethod]
        public void TryParseToIntMethod_ShouldNotThrowWhenCorrectValuePassed()
        {
            var sut = new Validator();
            sut.TryParseToInt("5");
            var expected = 5;
            Assert.AreEqual(expected, 5);
        }
        [TestMethod]
        public void CommandNameIsLoginMethod_ShouldReturnTrueWhenLoginPassed()
        {
            var sut = new Validator();
            var result = sut.CommandNameIsLogin("login");
            Assert.AreEqual(true, result);
        }
        [TestMethod]
        public void CommandNameIsLoginMethod_ShouldReturnFalseWhenOtherPassed()
        {
            var sut = new Validator();
            var result = sut.CommandNameIsLogin("autoFack");
            Assert.AreEqual(false, result);
        }
        [TestMethod]
        public void CommandNameIsRegisterMethod_ShouldReturnFalseWhenOtherPassed()
        {
            var sut = new Validator();
            var result = sut.CommandNameIsRegister("autoFack");
            Assert.AreEqual(false, result);
        }
        [TestMethod]
        public void CommandNameIsRegisterMethod_ShouldReturnTrueWhenRegisterPassed()
        {
            var sut = new Validator();
            var result = sut.CommandNameIsRegister("register");
            Assert.AreEqual(true, result);
        }
        [TestMethod]
        public void SearchByYearParametersCountValidation_ShouldNotThrowWhenValidValuesPassed()
        {
            var sut = new Validator();
            sut.SearchByYearParametersCountValidation(pramateresWithOneValue);
            var expectedCount = 1;
            Assert.AreEqual(expectedCount, pramateresWithOneValue.Count);
        }
        [TestMethod]
        public void SearchByYearParametersCountValidation_ShouldThrowArgumentExWhenInvalidValuesPassed()
        {
            var sut = new Validator();

            Assert.ThrowsException<ArgumentException>(
                () => sut.SearchByYearParametersCountValidation(parameters));
        }
    }
}
