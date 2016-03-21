namespace GitHubIssueTracker.Data
{
    using System.Collections.Generic;
    using Interfaces;
    using Models;
    using Wintellect.PowerCollections;

    public class GitHubIssueTrackerData : IGitHubIssueTrackerData
    {
        private int nextIssueId;

        public GitHubIssueTrackerData()
        {
            this.nextIssueId = 1;
            this.Users = new Dictionary<string, User>();

            this.Issues = new MultiDictionary<Issue, string>(true);
            this.IssuesById = new OrderedDictionary<int, Issue>();
            this.IssuesByUsername = new MultiDictionary<string, Issue>(true);
           //// Bug fixed: initialised IssuesByTags CommentsByUsers.
            this.IssuesByTags = new MultiDictionary<string, Issue>(true);
            this.CommentsByUsers = new MultiDictionary<User, Comment>(true);
            this.Comments = new Dictionary<Comment, User>();
        }

        public User LoggedInUser { get; set; }

        public MultiDictionary<Issue, string> Issues { get; set; }

        public Dictionary<Comment, User> Comments { get; set; }

        public IDictionary<string, User> Users { get; set; }

        public OrderedDictionary<int, Issue> IssuesById { get; set; }

        public MultiDictionary<string, Issue> IssuesByUsername { get; set; }

        public MultiDictionary<string, Issue> IssuesByTags { get; set; }

        public MultiDictionary<User, Comment> CommentsByUsers { get; set; }

        public int AddIssue(Issue issue)
        {
            issue.Id = this.nextIssueId;
            this.IssuesById.Add(issue.Id, issue);
            this.nextIssueId++;
            this.IssuesByUsername[this.LoggedInUser.Username].Add(issue);
            foreach (var tag in issue.Tags)
            {
                this.IssuesByTags[tag].Add(issue);
            }

            return issue.Id;
        }

        public void RemoveIssue(Issue issue)
        {
            this.IssuesByUsername[this.LoggedInUser.Username].Remove(issue);
            foreach (var tag in issue.Tags)
            {
                this.IssuesByTags[tag].Remove(issue);
            }

            this.IssuesById.Remove(issue.Id);
        }
    }
}