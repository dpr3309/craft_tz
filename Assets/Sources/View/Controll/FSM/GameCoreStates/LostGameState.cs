using System;
using Craft_TZ.Shared.FSM;
using Craft_TZ.View;

namespace Craft_TZ.GameCore.FSM
{
    internal class LostGameState : AState, ILostGameState
    {
        private readonly IPlayerChip playerChip;
        private readonly IGameCore gameCore;

        public LostGameState(IPlayerChip playerChip, IGameCore gameCore)
        {
            this.playerChip = playerChip;
            this.gameCore = gameCore;
        }

        public override void OnEnter()
        {
            base.OnEnter();
            playerChip.StartFall();
            gameCore.WaitLost();
        }

        public override Type Event(Shared.FSM.EventArgs args)
        {
            if (args.Id == GameCoreEvents.EndOfGame.Id)
            {
                return typeof(EndOfGameState);
            }
            return base.Event(args);
        }
    }
}

