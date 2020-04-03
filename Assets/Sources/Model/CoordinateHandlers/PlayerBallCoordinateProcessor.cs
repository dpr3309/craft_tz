using Craft_TZ.Shared.Calculations;
using UnityEngine;

namespace Craft_TZ.Model.CoordinateHandlers
{
    internal sealed class PlayerBallCoordinateProcessor : IFigureCoordinateProcessor
    {
        private readonly float radius;

        public PlayerBallCoordinateProcessor(float radius)
        {
            this.radius = radius;
        }

        public bool ContainsCoordinates(Vector2 coordinatesCenterOfFigure, Vector2 otherCoordinates)
        {
            return GeometricCalculator.CircleContainsPoint(coordinatesCenterOfFigure, otherCoordinates, radius);
        }
    }
}