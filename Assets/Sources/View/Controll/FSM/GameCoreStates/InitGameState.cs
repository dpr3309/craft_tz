using System;
using Craft_TZ.Shared.FSM;

namespace Craft_TZ.GameCore.FSM
{
    internal class InitGameState : AState, IInitGameState
    {
        public override Type Event(Shared.FSM.EventArgs args)
        {
            if (args.Id == GameCoreEvents.Startup.Id)
                return typeof(ReadyToStartState);

            return base.Event(args);
        }
    }
}

