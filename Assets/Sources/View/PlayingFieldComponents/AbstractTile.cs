using UnityEngine;

namespace Craft_TZ.View
{
    internal abstract class AbstractTile : MonoBehaviour
    {
        public abstract Vector2 Position { get; protected set; }

        internal abstract void Setup(Vector2 position);
        internal abstract void Show();
        internal abstract void Hide();
    }
}