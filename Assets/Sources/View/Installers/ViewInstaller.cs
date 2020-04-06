using Craft_TZ.GameCore.FSM;
using Craft_TZ.Model.Installers;
using UnityEngine;
using Zenject;

namespace Craft_TZ.View.Installers
{
    public class ViewInstaller : MonoInstaller
    {
        [SerializeField]
        private AbstractTile tilePrototype = null;

        [SerializeField]
        private AbstractCrystal crystalPrototype = null;

        [SerializeField]
        private PlayerChip playerChip = null;

        public override void InstallBindings()
        {
            InstallGameCoreFSM();

            Container.BindInterfacesAndSelfTo<AbstractTile>().FromComponentInNewPrefab(tilePrototype).AsSingle();
            Container.BindInterfacesAndSelfTo<AbstractCrystal>().FromComponentInNewPrefab(crystalPrototype).AsSingle();
            Container.BindInterfacesAndSelfTo<PlayerChip>().FromComponentInNewPrefab(playerChip).AsSingle();

            Container.Bind<GameSettings>().AsSingle().WithArguments(tilePrototype.TileType, tilePrototype.TileSize, playerChip.ChipType, playerChip.Radius);
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