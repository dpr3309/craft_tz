using System;
using Craft_TZ.Shared.FSM;
using Craft_TZ.View;

namespace Craft_TZ.GameCore.FSM
{
    internal class EndOfGameState : AState, IEndOfGameState
    {
        private readonly IPlayerChip playerChip;
        private readonly IInfoManager infoManager;

        public EndOfGameState(IPlayerChip playerChip, IInfoManager infoManager)
        {
            this.playerChip = playerChip;
            this.infoManager = infoManager;
        }

        public override void OnEnter()
        {
            base.OnEnter();
            playerChip.StopMove();
            infoManager.EndOfGameMessage();
        }

        public override Type Event(Shared.FSM.EventArgs args)
        {
            if (args.Id == GameCoreEvents.Click.Id)
                return typeof(ReleaseState);

            return base.Event(args);
        }
    }
}

