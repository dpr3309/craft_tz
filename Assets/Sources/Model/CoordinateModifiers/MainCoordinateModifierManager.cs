using UnityEngine;

namespace Craft_TZ.Model.CoordinateModifiers
{
    internal sealed class MainCoordinateModifierManager : ICoordinateModifierManager
    {
        private int index;
        private ICoordinateModifier currentCoordinateModifier;

        private ICoordinateModifier[] coordinateModifiers;

        public MainCoordinateModifierManager(params ICoordinateModifier[] coordinateModifiers)
        {
            if (coordinateModifiers.Length == 0)
                throw new System.Exception("[MainCoordinateModifierManager.ctor] coordinateModifiers.Length == 0!");

            this.coordinateModifiers = coordinateModifiers;
            currentCoordinateModifier = this.coordinateModifiers[0];
        }

        public void ChengeCoordinateModifier()
        {
            index = (++index < coordinateModifiers.Length) ? index : 0;
            currentCoordinateModifier = coordinateModifiers[index];
        }

        public Vector3 TransformCoordinates(Vector3 coordinate, float modifier)
        {
            return currentCoordinateModifier.TransformCoordinates(coordinate, modifier);
        }
    }
}