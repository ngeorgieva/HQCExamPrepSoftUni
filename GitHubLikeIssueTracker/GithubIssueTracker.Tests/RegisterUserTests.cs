namespace GithubIssueTracker.Tests
{
    using System.Linq;
    using GitHubIssueTracker.Core;
    using GitHubIssueTracker.Utilities;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class RegisterUserTests
    {
        [TestMethod]
        public void Test_RegisterUser_ShouldRegisterUser()
        {
            string username = "validUsername";
            string password = "validPassword";

            var tracker = new IssueTracker();
            string viewResult = tracker.RegisterUser(username, password, password);

            Assert.AreEqual("User validUsername registered successfully", viewResult);
            Assert.AreEqual(1, tracker.Data.Users.Count);
            var user = tracker.Data.Users.First().Value;
            Assert.AreEqual(username, user.Username);
            Assert.AreEqual(HashUtility.HashPassword(password), user.PasswordHash);
        }

        [TestMethod]
        public void Test_RegisterUserWithDifferentPasswords_ShouldReturnErrorMessage()
        {
            string username = "validUsername";
            string password = "validPassword";

            var tracker = new IssueTracker();
            string viewResult = tracker.RegisterUser(username, password, "invalidPasswprd");

            Assert.AreEqual("The provided passwords do not match", viewResult);
        }

        [TestMethod]
        public void Test_RegisterUserWithRepeatingUsername_ShouldRegisterUser()
        {
            string username = "validUsername";
            string password = "validPassword";

            var tracker = new IssueTracker();
            tracker.RegisterUser(username, password, password);

            string viewResult = tracker.RegisterUser(username, password, password);
            Assert.AreEqual("A user with username validUsername already exists", viewResult);
            Assert.AreEqual(1, tracker.Data.Users.Count);
            var user = tracker.Data.Users.First().Value;
            Assert.AreEqual(username, user.Username);
            Assert.AreEqual(HashUtility.HashPassword(password), user.PasswordHash);
        }

        [TestMethod]
        public void Test_RegisterUserWithLoggedInUser_ShouldRegisterUser()
        {
            string username = "validUsername";
            string password = "validPassword";

            var tracker = new IssueTracker();
            tracker.RegisterUser(username, password, password);
            tracker.LoginUser(username, password);
            string otherUsername = "otherUsername";
            string viewResult = tracker.RegisterUser(otherUsername, password, password);
            Assert.AreEqual("There is already a logged in user", viewResult);
        }
    }
}
