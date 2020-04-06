﻿using System;
using System.Collections.Generic;
using System.Linq;
using Craft_TZ.GameCore.FSM;
using Craft_TZ.Model;
using Craft_TZ.Model.CoordinateHandlers;
using Craft_TZ.Model.Crystal;
using Craft_TZ.Shared.Calculations;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Craft_TZ.View
{

    internal class PlayingFieldManager : MonoBehaviour, IGameCore
    {
        [SerializeField]
        private Text countText = null;

        [SerializeField]
        private float offset;

        [SerializeField]
        private int minTilesCount;

        private int counter;

        private List<AbstractTile> tileInstances = new List<AbstractTile>();
        private List<AbstractCrystal> crystalInstances = new List<AbstractCrystal>();

        private IMainCoordinateProcessor mainCoordinateProcessor;
        private ITilePositionGenerator positionGenerator;
        private ICrystalPositionGenerator crystalPositionGenerator;
        private IGameCoreStateMachine gameCoreStateMachine;

        private PoolOfTiles poolOfTiles;
        private PoolOfCrystals poolOfCrystals;

        private bool isConstructed = false;

        [Inject]
        private void Construct(ICrystalPositionGenerator crystalPositionGenerator, ITilePositionGenerator positionGenerator, IMainCoordinateProcessor mainCoordinateProcessor, IGameCoreStateMachine gameCoreStateMachine, PoolOfTiles poolOfTiles, PoolOfCrystals poolOfCrystals)
        {
            if (isConstructed)
                throw new System.Exception($"[{GetType().Name}.Construct] object already constructed");

            Debug.Log("PlayingFieldManager.Construct");
            this.crystalPositionGenerator = crystalPositionGenerator;
            this.mainCoordinateProcessor = mainCoordinateProcessor;
            this.positionGenerator = positionGenerator;
            this.gameCoreStateMachine = gameCoreStateMachine;
            this.poolOfTiles = poolOfTiles;
            this.poolOfCrystals = poolOfCrystals;

            isConstructed = true;
        }

        private void Start()
        {
            gameCoreStateMachine.Event(GameCoreEvents.Startup);
        }

        public void Startup()
        {
            var tilesPositions = positionGenerator.GenerateLaunchPadPositions().ToList().AsReadOnly();
            GenerateTilesInPositions(tilesPositions);

            CheckTilesCount();
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

        public void Release()
        {
            foreach (var crystal in crystalInstances)
            {
                crystal.gameObject.SetActive(false);
                poolOfCrystals.PutObject(crystal);
            }
            crystalInstances.Clear();

            foreach (var tile in tileInstances)
            {
                tile.gameObject.SetActive(false);
                poolOfTiles.PutObject(tile);
            }
            tileInstances.Clear();

            counter = 0;
            countText.text = counter.ToString();
            gameCoreStateMachine.Event(GameCoreEvents.Restart);
        }

        private void CheckTilesCount()
        {
            if (tileInstances.Count < minTilesCount)
            {
                HandleStepGeneration();
                CheckTilesCount();
            }
        }

        public void Step(Vector2 playerChipCoordinates)
        {
            //todo: проверить, стоит ли фишка чувака на полу
            if (mainCoordinateProcessor.CoordinatesAreWithinTiles(playerChipCoordinates, tileInstances.Select(i => i.Position)))
            {
                ReleaseTraversedObjects(playerChipCoordinates);

                CheckTilesCount();

                List<AbstractCrystal> toRemove = new List<AbstractCrystal>();

                // если да: 
                // потом, проверить коллизию с кристалами
                foreach (var crystalInstance in crystalInstances)
                {
                    if (mainCoordinateProcessor.PlayerChipCollisionWithOtherObject(playerChipCoordinates, crystalInstance.Position))
                    {
                        // механика подбора кристала
                        // увеличить счетчик очков, переместить кристал в коллекцию на удаление
                        counter++;
                        countText.text = counter.ToString();
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

                gameCoreStateMachine.Event(GameCoreEvents.EndOfGame);
            }
        }

        private void ReleaseTraversedObjects(Vector2 playerChipCoordinates)
        {
            var traversedTiles = SelectTraversedObject(tileInstances, playerChipCoordinates);
            ReleaseObjects(traversedTiles, tileInstances, poolOfTiles);

            var transferedCrystals = SelectTraversedObject(crystalInstances, playerChipCoordinates);
            ReleaseObjects(transferedCrystals, crystalInstances, poolOfCrystals);
        }

        private List<T> SelectTraversedObject<T>(List<T> instances, Vector2 playerChipCoordinates)
            where T : Component
        {
            return instances.Where(i => i.transform.position.x < playerChipCoordinates.x + offset || i.transform.position.z < playerChipCoordinates.y + offset).ToList();
        }

        private void ReleaseObjects<T>(List<T> itemsToFreed, List<T> instances, PoolOfPrototypes<T> pool)
            where T : Component
        {
            for (int i = 0; i < itemsToFreed.Count; i++)
            {
                itemsToFreed[i].gameObject.SetActive(false);
                pool.PutObject(itemsToFreed[i]);
                instances.Remove(itemsToFreed[i]);
            }
        }

        public Vector2 center, other;
        public float radius;

        public bool T1, T2, T3;
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                gameCoreStateMachine.Event(GameCoreEvents.Click);
            }

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