namespace Phonebook.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Interfaces;

    public class PhonebookEntry : IComparable<IPhonebookEntry>, IPhonebookEntry
    {
        private string name;
        private HashSet<string> phoneNumbers;

        public PhonebookEntry(string name, IEnumerable<string> phoneNums)
        {
            this.Name = name;
            this.phoneNumbers = new HashSet<string>();
            foreach (string number in phoneNums)
            {
                this.phoneNumbers.Add(number);
            }
        }

        public string Name
        {
            get
            {
                return this.name;
            }

            set
            {
                if (string.IsNullOrEmpty(value) 
                    || value.Length > 200 
                    || value.Contains(",") 
                    || value.Contains(":") 
                    || value.Contains("\n"))
                {
                    throw new ArgumentException(
                        @"The name cannot be empty, cannot be longer than 200 symbols or contain ',', ':', '\n'");
                }

                this.name = value;
            }
        }

        public ICollection<string> PhoneNumbers => this.phoneNumbers;

        public int CompareTo(IPhonebookEntry other)
        {
            return string.Compare(this.Name.ToLowerInvariant(), other.Name.ToLowerInvariant(), StringComparison.Ordinal);
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append('[');
            sb.Append(this.Name);
            bool isName = true;
            var sortedPhoneNumbers = this.PhoneNumbers.OrderBy(c => c);

            foreach (var phoneNumber in sortedPhoneNumbers)
            {
                if (isName)
                {
                    sb.Append(": ");
                    isName = false;
                }
                else
                {
                    sb.Append(", ");
                }

                sb.Append(phoneNumber);
            }

            sb.Append(']');

            return sb.ToString();
        }
    }
}