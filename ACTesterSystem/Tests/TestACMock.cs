namespace Tests
{
    using AirConditionerTesterSystem.Interfaces;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;

    [TestClass]
    public class TestACMock
    {
        [TestMethod]
        public void TestAc_ValidData_ShouldReturnSuccessMessage()
        {
            var mockData = new Mock<IAirConditionerTesterSystemData>();
        }
    }
}
