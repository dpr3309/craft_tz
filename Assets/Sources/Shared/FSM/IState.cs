using System;

namespace Craft_TZ.Shared.FSM
{
    public interface IState
    {
        void OnEnter();
        Type Event(EventArgs args);
        void OnExit();
    }
}

