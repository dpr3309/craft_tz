using System;
using Craft_TZ.Shared.FSM;
using Craft_TZ.View;

namespace Craft_TZ.GameCore.FSM
{
    internal class ReadyToStartState : AState, IReadyToStartState
    {
        private readonly IGameCore gameCore;
        private readonly IInfoManager infoManager;

        public ReadyToStartState(IGameCore gameCore, IInfoManager infoManager)
        {
            this.infoManager = infoManager;
            this.gameCore = gameCore;
        }

        public override void OnEnter()
        {
            base.OnEnter();
            gameCore.Startup();
            infoManager.StartMessage();
        }

        public override Type Event(Shared.FSM.EventArgs args)
        {
            if (args.Id == GameCoreEvents.Click.Id)
                return typeof(InGameState);

            return base.Event(args);
        }
    }
}

