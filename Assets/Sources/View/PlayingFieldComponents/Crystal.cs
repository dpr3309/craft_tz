using UnityEngine;

namespace Craft_TZ.View
{
    internal class Crystal : MonoBehaviour
    {
        public Vector2 Position { get; private set; }

        internal void Setup(Vector2 position)
        {
            this.transform.position = new Vector3(position.x, 1.5f, position.y);
            Position = position;
        }

        internal void Show()
        {
            this.gameObject.SetActive(true);
        }

        internal void Hide()
        {
            this.gameObject.SetActive(false);
        }
    }

}