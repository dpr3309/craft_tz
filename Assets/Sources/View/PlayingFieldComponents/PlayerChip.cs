using Craft_TZ.Model.CoordinateModifiers;
using UnityEngine;
using Zenject;

namespace Craft_TZ.View
{
    internal class PlayerChip : MonoBehaviour
    {
        [SerializeField]
        private float speed;

        [SerializeField]
        private PlayingFieldManager gameLoopManager;

        private bool isConstructed = false;

        private ICoordinateModifierManager coordinateModifierManager;

        [Inject]
        private void Construct(ICoordinateModifierManager coordinateModifierManager)
        {
            if (isConstructed)
                throw new System.Exception($"[{GetType().Name}.Construct] object already constructed");

            this.coordinateModifierManager = coordinateModifierManager;

            isConstructed = true;
        }

        private void Update()
        {
            transform.position = coordinateModifierManager.TransformCoordinates(transform.position, speed);
            gameLoopManager.Step(new Vector2(transform.position.x, transform.position.z));

            if (Input.GetKeyDown(KeyCode.Space))
            {
                coordinateModifierManager.ChengeCoordinateModifier();
            }
        }
    }
}