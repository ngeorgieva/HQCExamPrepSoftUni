namespace BangaloreUniversityLearningSystem.Data
{
    using System.Collections.Generic;
    using Models;

    /// <summary>
    /// A collection of users.
    /// </summary>
    public class UsersRepository : Repository<User>
    {
        private Dictionary<string, User> usersByUsername;

        public UsersRepository()
        {
            this.usersByUsername = new Dictionary<string, User>();
        }

        /// <summary>
        /// Determines whether the collection of users contains a user with the specified username.
        /// </summary>
        /// <param name="username"></param>
        /// <returns>the user with the username; otherwise, null</returns>
        public User GetByUsername(string username)
        {
            return this.usersByUsername.ContainsKey(username) ? this.usersByUsername[username] : null;
        }

        /// <summary>
        /// Adds a user to the collection.
        /// </summary>
        /// <param name="item"></param>
        public override void Add(User item)
        {
            this.usersByUsername.Add(item.UserName, item);
            base.Add(item);
        }
    }
}
