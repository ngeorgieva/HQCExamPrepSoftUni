namespace GitHubIssueTracker.Models
{
    using System;
    using System.Text;
    using Utilities;

    public class Comment
    {
        private string text;

        public Comment(User author, string text)
        {
            this.Author = author;
            this.Text = text;
        }

        public User Author { get; set; }

        public string Text
        {
            get
            {
                return this.text;
            }

            set
            {
                if (string.IsNullOrEmpty(value) || value.Length < Constants.MinimalCommentTextLength)
                {
                    throw new ArgumentException(string.Format("The text must be at least {0} symbols long", Constants.MinimalCommentTextLength));
                }

                this.text = value;
            }
        }

        public override string ToString()
        {
            var comment = new StringBuilder();
            comment
                .AppendLine(this.Text)
                .AppendFormat("-- {0}", this.Author.Username)
                .AppendLine();
            return comment.ToString().Trim();
        }
    }
}