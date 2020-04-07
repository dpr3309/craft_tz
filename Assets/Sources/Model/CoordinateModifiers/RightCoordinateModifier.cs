using UnityEngine;

namespace Craft_TZ.Model.CoordinateModifiers
{
    internal class RightCoordinateModifier : ICoordinateModifier
    {
        public Vector3 TransformCoordinates(Vector3 coordinate, float modifier)
        {
            return Vector3.Lerp(coordinate, coordinate + Vector3.right, modifier);
        }

        public Vector3 TransformCoordinatesFall(Vector3 coordinate, float modifier)
        {
            return Vector3.Lerp(coordinate, coordinate + Vector3.right * 0.5f + Vector3.down * 2, modifier);
        }
    }
}