using Craft_TZ.Model.CoordinateHandlers;
using Craft_TZ.Model.CoordinateModifiers;
using UnityEngine;

namespace Craft_TZ.View
{
    internal class PlayerChip : MonoBehaviour
    {
        [SerializeField]
        private float speed;

        private ICoordinateModifier coordinateModifier;
        private ICoordinateModifier[] modifiers;

        //private IGameLoopManager gameLoopManager;
        [SerializeField]
        private PlayingFieldManager gameLoopManager;

        private void Start()
        {
            modifiers = new ICoordinateModifier[2];
            modifiers[0] = new ForwardCoordinateModifier();
            modifiers[1] = new RightCoordinateModifier();
            index = 0;
            coordinateModifier = modifiers[index];
        }

        private int index = 0;
        private void Update()
        {
            transform.position = coordinateModifier.TransformCoordinates(transform.position, speed);
            gameLoopManager.Step(new Vector2(transform.position.x, transform.position.z));

            if(Input.GetKeyDown(KeyCode.Space))
            {
                index = index == 0 ? 1 : 0;
                coordinateModifier = modifiers[index];
            }
        }

    }
}