using Craft_TZ.Shared.Calculations;
using UnityEngine;

namespace Craft_TZ.Model.CoordinateHandlers
{
    internal sealed class SquareTileCoordinateProcessor : IFigureCoordinateProcessor
    {
        private readonly int tileSize;
        private readonly float halfTileSize;
        private readonly double tileArea;

        public SquareTileCoordinateProcessor(int tileSize)
        {
            this.tileSize = tileSize;
            halfTileSize = tileSize / 2f;
            tileArea = GeometricCalculator.CalculateAreaOfSquare(tileSize);
        }

        public bool ContainsCoordinates(Vector2 coordinatesCenterOfFigure, Vector2 otherCoordinates)
        {
            return GeometricCalculator.SquareContainsPoint(coordinatesCenterOfFigure, otherCoordinates, halfTileSize, tileArea);
        }
    }

}