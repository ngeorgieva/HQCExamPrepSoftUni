namespace AirConditionerTesterSystem.Interfaces
{
    public interface IEngine
    {
        IAction Action { get; }

        void Run();
    }
}
