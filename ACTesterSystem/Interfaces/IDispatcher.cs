namespace AirConditionerTesterSystem.Interfaces
{
    public interface IDispatcher
    {
        string DispatchAction(IAction command);
    }
}
