using System.Collections.Generic;
using Craft_TZ.Model;
using Craft_TZ.Model.SquareTile;
using Craft_TZ.Shared;
using UnityEngine;

namespace Craft_TZ.View
{
    internal class PlayingFieldManager : MonoBehaviour
    {
        [SerializeField]
        private Tile tilePrototype;

        private ObjectPool<Tile> pool;

        private ITilePositionGenerator tileGenerator;

        public DifficultyLevel difficultyLevel;

        private void Start()
        {
            tileGenerator = new SquareTilePositionGenerator(1, difficultyLevel);
            pool = new ObjectPool<Tile>(() => tilePrototype.Clone(false), 10);


            var tilesPositions = tileGenerator.GenerateLaunchPadPositions();
            GenerateTilesInPositions(tilesPositions);
        }

        /// <summary>
        /// Handles the tile generation.
        /// </summary>
        private void HandleTileGeneration()
        {
            var tilesPositions = tileGenerator.GenerateTilePositoins();

            /*
            foreach (var itemPosition in tilePositions)
            {
                //получить из пула тайл, поставить его в нужную позицию, и включить
                var tileInstance = pool.GetObject();
                tileInstance.Setup(itemPosition);
                tileInstance.Show();
            }
            */
            GenerateTilesInPositions(tilesPositions);
        }

        private void GenerateTilesInPositions(Vector2[,] positions)
        {
            for (int y = 0; y <= positions.GetUpperBound(1); y++)
            {
                for (int x = 0; x <= positions.GetUpperBound(0); x++)
                {
                    var tileInstance = pool.GetObject();
                    tileInstance.Setup(positions[x,y]);
                    tileInstance.Show();
                }
            }
        }


        public bool T1, T2;
        private void Update()
        {
            if (T1)
            {
                T1 = !T1;
                HandleTileGeneration();
                return;
            }

        }

    }
}