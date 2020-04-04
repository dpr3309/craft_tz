using UnityEngine;

namespace Craft_TZ.Model.CoordinateModifiers
{
    public class RightCoordinateModifier : ICoordinateModifier
    {
        public Vector3 TransformCoordinates(Vector3 coordinate, float modifier)
        {
            return Vector3.Lerp(coordinate, coordinate + Vector3.right, modifier);
        }
    }
}