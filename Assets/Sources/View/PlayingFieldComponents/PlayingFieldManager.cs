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

        private void Start()
        {
            tileGenerator = new SquareTilePositionGenerator();
            pool = new ObjectPool<Tile>(() => tilePrototype.Clone(false), 10);


            List<Vector2> tilePositions = tileGenerator.GenerateLaunchPadPositions();

            foreach (var itemPosition in tilePositions)
            {
                //получить из пула тайл, поставить его в нужную позицию, и включить
                var tileInstance = pool.GetObject();
                tileInstance.Setup(itemPosition);
                tileInstance.Show();
            }
        }

        /// <summary>
        /// Handles the tile generation.
        /// </summary>
        private void HandleTileGeneration()
        {
            List<Vector2> tilePositions = tileGenerator.GenerateTilePositoins();

            foreach (var itemPosition in tilePositions)
            {
                //получить из пула тайл, поставить его в нужную позицию, и включить
                var tileInstance = pool.GetObject();
                tileInstance.Setup(itemPosition);
                tileInstance.Show();
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