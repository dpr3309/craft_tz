using System;
using Craft_TZ.Shared.FSM;
using Craft_TZ.View;

namespace Craft_TZ.GameCore.FSM
{
    internal class ReleaseState : AState, IIReleaseState
    {
        private readonly IGameCore gameCore;
        private readonly IPlayerChip playerChip;

        public ReleaseState(IGameCore gameCore, IPlayerChip playerChip)
        {
            this.gameCore = gameCore;
            this.playerChip = playerChip;
        }

        public override void OnEnter()
        {
            base.OnEnter();
            gameCore.Release();
            playerChip.Restart();
        }

        public override Type Event(Shared.FSM.EventArgs args)
        {
            if (args.Id == GameCoreEvents.Restart.Id)
                return typeof(ReadyToStartState);

            return base.Event(args);
        }
    }
}

