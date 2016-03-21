namespace GitHubIssueTracker.Core
{
    using System;
    using Interfaces;
    using Models;

    public class ActionDispatcher
    {
        public ActionDispatcher() : this(new IssueTracker())
        {
        }

        private ActionDispatcher(IIssueTracker tracker)
        {
            this.Tracker = tracker;
        }

        private IIssueTracker Tracker { get; }

        public string DispatchAction(IEndpoint endpoint)
        {
            switch (endpoint.ActionName)
            {
                case "RegisterUser":
                    return this.Tracker.RegisterUser(
                        endpoint.Parameters["username"],
                        endpoint.Parameters["password"],
                        endpoint.Parameters["confirmPassword"]);
                case "LoginUser":
                    return this.Tracker.LoginUser(
                        endpoint.Parameters["username"],
                        endpoint.Parameters["password"]);
                case "LogoutUser":
                    return this.Tracker.LogoutUser();
                case "CreateIssue":
                    return this.Tracker.CreateIssue(
                        endpoint.Parameters["title"],
                        endpoint.Parameters["description"],
                        (IssuePriority)Enum.Parse(typeof(IssuePriority), endpoint.Parameters["priority"], true),
                        //// change to / if not working
                        endpoint.Parameters["tags"].Split('|'));
                case "RemoveIssue":
                    return this.Tracker.RemoveIssue(int.Parse(endpoint.Parameters["id"]));
                case "AddComment":
                    //// Bug fixed: changed to id, instead of Id.
                    return this.Tracker.AddComment(
                        int.Parse(endpoint.Parameters["id"]),
                        endpoint.Parameters["text"]);
                case "MyIssues":
                    return this.Tracker.GetMyIssues();
                case "MyComments":
                    return this.Tracker.GetMyComments();
                case "Search":
                    //// Bug fixed: changed to | instead of / 
                    return this.Tracker.SearchForIssues(endpoint.Parameters["tags"].Split('|'));
                default:
                    {
                        throw new InvalidOperationException(string.Format("Invalid action: {0}", endpoint.ActionName));
                    }
            }
        }
    }
}