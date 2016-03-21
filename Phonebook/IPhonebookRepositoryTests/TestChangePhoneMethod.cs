namespace Phonebook.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Data;
    using Execution;
    using Interfaces;

    [TestClass]
    public class TestChangePhoneMethod
    {
        private IPhonebookRepository repository;
        private IPhonebookFactory factory;

        [TestInitialize]
        public void SetUp()
        {
            this.factory = new PhonebookFactory();
            this.repository = new PhonebookRepository(this.factory);
            this.repository.AddPhone("Nakov", new string[] { "+359887333444" });
            this.repository.AddPhone("Minka", new string[] { "+359887333544" });
            this.repository.AddPhone("Ginka", new string[] { "+359887333774" });
            this.repository.AddPhone("Nakov", new string[] { "+359887333454" });
            this.repository.AddPhone("Nakov", new string[] { "+359887333434" });
            this.repository.AddPhone("Gichka", new string[] { "+359887333444" });
        }

        [TestMethod]
        public void TestChangePhone_CorrectInput_ShouldReturnCorrectCount()
        {
            var result = this.repository.ChangePhone("+359887333444", "+359888777666");

            Assert.AreEqual(2, result);
        }

        [TestMethod]
        public void TestChangePhone_NonExistantPhone_ShoulReturn0()
        {
            var result = this.repository.ChangePhone("+3591254875", "+359887333444");

            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void TestChangePhone_PhoneWithMerge_ShoulReturn0()
        {
            this.repository.AddPhone("Nakov", new string[] { "+359887333555", "+359887333999" });
            this.repository.AddPhone("Ina", new string[] { "+359887333999" });
            this.repository.AddPhone("Ani", new string[] { "+359887333555", "359887333444" });
            int changedPhonesCount = this.repository.ChangePhone("+359887333999", "+359887333555");
            Assert.AreEqual(2, changedPhonesCount);
        }


    }
}
