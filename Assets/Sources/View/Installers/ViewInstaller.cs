using Craft_TZ.GameCore.FSM;
using UnityEngine;
using Zenject;

namespace Craft_TZ.View.Installers
{
    public class ViewInstaller : MonoInstaller
    {
        [SerializeField]
        private Tile tilePrototype = null;

        [SerializeField]
        private Crystal crystalPrototype = null;

        public override void InstallBindings()
        {
            InstallGameCoreFSM();

            Container.BindInterfacesAndSelfTo<AbstractTile>().FromComponentInNewPrefab(tilePrototype).AsSingle();
            Container.BindInterfacesAndSelfTo<AbstractCrystal>().FromComponentInNewPrefab(crystalPrototype).AsSingle();

            //Container.BindInterfacesAndSelfTo<PoolOfCrystals>().AsSingle();
            //Container.BindInterfacesAndSelfTo<PoolOfTiles>().AsSingle();

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