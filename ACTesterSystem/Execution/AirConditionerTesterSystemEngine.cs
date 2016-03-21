namespace AirConditionerTesterSystem
{
    using System;
    using Interfaces;

    public class AirConditionerTesterSystemEngine : IEngine
    {
        private IDispatcher dispatcher;
        private IUserInterface userInterface;

        public AirConditionerTesterSystemEngine(IDispatcher dispatcher, IUserInterface userInterface)
        {
            this.dispatcher = dispatcher;
            this.userInterface = userInterface;
        }

        public IAction Action { get; private set; }

        public void Run()
        {
            while (true)
            {
                var input = this.userInterface.ReadLine();
                if (string.IsNullOrWhiteSpace(input))
                {
                    break;
                }

                input = input.Trim();
                var actionResult = string.Empty;
                try
                {
                    this.Action = new Execution.Action(input);
                    actionResult = this.dispatcher.DispatchAction(this.Action);
                    this.userInterface.WriteLine(actionResult);
                }
                catch (Exception ex)
                {
                    this.userInterface.WriteLine(ex.Message);
                }
            }
        }
    }
}