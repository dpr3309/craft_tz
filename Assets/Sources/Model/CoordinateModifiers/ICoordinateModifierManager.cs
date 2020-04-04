using UnityEngine;

namespace Craft_TZ.Model.CoordinateModifiers
{
    public interface ICoordinateModifierManager
    {
        void ChengeCoordinateModifier();
        Vector3 TransformCoordinates(Vector3 coordinate, float modifier);
    }
}