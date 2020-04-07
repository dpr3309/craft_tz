using System;
using System.Collections.Generic;
using System.Linq;
using Craft_TZ.Model.CoordinateHandlers;
using Craft_TZ.Model.CoordinateModifiers;
using Craft_TZ.Model.Crystal;
using Craft_TZ.Model.Enums;
using Craft_TZ.Model.SquareTile;
using Craft_TZ.Shared;
using UnityEngine;
using Zenject;

namespace Craft_TZ.Model.Installers
{
    internal class ModelInstaller : MonoInstaller
    {
        [SerializeField]
        private Vector2Int launchPadSize = Vector2Int.one;
        [SerializeField]
        private List<CoordinateModifierTypes> coordinateModifierTypes = null;
        [SerializeField]
        private DifficultyLevel difficaltyLevel = (DifficultyLevel)(-1);
        [SerializeField]
        private CrystalPositionGeneratorType crystalPositionGeneratorType = (CrystalPositionGeneratorType)(-1);

        [Inject]
        private GameSettings settings = null;


        public override void InstallBindings()
        {

            Debug.Log(GetType().Name);

            InstallCrystalPositionGenerator(crystalPositionGeneratorType);

            // 1 - с учетом выбранного типа тайлов, инстанцировать прототип тайла
            // 2 с учетом типа тайлов, и с учетом размера инстанцированного тала, инстанцировать генератор позиций тайлов
            InstallTilePositionGenerator(settings.TileType, settings.TileSize, difficaltyLevel, launchPadSize);
            // 3 с учетом типа тайла и его размеров, инстанцировать процессор координат тайлов (для проверки, находится ли фишки игрока, в пределах инстанированных тайлов, или нет)
            InstallTileCoordinateProcessor(settings.TileType, settings.TileSize);


            // 4 - с учетом выбранного типа фишки игрока - инстанцировать фишку игрока
            // 5  с учетом выбранного типа фишки игрока и ее размера, инстанцировать процессор координат позиции фишки игрока
            InstallPlayerChipCoordinateProcessor(settings.PlayerChipType, settings.PlayerChipRadius);


            Container.BindInterfacesAndSelfTo<MainCoordinateProcessor>().AsSingle();

            InstallCoordinateModifierManager(coordinateModifierTypes);

        }

        private void InstallCoordinateModifierManager(List<CoordinateModifierTypes> coordinateModifierTypes)
        {
            if (coordinateModifierTypes.Count == 0)
                throw new Exception("[ModelInstaller.InstallCoordinateModifierManager] settings.coordinateModifierTypes.Count ==0");

            //проверка на наличие повторяющихся элементов в коллекци
            if (coordinateModifierTypes.GroupBy(i => i).Any(i => i.Count() > 1))
                throw new Exception("[ModelInstaller.InstallCoordinateModifierManager] settings.coordinateModifierTypes contains duplicate items");

            List<ICoordinateModifier> coordinateModifiers = new List<ICoordinateModifier>();
            foreach (var coordinateModifierType in coordinateModifierTypes)
            {
                switch (coordinateModifierType)
                {
                    case CoordinateModifierTypes.Forward:
                        coordinateModifiers.Add(new ForwardCoordinateModifier());
                        break;
                    case CoordinateModifierTypes.Right:
                        coordinateModifiers.Add(new RightCoordinateModifier());
                        break;
                    default:
                        throw new Exception($"[ModelInstaller.InstallCoordinateModifierManager] unhandled coordinateModifierType : {coordinateModifierType}");
                }
            }

            Container.BindInterfacesTo<MainCoordinateModifierManager>().AsSingle().WithArguments(coordinateModifiers.ToArray());
        }

        private void InstallPlayerChipCoordinateProcessor(PlayerChipType playerChipType, float playerChipRadius)
        {
            switch (playerChipType)
            {
                case PlayerChipType.Circle:
                    Container.BindInterfacesTo<PlayerBallCoordinateProcessor>().AsSingle().WithArguments(playerChipRadius);
                    break;
                default:
                    throw new Exception($"[ModelInstaller.InstallPlayerChipCoordinateProcessor] unhandled TileType : {playerChipType}");
            }
        }

        private void InstallTileCoordinateProcessor(TileType tileType, float tileSize)
        {
            switch (tileType)
            {
                case TileType.Square:
                    Container.BindInterfacesTo<SquareTileCoordinateProcessor>().AsSingle().WithArguments(tileSize);
                    break;
                default:
                    throw new Exception($"[ModelInstaller.InstallTileCoordinateProcessor] unhandled TileType : {tileType}");
            }
        }

        private void InstallTilePositionGenerator(TileType tileType, float tileSize, DifficultyLevel difficultyLevel, Vector2Int launchPadSize)
        {
            if (tileSize <= 0)
                throw new Exception($"[ModelInstaller.InstallTilePositionGenerator] tile size <= 0");

            if (launchPadSize.x <= 0 || launchPadSize.y <= 0)
                throw new Exception($"[ModelInstaller.InstallTilePositionGenerator] incorrect launch pad dimensions");

            switch (tileType)
            {
                case TileType.Square:
                    Container.BindInterfacesTo<SquareTilePositionGenerator>().AsSingle().WithArguments(tileSize, difficultyLevel, launchPadSize);
                    break;
                default:
                    throw new Exception($"[ModelInstaller.InstallTilePositionGenerator] unhandled TileType : {tileType}");
            }
        }

        private void InstallCrystalPositionGenerator(CrystalPositionGeneratorType type)
        {
            switch (type)
            {
                case CrystalPositionGeneratorType.Random:
                    Container.BindInterfacesTo<RandomCrystalPositionGenerator>().AsSingle();
                    break;
                case CrystalPositionGeneratorType.InOrder:
                    Container.BindInterfacesTo<InOrderCrystalPositionGenerator>().AsSingle();
                    break;
                default:
                    throw new Exception($"[ModelInstaller.InstallCrystalPositionGenerator] unhandled CrystalPositionGeneratorType : {type}");
            }
        }
    }
}