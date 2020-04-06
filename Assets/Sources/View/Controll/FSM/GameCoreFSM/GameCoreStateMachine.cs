using Craft_TZ.Shared.FSM;
using Zenject;

namespace Craft_TZ.GameCore.FSM
{
    internal sealed class GameCoreStateMachine : SimpleStateMachine, IGameCoreStateMachine
    {
        [Inject]
        internal void Create(IInitGameState initGameState, IReadyToStartState readyToStartState, IInGameState inGameState, IEndOfGameState endOfGameState, IIReleaseState releaseState)
        {
            Add(initGameState);
            Add(readyToStartState);
            Add(inGameState);
            Add(endOfGameState);
            Add(releaseState);

            Start<InitGameState>();
        }
    }
}