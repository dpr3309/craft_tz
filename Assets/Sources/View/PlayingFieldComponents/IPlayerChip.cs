namespace Craft_TZ.View
{
    public interface IPlayerChip
    {
        void StartMove();
        void StopMove();
        void ChangeDirection();
        void Restart();
        void StartFall();
    }
}