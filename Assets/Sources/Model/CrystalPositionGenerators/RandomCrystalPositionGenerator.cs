using System.Collections.Generic;
using System.Linq;
using Craft_TZ.Shared.SelectionAlgorithms;
using UnityEngine;

namespace Craft_TZ.Model.Crystal
{
    internal sealed class RandomCrystalPositionGenerator : ICrystalPositionGenerator
    {
        private RandomItemSelector selector;

        public RandomCrystalPositionGenerator()
        {
            selector = new RandomItemSelector(5);
        }

        public IReadOnlyCollection<Vector2> GenerateCrystalPositions(IEnumerable<Vector2> tileGenerationPositions)
        {
            return selector.SelectItems(tileGenerationPositions).ToList().AsReadOnly();
        }
    }
}