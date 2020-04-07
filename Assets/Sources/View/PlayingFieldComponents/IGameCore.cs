namespace Craft_TZ.View
{
    public interface IGameCore
    {
        int Score { get; }

        void Startup();
        void Release();
        void WaitLost();
        void HandleInputClick();
    }
}