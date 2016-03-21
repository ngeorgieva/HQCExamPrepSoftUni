namespace GitHubIssueTracker.Models
{
    using Utilities;

    public class User
    {
        public User(string username, string password)
        {
            this.Username = username;
            this.PasswordHash = HashUtility.HashPassword(password);
        }

        public string Username { get; set; }

        public string PasswordHash { get; set; }   
    }
}