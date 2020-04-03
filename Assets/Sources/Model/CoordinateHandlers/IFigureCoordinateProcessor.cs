using UnityEngine;

namespace Craft_TZ.Model.CoordinateHandlers
{
    public interface IFigureCoordinateProcessor
    {
        bool ContainsCoordinates(Vector2 coordinatesCenterOfFigure, Vector2 otherCoordinates);
    }
}