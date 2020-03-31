using System;
using System.Collections.Generic;
using System.Linq;
using Craft_TZ.Shared;
using UnityEngine;

namespace Craft_TZ.Model.SquareTile
{
    public class SquareTilePositionGenerator : ITilePositionGenerator
    {
        internal enum FaceDirections
        {
            Horizontal, //top face
            Vertical    //right face
        }

        private DifficultyLevel difficultyLevel = DifficultyLevel.Low;
        private int tileSize = 1;

        private Dictionary<FaceDirections, Vector2[]> extremeCellPositions;

        public List<Vector2> GenerateLaunchPadPositions()
        {
            List<Vector2> result = new List<Vector2>();

            for (int y = 0; y < 3; y++)
            {
                for (int x = 0; x < 3; x++)
                {
                    result.Add(new Vector2(x * tileSize, y * tileSize));
                }
            }

            List<Vector2> generatedPositionsAccordingToDifficultyLevel = HandleGeneratedPositionsAccordingDifficultyLevel(result);
            extremeCellPositions = GenerateExtremeCellPositions(generatedPositionsAccordingToDifficultyLevel);
            return result;
        }

        /// <summary>
        /// т.к. для любого уровня сложности, стартовая площадка всегда 3х3,
        /// нужно сделать выборку позиций, так, как будто генерироваллась не стартовая площадка, а обычная генерация тайлов, в соответствии с текущим уровнем сложности
        /// </summary>
        /// <returns>The generated positions according difficulty level.</returns>
        /// <param name="generatedTilePositions">Generated tile positions.</param>
        private List<Vector2> HandleGeneratedPositionsAccordingDifficultyLevel(IReadOnlyList<Vector2> generatedTilePositions)
        {
            List<Vector2> result = new List<Vector2>();
            //в зависимости от текущего уровня сложности сохранить координаты крайних ячеек
            switch (difficultyLevel)
            {
                // ширина поля 1 тайл
                case DifficultyLevel.High:
                    result.Add(generatedTilePositions.Last());
                    break;
                // ширина поля 2х2 тайла 
                case DifficultyLevel.Middle:
                    result.AddRange(generatedTilePositions.Skip(4).Take(2));
                    result.AddRange(generatedTilePositions.Skip(7).Take(2));
                    break;
                //ширина поля 3х3 тайла
                case DifficultyLevel.Low:
                    result = generatedTilePositions.Select(i => i).ToList();
                    break;
                default:
                    throw new Exception($"[SquareTilePositionGenerator.HandleGeneratedPositionsAccordingDifficultyLevel] unhandled DifficaltyLevel: {difficultyLevel}");
            }

            return result;
        }

        private Dictionary<FaceDirections, Vector2[]> GenerateExtremeCellPositions(IReadOnlyList<Vector2> generatedTilePositions)
        {
            //в зависимости от текущего уровня сложности сохранить координаты крайних ячеек
            switch (difficultyLevel)
            {
                // ширина поля 1 тайл
                case DifficultyLevel.High:
                    return new Dictionary<FaceDirections, Vector2[]>
                    {
                        { FaceDirections.Horizontal, new Vector2[] { generatedTilePositions.Last() } } ,
                        { FaceDirections.Vertical, new Vector2[] { generatedTilePositions.Last() } }
                    };
                // ширина поля 2х2 тайла 
                case DifficultyLevel.Middle:
                    return new Dictionary<FaceDirections, Vector2[]>
                    {
                        { FaceDirections.Horizontal, new Vector2[] { generatedTilePositions[2],generatedTilePositions[3] } } ,
                        { FaceDirections.Vertical, new Vector2[] { generatedTilePositions[1], generatedTilePositions[3] } }
                    };
                //ширина поля 3х3 тайла
                case DifficultyLevel.Low:
                    return new Dictionary<FaceDirections, Vector2[]>
                    {
                        { FaceDirections.Horizontal, new Vector2[] { generatedTilePositions[6],generatedTilePositions[7], generatedTilePositions[8] } } ,
                        { FaceDirections.Vertical, new Vector2[] { generatedTilePositions[2], generatedTilePositions[5], generatedTilePositions[8] } }
                    };
                default:
                    throw new Exception($"[SquareTilePositionGenerator.GenerateExtremeCellPositions] unhandled DifficaltyLevel: {difficultyLevel}");
            }
        }

