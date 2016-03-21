namespace Phonebook.Interfaces
{
    using System.Collections.Generic;

    public interface IPhonebookRepository
    {
        /// <summary>
        /// Gets a collection of entries - name and one or more phone numbers.
        /// </summary>
        IEnumerable<KeyValuePair<string, IPhonebookEntry>> Contacts { get; }

        /// <summary>
        /// Adds a non-existing entry to the phonebook or a non-existing number to an existing entry.
        /// </summary>
        /// <param name="name">The name associated with the phone number.It is case-insensitive non-empty string</param>
        /// <param name="numbers">The phone number to be added.</param>
        /// <returns>Returns trues if a new entry was created and 
        /// false if an entry was existing and the phone numbers were merged.</returns>
        /// <remarks>When merging an existing name with a new name, the name remains in
        /// the same character casing as the existing name in the repository and the
        /// duplicated phone numbers are removed. The phone numbers for each name are
        /// sorted in alphabetical order as simple text.</remarks>
        bool AddPhone(string name, IEnumerable<string> numbers);

        /// <summary>
        /// Changes the specified old phone number in all phonebook entries with a new one.
        /// </summary>
        /// <param name="oldPhoneNumber">The phone number that is to be cnahged.</param>
        /// <param name="newPhoneNumber">The new phone number to replace the old one.</param>
        /// <returns>The number of changes made.</returns>
        /// <remarks>Changing a phone number works with merging and thus any duplicating phone numbers are omitted.</remarks>
        int ChangePhone(string oldPhoneNumber, string newPhoneNumber);

        /// <summary>
        /// Lists the phonebook entries with paging. The page is specified by start index and count in the phonebook assuming that the entries are sorted by name (case-insensitive).
        /// </summary>
        /// <param name="startIndex">The start index of the entries in the repository. It is zero-based.</param>
        /// <param name="count">The count specifies the page size (the number of phonebook entries to be retrieved).</param>
        /// <returns>An array of the listed entries if successful and an error message in case the start index or the count is invalid.</returns>
        IPhonebookEntry[] ListEntries(int startIndex, int count);

        /// <summary>
        /// Created for making unit testing easier and better.
        /// Gets the number of all phone contacts in the repository.
        /// </summary>
        /// <returns></returns>
        int PhonesNumbersCount();
    }
}