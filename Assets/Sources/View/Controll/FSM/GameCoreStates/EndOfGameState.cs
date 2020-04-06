using System;
using Craft_TZ.Shared.FSM;
using Craft_TZ.View;

namespace Craft_TZ.GameCore.FSM
{
    internal class EndOfGameState : AState, IEndOfGameState
    {
        private readonly IPlayerChip playerChip;

        public EndOfGameState(IPlayerChip playerChip)
        {
            this.playerChip = playerChip;
        }

        public override void OnEnter()
        {
            base.OnEnter();
            playerChip.StopMove();

            //todo: отобразить инфу о конце игры...
        }


        public override Type Event(Shared.FSM.EventArgs args)
        {
            if (args.Id == GameCoreEvents.Click.Id)
                return typeof(ReleaseState);

            return base.Event(args);
        }
    }
}