        /// <summary>
        /// Creates tile positions.
        /// с учетом текущего уровня, сгенерировать позиции для следующих квадратных тайлов
        /// </summary>
        /// <returns>The tile positoins.</returns>
        public List<Vector2> GenerateTilePositoins()
        {
            List<Vector2> result = new List<Vector2>();

            var extremeFace = (FaceDirections)UnityEngine.Random.Range(0, 2);

            switch (difficultyLevel)
            {
                case DifficultyLevel.High:

                    switch (extremeFace)
                    {
                        case FaceDirections.Horizontal:
                            {
                                //значит следующий тайл генерируется с верху
                                var extremeCellPosition = extremeCellPositions[extremeFace][0];
                                var tilePosition = new Vector2(extremeCellPosition.x, extremeCellPosition.y + tileSize);
                                result.Add(tilePosition);
                            }
                            break;
                        case FaceDirections.Vertical:
                            {
                                //значит следующий тайл генерируется с боку
                                var extremeCellPosition = extremeCellPositions[extremeFace][0];
                                var tilePosition = new Vector2(extremeCellPosition.x + tileSize, extremeCellPosition.y);
                                result.Add(tilePosition);
                            }
                            break;
                        default:
                            throw new Exception($"[SquareTilePositionGenerator.GenerateTilePositoins] unhandled FaceDirections: {extremeFace}");
                    }

                    break;
                case DifficultyLevel.Middle:
                    switch (extremeFace)
                    {

                        case FaceDirections.Horizontal:
                            {
                                // значит следующие тайлы генерируются сверху
                                Vector2[] extremeCellPositionItems = extremeCellPositions[extremeFace];
                                for (int y = 1; y < 3; y++)
                                {
                                    for (int x = 0; x < 2; x++)
                                    {
                                        var extremeCellPositionItem = extremeCellPositionItems[x];
                                        var tilePosition = new Vector2(extremeCellPositionItem.x, extremeCellPositionItem.y + tileSize * y);
                                        result.Add(tilePosition);
                                    }
                                }
                            }
                            break;
                        case FaceDirections.Vertical:
                            {
                                // значит следующие тайлы генерируются с боку
                                Vector2[] extremeCellPositionItems = extremeCellPositions[extremeFace];

                                for (int x = 0; x < 2; x++)
                                {
                                    for (int y = 1; y < 3; y++)
                                    {
                                        var extremeCellPositionItem = extremeCellPositionItems[x];
                                        var tilePosition = new Vector2(extremeCellPositionItem.x + tileSize * y, extremeCellPositionItem.y);
                                        result.Add(tilePosition);
                                    }
                                }
                            }
                            break;
                        default:
                            throw new Exception($"[SquareTilePositionGenerator.GenerateTilePositoins] unhandled FaceDirections: {extremeFace}");
                    }
                    break;

                case DifficultyLevel.Low:
                    switch (extremeFace)
                    {
                        case FaceDirections.Horizontal:
                            {
                                // значит следующие тайлы генерируются сверху
                                Vector2[] extremeCellPositionItems = extremeCellPositions[extremeFace];
                                for (int y = 1; y < 4; y++)
                                {
                                    for (int x = 0; x < 3; x++)
                                    {
                                        var extremeCellPositionItem = extremeCellPositionItems[x];
                                        var tilePosition = new Vector2(extremeCellPositionItem.x, extremeCellPositionItem.y + tileSize * y);
                                        result.Add(tilePosition);
                                    }
                                }
                            }
                            break;
                        case FaceDirections.Vertical:
                            {
                                // значит следующие тайлы генерируются с боку
                                Vector2[] extremeCellPositionItems = extremeCellPositions[extremeFace];

                                for (int x = 0; x < 3; x++)
                                {
                                    for (int y = 1; y < 4; y++)
                                    {
                                        var extremeCellPositionItem = extremeCellPositionItems[x];
                                        var tilePosition = new Vector2(extremeCellPositionItem.x + tileSize * y, extremeCellPositionItem.y);
                                        result.Add(tilePosition);
                                    }
                                }
                            }

                            break;
                        
                        default:
                            throw new Exception($"[SquareTilePositionGenerator.GenerateTilePositoins] unhandled FaceDirections: {extremeFace}");
                    }
                    /*
                    for (int y = 0; y < 3; y++)
                    {
                        for (int x = 0; x < 3; x++)
                        {
                            result.Add(new Vector2(x * tileSize, y * tileSize));
                        }
                    }*/
                    break;
                default:
                    throw new Exception($"[SquareTilePositionGenerator.GenerateTilePositoins] unhandled DifficaltyLevel: {difficultyLevel}");
            }
            extremeCellPositions = GenerateExtremeCellPositions(result);
            return result;
        }
    }
}