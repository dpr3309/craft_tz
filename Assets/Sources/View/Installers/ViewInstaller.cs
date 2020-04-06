using Craft_TZ.GameCore.FSM;
using Zenject;

namespace Craft_TZ.View.Installers
{
    public class ViewInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            InstallGameCoreFSM();
        }

        private void InstallGameCoreFSM()
        {
            Container.BindInterfacesTo<InitGameState>().AsSingle();
            Container.BindInterfacesTo<ReadyToStartState>().AsSingle();
            Container.BindInterfacesTo<InGameState>().AsSingle();
            Container.BindInterfacesTo<EndOfGameState>().AsSingle();
            Container.BindInterfacesTo<ReleaseState>().AsSingle();
            Container.BindInterfacesTo<GameCoreStateMachine>().AsSingle();
        }
    }
}