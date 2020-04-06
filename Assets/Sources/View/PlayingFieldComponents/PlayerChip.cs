using System;
using Craft_TZ.Model.CoordinateModifiers;
using UnityEngine;
using Zenject;

namespace Craft_TZ.View
{
    internal class PlayerChip : MonoBehaviour, IPlayerChip
    {
        [SerializeField]
        private float speed;

        private PlayingFieldManager gameLoopManager;

        private bool isConstructed = false;

        private Vector3 startPosition;

        private ICoordinateModifierManager coordinateModifierManager;

        private Action moveAction;

        [Inject]
        private void Construct(ICoordinateModifierManager coordinateModifierManager, PlayingFieldManager gameLoopManager)
        {
            if (isConstructed)
                throw new Exception($"[{GetType().Name}.Construct] object already constructed");

            this.coordinateModifierManager = coordinateModifierManager;
            this.gameLoopManager = gameLoopManager;
            isConstructed = true;
        }

        private void Start()
        {
            startPosition = this.transform.position;
        }

        private void Update()
        {
            moveAction?.Invoke();
        }

        public void StartMove()
        {
            moveAction = () =>
            {
                transform.position = coordinateModifierManager.TransformCoordinates(transform.position, speed);
                gameLoopManager.Step(new Vector2(transform.position.x, transform.position.z));
            };
        }

        public void StopMove()
        {
            moveAction = null;
        }

        public void ChangeDirection()
        {
            coordinateModifierManager.ChengeCoordinateModifier();
        }

        public void Restart()
        {
            transform.position = startPosition;
            coordinateModifierManager.ResetCoordinateModifier();
        }
    }
}