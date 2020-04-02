using System.Collections.Generic;
using UnityEngine;

namespace Craft_TZ.Model
{
    public interface ITilePositionGenerator
    {
        IReadOnlyCollection<Vector2> GenerateLaunchPadPositions();
        IReadOnlyCollection<Vector2> GeneratePositoins();
    }
}