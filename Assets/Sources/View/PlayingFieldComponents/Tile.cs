using UnityEngine;

namespace Craft_TZ.View
{
    internal class Tile : MonoBehaviour
    {
        internal void Setup(Vector2 position)
        {
            this.transform.position = new Vector3(position.x, 0, position.y);
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