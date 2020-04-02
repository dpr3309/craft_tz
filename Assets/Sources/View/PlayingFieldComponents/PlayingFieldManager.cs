using System.Collections.Generic;
using System.Linq;
using Craft_TZ.Model;
using Craft_TZ.Model.Crystal;
using Craft_TZ.Model.SquareTile;
using Craft_TZ.Shared;
using UnityEngine;

namespace Craft_TZ.View
{
    internal class PlayingFieldManager : MonoBehaviour
    {
        [SerializeField]
        private Tile tilePrototype;
        [SerializeField]
        private Crystal crystalPrototype;

        private ObjectPool<Tile> poolOfTiles;
        private ObjectPool<Crystal> poolOfCrystals;

        private ITilePositionGenerator positionGenerator;
        private ICrystalPositionGenerator crystalPositionGenerator;

        public DifficultyLevel difficultyLevel;


        private void Start()
        {
            positionGenerator = new SquareTilePositionGenerator(1, difficultyLevel);
            crystalPositionGenerator = new RandomCrystalPositionGenerator();

            poolOfTiles = new ObjectPool<Tile>(() => tilePrototype.Clone(false), 100);
            poolOfCrystals = new ObjectPool<Crystal>(() => crystalPrototype.Clone(false), 10);


            var tilesPositions = positionGenerator.GenerateLaunchPadPositions().ToList().AsReadOnly();
            GenerateTilesInPositions(tilesPositions);


            for (int i = 0; i < 10; i++)
            {
                HandleStepGeneration();
            }

        }

        /// <summary>
        /// Handles the tile generation.
        /// </summary>
        private void HandleStepGeneration()
        {
            // получаем новые позициигенерации
            var positions = positionGenerator.GeneratePositoins();

            // генерируем в этих позициях тайлы
            GenerateTilesInPositions(positions);
            //делаем выборку позиций генерации кристалов
            var crystalGeneratePositions = crystalPositionGenerator.GenerateCrystalPositions(positions);
            // в полученных позициях генерируем кристалы
            GenerateCrystalsInPositions(crystalGeneratePositions);
        }

        private void GenerateCrystalsInPositions(IEnumerable<Vector2> positions)
        {
            foreach (var item in positions)
            {
                var crystalInstance = poolOfCrystals.GetObject();
                crystalInstance.Setup(item);
                crystalInstance.Show();
            }
        }

        private void GenerateTilesInPositions(IEnumerable<Vector2> positions)
        {
            foreach (var item in positions)
            {
                var tileInstance = poolOfTiles.GetObject();
                tileInstance.Setup(item);
                tileInstance.Show();
            }
        }


        public bool T1, T2, T3;
        private void Update()
        {
            if (T1)
            {
                T1 = !T1;
                HandleStepGeneration();
                return;
            }
        }
    }
}