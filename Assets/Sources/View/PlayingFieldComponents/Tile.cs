using UnityEngine;

namespace Craft_TZ.View
{
    internal sealed class Tile : AbstractTile
    {
        public override Vector2 Position { get; protected set; }

        internal override void Setup(Vector2 position)
        {
            transform.position = new Vector3(position.x, 0, position.y);
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