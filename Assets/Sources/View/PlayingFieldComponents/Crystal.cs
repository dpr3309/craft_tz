using UnityEngine;

namespace Craft_TZ.View
{
    internal sealed class Crystal : AbstractCrystal
    {
        public  override Vector2 Position { get; protected set; }

        internal override void Setup(Vector2 position)
        {
            transform.position = new Vector3(position.x, 1.5f, position.y);
            Position = position;
        }

        internal override void Show()
        {
            gameObject.SetActive(true);
        }

        internal override void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}