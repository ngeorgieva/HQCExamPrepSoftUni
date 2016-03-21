namespace Phonebook.Execution
{
    using System.Collections.Generic;
    using Interfaces;
    using Models;

    public class PhonebookFactory : IPhonebookFactory
    {
        public IPhonebookEntry CreatePhonebookEntry(string name, IEnumerable<string> numbers)
        {
            return new PhonebookEntry(name, numbers);
        }
    }
}
