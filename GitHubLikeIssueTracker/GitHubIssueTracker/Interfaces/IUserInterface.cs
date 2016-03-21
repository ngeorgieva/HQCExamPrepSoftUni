namespace GitHubIssueTracker.Interfaces
{
    public interface IUserInterface
    {
        string ReadLine();

        void WriteLine(string value, params object[] arguments);
    }
}
