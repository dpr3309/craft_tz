using System;
using System.Collections.Generic;
using Craft_TZ.Shared;
using UnityEngine;

namespace Craft_TZ.Model.SquareTile
{
    internal class SquareTilePositionGenerator : ITilePositionGenerator
    {
        private Dictionary<FaceDirections, Vector2[]> extremeCellPositions;

        private readonly Vector2Int launchPadSize = new Vector2Int(3, 3);

        private readonly DifficultyLevel difficultyLevel;
        private readonly int tileSize;
        private readonly ALevelSquareTilePositionGenerator tilePositionGenerator;

        public SquareTilePositionGenerator(int tileSize, DifficultyLevel difficultyLevel)
        {
            this.difficultyLevel = difficultyLevel;
            this.tileSize = tileSize;

            tilePositionGenerator = GenerateTilePositionGenerator(difficultyLevel, tileSize);
        }

        /// <summary>
        /// factory method, generates tiles position generator taking into account the current level of complexity
        /// </summary>
        /// <returns>The tile position generator.</returns>
        /// <param name="currentDifficultyLevel">Current difficulty level.</param>
        /// <param name="currentTileSize">Current tile size.</param>
        private ALevelSquareTilePositionGenerator GenerateTilePositionGenerator(DifficultyLevel currentDifficultyLevel, int currentTileSize)
        {
            switch (currentDifficultyLevel)
            {
                case DifficultyLevel.High:
                    return new HighLevelSquareTilePositionGenerator(currentTileSize);
                case DifficultyLevel.Middle:
                    return new MiddleLevelSquareTilePositionGenerator(currentTileSize);
                case DifficultyLevel.Low:
                    return new LowLevelSquareTilePositionGenerator(currentTileSize);
                default:
                    throw new Exception($"[SquareTilePositionGenerator.GenerateTilePosiitonGenerator]unhandled difficulty level: {currentDifficultyLevel}");
            }
        }

        public IReadOnlyCollection<Vector2> GenerateLaunchPadPositions()
        {
            Vector2[,] generatedTilePositions = new Vector2[launchPadSize.x, launchPadSize.y];
            List<Vector2> result = new List<Vector2>();
            for (int y = 0; y < launchPadSize.y; y++)
            {
                for (int x = 0; x < launchPadSize.x; x++)
                {
                    generatedTilePositions[x, y] = new Vector2(x * tileSize, y * tileSize);
                    result.Add(generatedTilePositions[x, y]);
                }
            }

            Vector2[,] generatedPositionsAccordingToDifficultyLevel = ConvertCoordinatesAccordingDifficultyLevel(generatedTilePositions, difficultyLevel);
            extremeCellPositions = tilePositionGenerator.GenerateExtremeCellPositions(generatedPositionsAccordingToDifficultyLevel);

            return result.AsReadOnly();
        }

        private Vector2[,] ConvertCoordinatesAccordingDifficultyLevel(Vector2[,] originalCoordinates, DifficultyLevel targetCoordinatesDifficultyLevel)
        {
            DifficultyLevel originalCoordinatesDifficaltyLevel = DetermineDifficultyLevelForCoordinates(originalCoordinates.GetUpperBound(0));

            if (targetCoordinatesDifficultyLevel < originalCoordinatesDifficaltyLevel)
                throw new Exception($"[SquareTilePositionGenerator.ConvertCoordinatesAccordingDifficultyLevel] attempt to convert from greater difficulty to less. {targetCoordinatesDifficultyLevel} < {originalCoordinatesDifficaltyLevel}");

            switch (targetCoordinatesDifficultyLevel)
            {
                // ширина поля 1 тайл
                case DifficultyLevel.High:
                    {
                        var x = originalCoordinates.GetUpperBound(0);
                        var y = originalCoordinates.GetUpperBound(1);
                        return new Vector2[1, 1] { { originalCoordinates[x, y] } };
                    }
                // ширина поля 2х2 тайла 
                case DifficultyLevel.Middle:
                    {
                        Vector2[,] result = new Vector2[2, 2];

                        int[] coordinatesIndexes = { 1, 2 };

                        for (int y = 0; y < coordinatesIndexes.Length; y++)
                        {
                            for (int x = 0; x < coordinatesIndexes.Length; x++)
                            {
                                result[x, y] = originalCoordinates[coordinatesIndexes[x], coordinatesIndexes[y]];
                            }
                        }
                        return result;
                    }
                //ширина поля 3х3 тайла
                case DifficultyLevel.Low:
                    return originalCoordinates;

                default:
                    throw new Exception($"[SquareTilePositionGenerator.ConvertCoordinatesAccordingDifficultyLevel] unhandled difficulty level: {targetCoordinatesDifficultyLevel}");
            }
        }

        private DifficultyLevel DetermineDifficultyLevelForCoordinates(int upperBoundOfDimensions)
        {
            switch (upperBoundOfDimensions)
            {
                case 0:
                    return DifficultyLevel.High;
                case 1:
                    return DifficultyLevel.Middle;
                case 2:
                    return DifficultyLevel.Low;

                default:
                    throw new Exception($"[SquareTilePositionGenerator.DetermineDifficultyLevelForCoordinates] unhandled dimensions of coordinates array. Current array dimensions = {upperBoundOfDimensions}");
            }
        }

        public IReadOnlyCollection<Vector2> GeneratePositoins()
        {
            FaceDirections extremeFace = (FaceDirections)UnityEngine.Random.Range(0, 2);
            Vector2[,] generatedTilePositions = tilePositionGenerator.GenerateTilePositoins(extremeFace, extremeCellPositions);
            extremeCellPositions = tilePositionGenerator.GenerateExtremeCellPositions(generatedTilePositions);

            List<Vector2> result = new List<Vector2>();
            for (int y = 0; y <= generatedTilePositions.GetUpperBound(1); y++)
            {
                for (int x = 0; x <= generatedTilePositions.GetUpperBound(0); x++)
                {
                    result.Add(generatedTilePositions[x, y]);
                }
            }

            return result.AsReadOnly();
        }
    }
}