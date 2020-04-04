using System.Collections.Generic;
using System.Linq;
using Craft_TZ.Shared.SelectionAlgorithms;
using UnityEngine;

namespace Craft_TZ.Model.Crystal
{
    internal sealed class InOrderCrystalPositionGenerator : ICrystalPositionGenerator
    {
        private InOrderItemSelector selector;

        public InOrderCrystalPositionGenerator()
        {
            selector = new InOrderItemSelector(5);
        }

        public IReadOnlyCollection<Vector2> GenerateCrystalPositions(IEnumerable<Vector2> tileGenerationPositions)
        {
            return selector.SelectItems(tileGenerationPositions).ToList().AsReadOnly();
        }
    }
}