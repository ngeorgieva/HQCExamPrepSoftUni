namespace Phonebook.Tests
{
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Phonebook.Data;
    using Phonebook.Execution;
    using Phonebook.Interfaces;

    [TestClass]
    public class TestAddPhoneMethod
    {
        private IPhonebookRepository repository;
        private IPhonebookFactory factory;

        [TestInitialize]
        public void SetUp()
        {
            this.factory = new PhonebookFactory();
            this.repository = new PhonebookRepository(this.factory);
        }

        [TestMethod]
        public void TestAddPhone_SingleEntry_ShouldReturnCorrectNumberOfEntries()
        {
            this.repository.AddPhone("Nakov", new string[] { "+359887333444" });

            Assert.AreEqual(1, this.repository.Contacts.Count());
        }

        [TestMethod]
        public void TestAddPhone_MultipleEntries_ShouldReturnCorrectNumberOfEntries()
        {
            this.repository.AddPhone("Nakov", new string[] {"+359887333444"});
            this.repository.AddPhone("Minka", new string[] { "+359887333544" });
            this.repository.AddPhone("Ginka", new string[] { "+359887333774" });

            Assert.AreEqual(3, this.repository.Contacts.Count());
        }

        [TestMethod]
        public void TestAddPhone_DuplicateEntries_ShouldMergeThem()
        {
            this.repository.AddPhone("Nakov", new string[] { "+359887333444" });
            this.repository.AddPhone("Nakov", new string[] { "+359887333444" });

            Assert.AreEqual(1, this.repository.Contacts.Count());
            Assert.AreEqual(1, this.repository.PhonesNumbersCount());
        }

        [TestMethod]
        public void TestAddPhone_DifferentCaseing_ShoudReturnCorrectCount()
        {
            bool isNew = this.repository.AddPhone("Nakov", new string[] { "+359887333444" });
            Assert.AreEqual(true, isNew);
            isNew = this.repository.AddPhone("NAKOV", new string[] { "+359887333555" });
            Assert.AreEqual(false, isNew);
            isNew = this.repository.AddPhone("nakov", new string[] { "+359887333777" });
            Assert.AreEqual(false, isNew);
            Assert.AreEqual(1, this.repository.Contacts.Count());
            Assert.AreEqual(3, this.repository.PhonesNumbersCount());
        }

        [TestMethod]
        public void TestAddPhone_ManyFormats_ShouldConvertToCanonical()
        {
            this.repository.AddPhone("Nakov", new string[] { "+359887333555" });
            this.repository.AddPhone("Nakov", new string[] { "0887 333 555" });
            this.repository.AddPhone("Nakov", new string[] { "0887 33 35 55" });
            this.repository.AddPhone("Nakov", new string[] { "+359 887 33 35 55" });
            this.repository.AddPhone("Nakov", new string[] { "(+359) 887 33 35 55" });
            this.repository.AddPhone("Nakov", new string[] { "(+359) 887 333-555" });
            this.repository.AddPhone("Nakov", new string[] { "0887 / 33 35 55" });
            Assert.AreEqual(1, this.repository.Contacts.Count());
            Assert.AreEqual(7, this.repository.PhonesNumbersCount());
        }
    }
}
