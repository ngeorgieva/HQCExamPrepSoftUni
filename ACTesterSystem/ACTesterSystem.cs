namespace AirConditionerTesterSystem
{
    using Execution;
    using Interfaces;
    using UI;

    public class ACTesterSystem
    {
        public static void Main()
        {
            IAirConditionerTesterSystemData data = new AirConditionerTesterSystemData();
            IController controller = new Controller(data);
            IDispatcher dispatcher = new Dispatcher(controller);
            IUserInterface userInterface = new ConsoleUserInterface();
            IEngine engine = new AirConditionerTesterSystemEngine(dispatcher, userInterface);
            engine.Run();
        }
    }
}