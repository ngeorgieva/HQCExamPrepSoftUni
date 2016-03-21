namespace Tests
{
    using System;
    using AirConditionerTesterSystem;
    using AirConditionerTesterSystem.Exceptions;
    using AirConditionerTesterSystem.Execution;
    using AirConditionerTesterSystem.Interfaces;
    using AirConditionerTesterSystem.Utilities;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class RegisterStationaryACsTests
    {
        private IAirConditionerTesterSystemData data;
        private IController controller;

        [TestInitialize]
        public void SetUp()
        {
            this.data = new AirConditionerTesterSystemData();
            this.controller = new Controller(this.data);
        }

        [TestMethod]
        public void RegisterStationaryAC_ValidInput_ShoudReturnSuccessMessage()
        {
            var message = this.controller.RegisterStationaryAirConditioner("Toshiba", "AXN100", 'A', 777);
            
            Assert.AreEqual("Air Conditioner model AXN100 from Toshiba registered successfully.", message);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentException))]
        public void RegisterStationaryAC_InvalidEnergyEfficientRating_ThrowException()
        {
            this.controller.RegisterStationaryAirConditioner("Toshiba", "AXN100", 'N', 777);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void RegisterStationaryAC_ShortManufacturerName_ThrowException()
        {
            this.controller.RegisterStationaryAirConditioner("T", "AXN100", 'A', 777);
        }

        [TestMethod]
        public void RegisterStationaryAC_ShortManufacturerName_TestErrorMessage()
        {
            try
            {
                this.controller.RegisterStationaryAirConditioner("T", "AXN100", 'A', 777);
            }
            catch (ArgumentException ex)
            {
                Assert.AreEqual($"Manufacturer's name must be at least {Constants.ManufacturerMinLength} symbols long.", ex.Message);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void RegisterStationaryAC_ShortModelName_ThrowException()
        {
            this.controller.RegisterStationaryAirConditioner("Toshiba", "A", 'B', 777);
        }

        [TestMethod]
        public void RegisterStationaryAC_ShortModelName_ShouldReturnCorrectErrorMessage()
        {
            try
            {
                this.controller.RegisterStationaryAirConditioner("Toshiba", "A", 'B', 777);
            }
            catch (ArgumentException ex)
            {
                Assert.AreEqual($"Model's name must be at least {Constants.ModelMinLength} symbols long.", ex.Message);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void RegisterStationaryAC_ZeroPowerUsage_ThrowException()
        {
            this.controller.RegisterStationaryAirConditioner("Toshiba", "A", 'B', 0);
        }

        [TestMethod]
        public void RegisterStationaryAc_WithIncorrectPowerUsage_ShouldThrowCorrectExceptionMessage()
        {
            try
            {
                this.controller.RegisterStationaryAirConditioner("Toshiba", "EX100", 'B', -50);
            }
            catch (ArgumentException ex)
            {
                Assert.AreEqual("Power Usage must be a positive integer.", ex.Message, "Expected message did not match!");
            }
        }

        [TestMethod]
        [ExpectedException(typeof(DuplicateEntryException))]
        public void RegisterStationaryAC_ACAlreadyRegistered_ThrowException()
        {
            this.controller.RegisterStationaryAirConditioner("Toshiba", "AXN100", 'A', 777);
            this.controller.RegisterStationaryAirConditioner("Toshiba", "AXN100", 'A', 777);
        }

        [TestMethod]
        public void RegisterStationaryAC_ACAlreadyRegistered_TestErrorMessage()
        {
            try
            {
                this.controller.RegisterStationaryAirConditioner("Toshiba", "AXN100", 'A', 777);
                this.controller.RegisterStationaryAirConditioner("Toshiba", "AXN100", 'A', 777);
            }
            catch (DuplicateEntryException ex)
            {
               Assert.AreEqual("An entry for the given model already exists.", ex.Message);
            }
        }
    }
}
