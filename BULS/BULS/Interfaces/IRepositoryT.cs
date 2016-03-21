namespace BangaloreUniversityLearningSystem.Interfaces
{
    using System.Collections.Generic;

    /// <summary>
    /// An interface classes for storing and retrieving collections of items.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IRepository<T>
    {
        /// <summary>
        /// Gets all items in a collection.
        /// </summary>
        /// <returns></returns>
        IEnumerable<T> GetAll();

        /// <summary>
        /// Gets an items by its ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        T Get(int id);

        /// <summary>
        /// Adds an item to a collection.
        /// </summary>
        /// <param name="item"></param>
        void Add(T item);
    }
}
