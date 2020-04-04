using System.Collections.Generic;
using UnityEngine;

namespace Craft_TZ.Model.CoordinateHandlers
{
    public interface IMainCoordinateProcessor
    {
        bool CoordinatesAreWithinTiles(Vector2 coordinate, IEnumerable<Vector2> tilesCoordinates);
        bool PlayerChipCollisionWithOtherObject(Vector2 playerChipCoordinate, Vector2 otherObjectCoordinate);
    }
}