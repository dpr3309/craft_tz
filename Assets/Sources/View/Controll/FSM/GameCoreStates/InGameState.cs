using System;
using Craft_TZ.Shared.FSM;
using Craft_TZ.View;

namespace Craft_TZ.GameCore.FSM
{
    internal class InGameState : AState, IInGameState
    {
        private readonly IPlayerChip playerChip;

        public InGameState(IPlayerChip playerChip)
        {
            this.playerChip = playerChip;
        }

        public override void OnEnter()
        {
            base.OnEnter();
            playerChip.StartMove();
        }

        public override Type Event(Shared.FSM.EventArgs args)
        {
            if (args.Id == GameCoreEvents.Click.Id)
            {
                playerChip.ChangeDirection();
            }

            if (args.Id == GameCoreEvents.EndOfGame.Id)
            {
                return typeof(EndOfGameState);
            }
            return base.Event(args);
        }
    }
}

