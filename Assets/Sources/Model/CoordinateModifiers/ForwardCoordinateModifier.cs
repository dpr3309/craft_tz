using UnityEngine;

namespace Craft_TZ.Model.CoordinateModifiers
{
    internal class ForwardCoordinateModifier : ICoordinateModifier
    {
        public Vector3 TransformCoordinates(Vector3 coordinate, float modifier)
        {
            return Vector3.Lerp(coordinate, coordinate + Vector3.forward, modifier);
        }

        public Vector3 TransformCoordinatesFall(Vector3 coordinate, float modifier)
        {
            return Vector3.Lerp(coordinate, coordinate + Vector3.forward * 0.5f + Vector3.down * 2, modifier);
        }
    }
}