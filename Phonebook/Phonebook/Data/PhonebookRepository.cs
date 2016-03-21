namespace Phonebook.Data
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Linq;
    using Interfaces;

    public class PhonebookRepository : IPhonebookRepository
    {
        private readonly SortedDictionary<string, IPhonebookEntry> contacts;

        private readonly IPhonebookFactory factory;

        public PhonebookRepository(IPhonebookFactory factory)
        {
            this.factory = factory;
            this.contacts = new SortedDictionary<string, IPhonebookEntry>();
        }

        public IEnumerable<KeyValuePair<string, IPhonebookEntry>> Contacts => this.contacts;

        public bool AddPhone(string name, IEnumerable<string> numbers)
        {
            bool resultMessage = false;
            var key = name.ToLowerInvariant();
            if (!this.contacts.ContainsKey(key))
            {
                var entry = this.factory.CreatePhonebookEntry(name, numbers);
                this.contacts.Add(key, entry);
                resultMessage = true;
            }
            else
            {
                foreach (string number in numbers)
                {
                    if (this.contacts[key].PhoneNumbers.Contains(number))
                    {
                        resultMessage = false;
                    }
                    else
                    {
                        this.contacts[key].PhoneNumbers.Add(number);
                        resultMessage = false;
                    }
                }
            }

            return resultMessage;
        }

        public int ChangePhone(string oldPhoneNumber, string newPhoneNumber)
        {
            IList<IPhonebookEntry> entriesTochange = (
                from entry in this.contacts
                where entry.Value.PhoneNumbers.Contains(oldPhoneNumber)
                select entry.Value)
                .ToList();

            foreach (var entry in entriesTochange)
            {
                entry.PhoneNumbers.Remove(oldPhoneNumber);
                entry.PhoneNumbers.Add(newPhoneNumber);
            }

            return entriesTochange.Count;
        }

        public IPhonebookEntry[] ListEntries(int first, int num)
        {
            if (first < 0 || first + num > this.contacts.Count)
            {
                throw new ArgumentException("Invalid range");
            }
            else
            {
                var entires = new HashSet<IPhonebookEntry>();

                for (var i = first; i < first + num; i++)
                {
                    var entry = this.contacts.ElementAt(i);
                    entires.Add(entry.Value);
                }

                return entires.ToArray();
            }
        }

        // Wrote this method for better unit testing
        public int PhonesNumbersCount()
        {
            return this.Contacts.Sum(contact => contact.Value.PhoneNumbers.Count);
        }
    }
}