using UnityEngine;
using Zenject;

namespace Craft_TZ.View
{
    public class MovingCamera : MonoBehaviour
    {
        [SerializeField]
        private Vector3 offset;
        [SerializeField]
        private float smooth = 0.5f;

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
            transform.position = Vector3.Lerp(transform.position, playerChipTransform.position + offset, smooth);
        }
    }
}
