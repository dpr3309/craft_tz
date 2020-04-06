using System;
using Craft_TZ.Shared.FSM;
using Craft_TZ.View;

namespace Craft_TZ.GameCore.FSM
{
    internal class ReadyToStartState : AState, IReadyToStartState
    {
        private readonly IGameCore gameCore;

        public ReadyToStartState(IGameCore gameCore)
        {
            this.gameCore = gameCore;
        }

        public override void OnEnter()
        {
            base.OnEnter();
            gameCore.Startup();
        }

        public override Type Event(Shared.FSM.EventArgs args)
        {
            if (args.Id == GameCoreEvents.Click.Id)
                return typeof(InGameState);

            return base.Event(args);
        }
    }
}

