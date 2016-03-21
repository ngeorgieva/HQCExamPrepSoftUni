namespace Phonebook.Interfaces
{
    using System.Collections.Generic;

    public interface IPhonebookFactory
    {
        IPhonebookEntry CreatePhonebookEntry(string name, IEnumerable<string> numbers);
    }
}
