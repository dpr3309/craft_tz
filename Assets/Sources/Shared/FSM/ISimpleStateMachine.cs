namespace Craft_TZ.Shared.FSM
{
    public interface ISimpleStateMachine
    {
        void Event(EventArgs args);
        void Add(IState state);
        void Start<T>() where T : IState;
    }
}

