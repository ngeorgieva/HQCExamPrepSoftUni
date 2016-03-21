namespace GitHubIssueTracker.UserInterface
{
    using System;
    using Interfaces;

    public class ConsoleUserInterface : IUserInterface
    {
        public string ReadLine()
        {
            return Console.ReadLine();
        }

        public void WriteLine(string value, params object[] arguments)
        {
            Console.WriteLine(value, arguments);
        }
    }
}
