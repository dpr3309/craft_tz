namespace Craft_TZ.Shared.FSM
{
    public class EventArgs
    {
        public string Id { get; }

        public EventArgs(string id)
        {
            Id = id;
        }
    }
}

