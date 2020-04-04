using System.Collections.Generic;
using System.Linq;
using Craft_TZ.Model;
using Craft_TZ.Model.CoordinateHandlers;
using Craft_TZ.Model.Crystal;
using Craft_TZ.Shared;
using Craft_TZ.Shared.Calculations;
using UnityEngine;
using Zenject;

namespace Craft_TZ.View
{
    internal class PlayingFieldManager : MonoBehaviour
    {
        [SerializeField]
        private Tile tilePrototype;
        [SerializeField]
        private Crystal crystalPrototype;

        private int counter;

        private ObjectPool<Tile> poolOfTiles;
        private ObjectPool<Crystal> poolOfCrystals;

        private List<Tile> tileInstances = new List<Tile>();
        private List<Crystal> crystalInstances = new List<Crystal>();

        private IMainCoordinateProcessor mainCoordinateProcessor;
        private ITilePositionGenerator positionGenerator;
        private ICrystalPositionGenerator crystalPositionGenerator;
        public DifficultyLevel difficultyLevel;

        private bool isConstructed = false;

        [Inject]
        private void Construct(ICrystalPositionGenerator crystalPositionGenerator, ITilePositionGenerator positionGenerator, IMainCoordinateProcessor mainCoordinateProcessor)
        {
            if (isConstructed)
                throw new System.Exception($"[{GetType().Name}.Construct] object already constructed");

            Debug.Log("PlayingFieldManager.Construct");
            this.crystalPositionGenerator = crystalPositionGenerator;
            this.mainCoordinateProcessor = mainCoordinateProcessor;
            this.positionGenerator = positionGenerator;

            isConstructed = true;
        }


        private void Start()
        {
            poolOfTiles = new ObjectPool<Tile>(() => tilePrototype.Clone(false), 100);
            poolOfCrystals = new ObjectPool<Crystal>(() => crystalPrototype.Clone(false), 10);


            var tilesPositions = positionGenerator.GenerateLaunchPadPositions().ToList().AsReadOnly();
            GenerateTilesInPositions(tilesPositions);


            for (int i = 0; i < 5; i++)
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
                crystalInstances.Add(crystalInstance);
            }
        }

        private void GenerateTilesInPositions(IEnumerable<Vector2> positions)
        {
            foreach (var item in positions)
            {
                var tileInstance = poolOfTiles.GetObject();
                tileInstance.Setup(item);
                tileInstance.Show();
                tileInstances.Add(tileInstance);
            }
        }



        public void Step(Vector2 playerChipCoordinates)
        {
            //todo: проверить, стоит ли фишка чувака на полу
            if (mainCoordinateProcessor.CoordinatesAreWithinTiles(playerChipCoordinates, tileInstances.Select(i => i.Position)))
            {
                List<Crystal> toRemove = new List<Crystal>();

                // если да: 
                // потом, проверить коллизию с кристалами
                foreach (var crystalInstance in crystalInstances)
                {
                    if (mainCoordinateProcessor.PlayerChipCollisionWithOtherObject(playerChipCoordinates, crystalInstance.Position))
                    {
                        // механика подбора кристала
                        // увеличить счетчик очков, переместить кристал в коллекцию на удаление
                        counter++;
                        toRemove.Add(crystalInstance);
                    }
                }

                for (int i = 0; i < toRemove.Count; i++)
                {
                    var crystal = toRemove[i];
                    crystal.Hide();
                    poolOfCrystals.PutObject(crystal);
                    crystalInstances.Remove(crystal);
                }

            }
            else
            {
                //todo: фишка игрока должна упасть, перевести игру с состояние конец игры
                Debug.LogWarning("ПАДАЕМ!!!!!");
            }
        }

        public Vector2 center, other;
        public float radius;

        public bool T1, T2, T3;
        private void Update()
        {
            if (T1)
            {
                T1 = !T1;
                HandleStepGeneration();
                return;
            }
            if (T2)
            {
                T2 = !T2;
                Debug.Log(GeometricCalculator.CircleContainsPoint(center, other, radius));
            }
        }
    }
}