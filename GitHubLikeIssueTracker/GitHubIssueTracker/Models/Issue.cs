namespace GitHubIssueTracker.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Utilities;

    public class Issue
    {
        private string description;
        private string title;

        public Issue(string title, string description, IssuePriority priority, List<string> tags)
        {
            this.Title = title;
            this.Description = description;
            this.Priority = priority;
            this.Tags = tags;
            this.Comments = new List<Comment>();
        }

        public int Id { get; set; }

        public string Title
        {
            get
            {
                return this.title;
            }

            set
            {
                if (string.IsNullOrEmpty(value) || value.Length < Constants.MinimalIssueTitleLength)
                {
                    throw new ArgumentException(string.Format("The title must be at least {0} symbols long", Constants.MinimalIssueTitleLength));
                }

                this.title = value;
            }
        }

        public string Description
        {
            get
            {
                return this.description;
            }

            set
            {
                if (string.IsNullOrEmpty(value) || value.Length < Constants.MinimalIssueDescriptionLength)
                {
                    throw new ArgumentException(string.Format("The description must be at least {0} symbols long", Constants.MinimalIssueDescriptionLength));
                }

                this.description = value;
            }
        }

        public IssuePriority Priority { get; set; }

        public IList<string> Tags { get; set; }

        public IList<Comment> Comments { get; set; }

        public void AddComment(Comment comment)
        {
            this.Comments.Add(comment);
        }

        public override string ToString()
        {
            var issue = new StringBuilder();
            issue
                .AppendLine(this.Title)
                .AppendFormat("Priority: {0}", this.GetPriorityAsString()).AppendLine()
                .AppendLine(this.Description);
            if (this.Tags.Count > 0)
            {
                issue.AppendFormat("Tags: {0}", string.Join(",", this.Tags.OrderBy(t => t))).AppendLine();
            }

            if (this.Comments.Count > 0)
            {
                issue.AppendFormat("Comments:{0}{1}", Environment.NewLine, string.Join(Environment.NewLine, this.Comments)).AppendLine();
            }

            return issue.ToString().Trim();
        }

        private void AppendComments(StringBuilder issue)
        {
            if (this.Comments.Count > 0)
            {
                string allComments = string.Join(Environment.NewLine, this.Comments);
                issue.AppendFormat(
                    "Comments:{0}{1}", 
                    Environment.NewLine, 
                    this.Comments)
                    .AppendLine();
            }
        }

        private void AppendTags(StringBuilder issue)
        {
            if (this.Tags.Count > 0)
            {
                issue.AppendFormat("Tags: {0}", string.Join(",", this.Tags.OrderBy(t => t))).AppendLine();
            }
        }

        private string GetPriorityAsString()
        {
            switch (this.Priority)
            {
                case IssuePriority.Showstopper:
                    return "****";
                case IssuePriority.High:
                    return "***";
                case IssuePriority.Medium:
                    return "**";

                case IssuePriority.Low:
                    return "*";
                default:
                    throw new InvalidOperationException("The priority is invalid");
            }
        }
    }
}