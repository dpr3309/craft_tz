using System;
using Craft_TZ.Model.CoordinateHandlers;
using Craft_TZ.Model.Crystal;
using Craft_TZ.Model.SquareTile;
using Craft_TZ.Shared;
using UnityEngine;
using Zenject;

namespace Craft_TZ.Model.Installers
{
    internal class ModelInstaller : MonoInstaller
    {
        [SerializeField]
        private GameSettings settings;

        

        public override void InstallBindings()
        {
            Debug.Log(this.GetType().Name);

            InstallCrystalPositionGenerator(settings.crystalPositionGeneratorType);

            // 1 - с учетом выбранного типа тайлов, инстанцировать прототип тайла
            // 2 с учетом типа тайлов, и с учетом размера инстанцированного тала, инстанцировать генератор позиций тайлов
            InstallTilePositionGenerator(settings.tileType, settings.tileSize, settings.difficaltyLevel);
            // 3 с учетом типа тайла и его размеров, инстанцировать процессор координат тайлов (для проверки, находится ли фишки игрока, в пределах инстанированных тайлов, или нет)
            InstallTileCoordinateProcessor(settings.tileType, settings.tileSize);


            // 4 - с учетом выбранного типа фишки игрока - инстанцировать фишку игрока
            // 5  с учетом выбранного типа фишки игрока и ее размера, инстанцировать процессор координат позиции фишки игрока
            InstallPlayerChipCoordinateProcessor(settings.playerChipType, settings.playerChipRadius);


            Container.BindInterfacesAndSelfTo<MainCoordinateProcessor>().AsSingle();

        }

        private void InstallPlayerChipCoordinateProcessor(PlayerChipType playerChipType, int playerChipRadius)
        {
            switch (playerChipType)
            {
                case PlayerChipType.Circle:
                    Container.BindInterfacesTo<PlayerBallCoordinateProcessor>().AsSingle().WithArguments<float>(playerChipRadius);
                    break;
                default:
                    throw new Exception($"[ModelInstaller.InstallPlayerChipCoordinateProcessor] unhandled TileType : {playerChipType}");
            }
        }

        private void InstallTileCoordinateProcessor(TileType tileType, int tileSize)
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

        private void InstallTilePositionGenerator(TileType tileType, int tileSize, DifficultyLevel difficultyLevel)
        {
            switch (tileType)
            {
                case TileType.Square:
                    Container.BindInterfacesTo<SquareTilePositionGenerator>().AsSingle().WithArguments(tileSize, difficultyLevel);
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

    [System.Serializable]
    public class GameSettings
    {
        public PlayerChipType playerChipType;
        public float playerChipSpeed;
        public TileType tileType;
        public CrystalPositionGeneratorType crystalPositionGeneratorType;

        public int tileSize;
        public int playerChipRadius;
        public DifficultyLevel difficaltyLevel;
    }

    public enum TileType
    {
        Square,
    }

    public enum PlayerChipType
    {
        Circle,
    }

    public enum CrystalPositionGeneratorType
    {
        Random,
        InOrder
    }
}