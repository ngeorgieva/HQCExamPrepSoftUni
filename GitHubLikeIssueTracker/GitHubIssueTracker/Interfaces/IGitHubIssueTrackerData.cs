namespace GitHubIssueTracker.Interfaces
{
    using System.Collections.Generic;
    using Models;
    using Wintellect.PowerCollections;

    public interface IGitHubIssueTrackerData
    {
        User LoggedInUser { get; set; }

        IDictionary<string, User> Users { get; }

        OrderedDictionary<int, Issue> IssuesById { get; }

        MultiDictionary<string, Issue> IssuesByUsername { get; }

        MultiDictionary<string, Issue> IssuesByTags { get; }

        MultiDictionary<User, Comment> CommentsByUsers { get; }

        int AddIssue(Issue issue);

        void RemoveIssue(Issue issue);
    }
}