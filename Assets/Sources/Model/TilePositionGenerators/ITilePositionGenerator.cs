using System.Collections.Generic;
using UnityEngine;

namespace Craft_TZ.Model
{
    public interface ITilePositionGenerator
    {
        List<Vector2> GenerateLaunchPadPositions();
        List<Vector2> GenerateTilePositoins();
    }
}