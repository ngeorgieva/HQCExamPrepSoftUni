namespace GitHubIssueTracker
{
    using System.Globalization;
    using System.Threading;
    using Core;

    public class Program
    {
        private static void Main()
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;

            var engine = new Engine();
            engine.Run();
        }
    }
}