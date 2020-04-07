using Craft_TZ.Shared.FSM;
using Zenject;

namespace Craft_TZ.GameCore.FSM
{
    internal sealed class GameCoreStateMachine : SimpleStateMachine, IGameCoreStateMachine
    {
        [Inject]
        internal void Create(IInitGameState initGameState, IReadyToStartState readyToStartState, IInGameState inGameState, IEndOfGameState endOfGameState, IIReleaseState releaseState, ILostGameState lostGameState)
        {
            Add(initGameState);
            Add(readyToStartState);
            Add(inGameState);
            Add(endOfGameState);
            Add(releaseState);
            Add(lostGameState);

            Start<InitGameState>();
        }
    }
}