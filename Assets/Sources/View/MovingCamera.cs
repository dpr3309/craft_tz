using UnityEngine;

namespace Craft_TZ.View
{
    public class MovingCamera : MonoBehaviour
    {
        [SerializeField]
        private Transform playerChipTransform = null;
        [SerializeField]
        private Vector3 offset;
        [SerializeField]
        private float smooth = 0.5f;
        
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
