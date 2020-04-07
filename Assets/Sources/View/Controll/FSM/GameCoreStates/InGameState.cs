using System;
using Craft_TZ.Shared.FSM;
using Craft_TZ.View;

namespace Craft_TZ.GameCore.FSM
{
    internal class InGameState : AState, IInGameState
    {
        private readonly IPlayerChip playerChip;
        private readonly IInfoManager infoManager;

        public InGameState(IPlayerChip playerChip, IInfoManager infoManager)
        {
            this.playerChip = playerChip;
            this.infoManager = infoManager;
        }

        public override void OnEnter()
        {
            base.OnEnter();
            infoManager.ClearMessage();
            playerChip.StartMove();
        }

        public override Type Event(Shared.FSM.EventArgs args)
        {
            if (args.Id == GameCoreEvents.Click.Id)
            {
                playerChip.ChangeDirection();
            }

            if (args.Id == GameCoreEvents.LostGame.Id)
            {
                return typeof(LostGameState);
            }
            return base.Event(args);
        }
    }
}

