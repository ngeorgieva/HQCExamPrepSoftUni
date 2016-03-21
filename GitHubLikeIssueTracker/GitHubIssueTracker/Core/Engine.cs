namespace GitHubIssueTracker.Core
{
    using System;
    using Data;
    using Interfaces;
    using UserInterface;

    public class Engine : IEngine
    {
        private readonly ActionDispatcher dispatcher;
        private IUserInterface userInterface;

        public Engine(ActionDispatcher dispatcher, IUserInterface userInterface)
        {
            this.dispatcher = dispatcher;
            this.userInterface = userInterface;
        }

        public Engine()
            : this(new ActionDispatcher(), new ConsoleUserInterface())
        {
        }

        public void Run()
        {
            while (true)
            {
                var url = this.userInterface.ReadLine();
                //// Bug fixed: changed from url != null to url == null.
                if (url == null)
                {
                    break;
                }

                url = url.Trim();

                //// Bug fixed: changed from url != null to url == null.
                if (!string.IsNullOrEmpty(url))
                {
                    try
                    {
                        var endpoint = new Endpoint(url);
                        var viewResult = this.dispatcher.DispatchAction(endpoint);
                        this.userInterface.WriteLine(viewResult);
                    }
                    catch (Exception ex)
                    {
                        this.userInterface.WriteLine(ex.Message);
                    }
                }
            }
        }
    }
}