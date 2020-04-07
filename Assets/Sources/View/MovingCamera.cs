using UnityEngine;
using Zenject;

namespace Craft_TZ.View
{
    public class MovingCamera : MonoBehaviour
    {
        [SerializeField]
        private float smooth = 0.5f;

        private Vector3 offset;

        private Transform playerChipTransform = null;

        [Inject]
        private void Construct (PlayerChip playerChip)
        {
            playerChipTransform = playerChip.transform;
        }

        private void Start()
        {
            offset = transform.position - playerChipTransform.position;
        }

        private void LateUpdate()
        {
            var newPosition = Vector3.Lerp(transform.position, new Vector3(playerChipTransform.position.x, 0, playerChipTransform.position.z) + offset, smooth);
            transform.position = newPosition;
        }
    }
}
