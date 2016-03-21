namespace Tests
{
    using AirConditionerTesterSystem;
    using AirConditionerTesterSystem.Execution;
    using AirConditionerTesterSystem.Interfaces;
    using AirConditionerTesterSystem.Models;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class FindAllReportsByManufacturerTests
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
        public void TestFindAllReportsByManufacturer_NoReports_ShoudlReturnNoReportMessage()
        {
            this.controller.RegisterStationaryAirConditioner("Toshiba", "AXN100", 'A', 777);
            var message = this.controller.FindAllReportsByManufacturer("Toshiba");
            Assert.AreEqual("No reports.", message);
        }

        [TestMethod]
        public void TestFindAllReportsByManufacturer_ValidReports_ShoudlReturnReports()
        {
            this.controller.RegisterStationaryAirConditioner("Toshiba", "EX1000", 'B', 1000);
            this.controller.RegisterStationaryAirConditioner("Toshiba", "WH70", 'A', 780);
            this.controller.TestAirConditioner("Toshiba", "EX1000");
            this.controller.TestAirConditioner("Toshiba", "WH70");
            var result = this.controller.FindAllReportsByManufacturer("Toshiba");
            var expectedResult = "Reports from Toshiba:\r\nReport\r\n====================\r\nManufacturer: Toshiba\r\nModel: EX1000\r\nMark: Passed\r\n====================\r\nReport\r\n====================\r\nManufacturer: Toshiba\r\nModel: WH70\r\nMark: Passed\r\n====================";

            Assert.AreEqual(expectedResult, result);
        }
    }
}
