using System.Collections.Generic;
using UnityEngine;

namespace Craft_TZ.Model.Crystal
{
    public interface ICrystalPositionGenerator
    {
        IReadOnlyCollection<Vector2> GenerateCrystalPositions(IEnumerable<Vector2> tileGenerationPositions);
    }
}