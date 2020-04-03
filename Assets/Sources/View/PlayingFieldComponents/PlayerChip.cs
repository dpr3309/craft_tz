using Craft_TZ.Model.CoordinateModifiers;
using UnityEngine;

namespace Craft_TZ.View
{
    internal class PlayerChip : MonoBehaviour
    {
        [SerializeField]
        private float speed;

        private ICoordinateModifier coordinateModifier;

        private void Update()
        {
            transform.position = coordinateModifier.TransformCoordinates(transform.position, speed);
        }
    }
}