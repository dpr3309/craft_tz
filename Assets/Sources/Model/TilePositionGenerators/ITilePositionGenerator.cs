using UnityEngine;

namespace Craft_TZ.Model
{
    public interface ITilePositionGenerator
    {
        Vector2[,] GenerateLaunchPadPositions();
        Vector2[,] GenerateTilePositoins();
    }
}