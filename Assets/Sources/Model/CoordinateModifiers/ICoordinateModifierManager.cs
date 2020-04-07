using UnityEngine;

namespace Craft_TZ.Model.CoordinateModifiers
{
    public interface ICoordinateModifierManager
    {
        void ChengeCoordinateModifier();
        Vector3 TransformCoordinates(Vector3 coordinate, float modifier);
        void ResetCoordinateModifier();
        Vector3 TransformCoordinatesFall(Vector3 position, float speed);
    }
}