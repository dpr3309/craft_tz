using System;
using System.Collections.Generic;
using System.Linq;
using Craft_TZ.Model.CoordinateHandlers;
using Craft_TZ.Model.CoordinateModifiers;
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
            InstallTilePositionGenerator(settings.tileType, settings.tileSize, settings.difficaltyLevel, settings.launchPadSize);
            // 3 с учетом типа тайла и его размеров, инстанцировать процессор координат тайлов (для проверки, находится ли фишки игрока, в пределах инстанированных тайлов, или нет)
            InstallTileCoordinateProcessor(settings.tileType, settings.tileSize);


            // 4 - с учетом выбранного типа фишки игрока - инстанцировать фишку игрока
            // 5  с учетом выбранного типа фишки игрока и ее размера, инстанцировать процессор координат позиции фишки игрока
            InstallPlayerChipCoordinateProcessor(settings.playerChipType, settings.playerChipRadius);


            Container.BindInterfacesAndSelfTo<MainCoordinateProcessor>().AsSingle();

            InstallCoordinateModifierManager(settings.coordinateModifierTypes);

        }

        private void InstallCoordinateModifierManager(List<CoordinateModifierTypes> coordinateModifierTypes)
        {
            if(coordinateModifierTypes.Count ==0)
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

        private void InstallTilePositionGenerator(TileType tileType, int tileSize, DifficultyLevel difficultyLevel, Vector2Int launchPadSize)
        {
            if (tileSize <= 0)
                throw new Exception($"[ModelInstaller.InstallTilePositionGenerator] tile size <= 0");

            if(launchPadSize.x <=0 || launchPadSize.y <=0)
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

    [System.Serializable]
    public class GameSettings
    {
        public Vector2Int launchPadSize;
        public PlayerChipType playerChipType;
        public float playerChipSpeed;
        public TileType tileType;
        public CrystalPositionGeneratorType crystalPositionGeneratorType;

        public int tileSize;
        public int playerChipRadius;
        public DifficultyLevel difficaltyLevel;
        public List<CoordinateModifierTypes> coordinateModifierTypes;
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

    public enum CoordinateModifierTypes
    {
        Forward,
        Right,
        Left
    }
}