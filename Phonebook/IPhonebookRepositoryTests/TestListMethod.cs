namespace Phonebook.Tests
{
    using System;
    using Data;
    using Execution;
    using Interfaces;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class TestListMethod
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
        public void TestListMethod_GetSingleEntry_ShouldReturnEntry()
        {
            IPhonebookEntry[] entries = this.repository.ListEntries(2, 1);
            string result = string.Empty;
            foreach (var entry in entries)
            {
                result += entry.ToString();
            }

            Assert.AreEqual(1, entries.Length);
            Assert.AreEqual("[Minka: +359887333544]", result);
        }

        [TestMethod]
        public void TestListMethod_SingleEntryMultiplePhoneNums_ShouldReturnEntry()
        {
            IPhonebookEntry[] entries = this.repository.ListEntries(3, 1);
            string result = string.Empty;
            foreach (var entry in entries)
            {
                result += entry.ToString();
            }

            Assert.AreEqual(1, entries.Length);
            Assert.AreEqual("[Nakov: +359887333434, +359887333444, +359887333454]", result);
        }

        [TestMethod]
        public void TestListMethod_GetMultipleEntry_ShouldReturnEntriesWithSorting()
        {
            IPhonebookEntry[] entries = this.repository.ListEntries(1, 3);
            string result = string.Empty;
            foreach (var entry in entries)
            {
                result += entry.ToString();
                result += " ";
            }

            Assert.AreEqual(3, entries.Length);
            Assert.AreEqual("[Ginka: +359887333774] [Minka: +359887333544] [Nakov: +359887333434, +359887333444, +359887333454]", result.Trim());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestListMethod_InvalidArgs_ShouldThrow()
        {
            this.repository.ListEntries(2, 20);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestListMethod_NegativeStartIndex_ShouldThrow()
        {
            this.repository.ListEntries(2, 20);
        }
    }
}
