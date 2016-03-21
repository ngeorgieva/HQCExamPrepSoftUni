namespace Phonebook.Interfaces
{
    using System.Collections.Generic;

    public interface IPhonebookEntry
    {
        string Name { get; }

        ICollection<string> PhoneNumbers { get; }

        int CompareTo(IPhonebookEntry other);
    }
}
