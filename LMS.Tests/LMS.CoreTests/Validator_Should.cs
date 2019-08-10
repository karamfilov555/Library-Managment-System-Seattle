using LMS.Core.IO;
using LMS.Core.Utils;
using LMS.Models;
using LMS.Models.ModelsContracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace LMS.Tests.LMS.CoreTests
{
    [TestClass]
    public class Validator_
    {
        List<string> parameters = new List<string> { "dddd", "dasdada" };
        List<string> invalidParameters = new List<string> { "dddd"};

        [TestMethod]
        public void Constructor_ShouldMakeInstanceOfValidator()
        {
            var sut = new Validator();
            Assert.IsInstanceOfType(sut, typeof(Validator));
        }
        [TestMethod]
        public void IsParametersCountIsValid_ShouldThrowArgumentExWhenParamsAreInvalid()
        {
            var sut = new Validator();

            Assert.ThrowsException<ArgumentException>(
                () => sut.IsParametersCountIsValid(parameters, 1));
        }
        [TestMethod]
        public void IsParametersCountIsValid_ShouldThrowCorrectArgumentExMessage()
        {
            var sut = new Validator();
            var expectedMsg = "Parameters count is not valid!";
            var ex = Assert.ThrowsException<ArgumentException>(
                () => sut.IsParametersCountIsValid(parameters, 1));
            Assert.AreEqual(expectedMsg, ex.Message);
        }
        [TestMethod]
        public void IsParametersCountIsValid_ShouldNotThrowWhenValidValuesPassed()
        {
            var sut = new Validator();
            sut.IsParametersCountIsValid(parameters, 2);
            var expectedCount = 2;
            Assert.AreEqual(expectedCount, parameters.Count);
        }
        [TestMethod]
        public void LoginParametersCountValidation_ShouldThrowArgumentExWhenParamsAreInvalid()
        {
            var sut = new Validator();

            Assert.ThrowsException<ArgumentException>(
                () => sut.LoginParametersCountValidation(invalidParameters));
        }
        [TestMethod]
        public void LoginParametersCountValidation_ShouldThrowCorrectArgumentExMessage()
        {
            var sut = new Validator();

            var ex = Assert.ThrowsException<ArgumentException>(
                () => sut.LoginParametersCountValidation(invalidParameters));
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
                ()=> sut.RegisterParametersCountValidation(invalidParameters));
        }
        [TestMethod]
        public void
            RegisterParametersCountValidation_ShouldThrowCorrectExMsgWhenInvalidValuesPassed()
        {
            var sut = new Validator();

            var ex =  Assert.ThrowsException<ArgumentException>(
                () => sut.RegisterParametersCountValidation(invalidParameters));
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
    }
}
