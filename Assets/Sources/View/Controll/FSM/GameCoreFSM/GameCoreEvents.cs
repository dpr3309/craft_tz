using Craft_TZ.Shared.FSM;

namespace Craft_TZ.GameCore.FSM
{
    public static class GameCoreEvents
    {
        public static EventArgs Startup = new EventArgs("startup");
        public static EventArgs Click = new EventArgs("click");
        public static EventArgs EndOfGame = new EventArgs("end_of_game");
        public static EventArgs Restart = new EventArgs("restart");
        public static EventArgs LostGame = new EventArgs("lost_game");
    }
}