using System;
using System.Collections.Generic;

namespace Craft_TZ.Shared.FSM
{
    public class SimpleStateMachine : ISimpleStateMachine
    {
        protected IState currentState;
        public bool IsRunning => currentState != null;

        protected Dictionary<Type, IState> states { get; set; }

        public SimpleStateMachine()
        {
            states = new Dictionary<Type, IState>();
        }

        public void Add(IState state)
        {
            if (IsRunning)
                throw new InvalidOperationException(GetType().Name + " has allready started and cannot receive new states");

            Type type = state.GetType();

            if (states.ContainsKey(type))
                throw new InvalidOperationException($"The given state ({type.Name}) allready present in {GetType().Name}");
            states.Add(type, state);
        }

        public void Event(EventArgs args)
        {
            if (!IsRunning)
                return;

            Type typeOfNewState = currentState.Event(args);

            if (typeOfNewState != null)
            {
                if (typeOfNewState != currentState.GetType())
                {
                    ChangeState(typeOfNewState);
                }
            }
            else
            {
                throw new InvalidOperationException($" Returned type of next state  is null on event {args.Id} in {currentState.GetType().Name}");
            }
        }

        public void Start<T>() where T : IState
        {
            if (IsRunning)
                throw new InvalidOperationException(GetType().Name + " has allready started");

            ChangeState(typeof(T));
        }

        private void ChangeState(Type newState)
        {
            if (!states.ContainsKey(newState))
                throw new InvalidOperationException($"The given state ({newState.Name}) was not present in {GetType().Name}");

            currentState?.OnExit();

            var prevState = currentState;
            currentState = states[newState];

            currentState.OnEnter();
        }
    }
}

