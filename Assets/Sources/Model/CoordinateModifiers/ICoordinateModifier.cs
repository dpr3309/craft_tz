using UnityEngine;

namespace Craft_TZ.Model.CoordinateModifiers
{
    public interface ICoordinateModifier
    {
        Vector3 TransformCoordinates(Vector3 coordinate, float modifier);
        Vector3 TransformCoordinatesFall(Vector3 coordinate, float modifier);
    }
}