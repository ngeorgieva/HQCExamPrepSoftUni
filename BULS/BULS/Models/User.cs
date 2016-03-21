namespace BangaloreUniversityLearningSystem.Models
{
    using System;
    using System.Collections.Generic;
    using Enums;
    using Utilities;
    
    public class User
    {
        private string username;
        private string passwordHash;

        public User(string username, string password, Role role)
        {
            this.UserName = username;
            this.PasswordHash = password;
            this.Role = role;
            this.Courses = new HashSet<Course>();
        }

        public string UserName
        {
            get
            {
                return this.username;
            }

            set
            {
                if (string.IsNullOrEmpty(value) || value.Length < 5)
                {
                    throw new ArgumentException("The username must be at least 5 symbols long.");
                }

                this.username = value;
            }
        }

        public string PasswordHash
        {
            get
            {
                return this.passwordHash;
            }

            set
            {
                if (value.Length < 6)
                {
                    throw new ArgumentException("The password must be at least 6 symbols long.");
                }

                this.passwordHash = HashUtilities.HashPassword(value);
            }
        }

        public Role Role { get; private set; }

        public HashSet<Course> Courses { get; private set; }
    }
}
