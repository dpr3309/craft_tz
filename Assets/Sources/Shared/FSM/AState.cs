using System;
using UnityEngine;

namespace Craft_TZ.Shared.FSM
{
    public abstract class AState : IState
    {
        public virtual void OnEnter()
        {
            LogStateName();
        }

        protected virtual void LogStateName()
        {
            Debug.Log($"---S_FMS----{GetType().Name}---S_FMS---");
        }

        public virtual void OnExit() { }

        public virtual Type Event(EventArgs args)
        {
            return GetType();
        }
    }
}

