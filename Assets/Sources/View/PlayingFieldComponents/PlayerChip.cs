using System;
using Craft_TZ.Model.CoordinateModifiers;
using Craft_TZ.Model.Enums;
using UnityEngine;
using Zenject;

namespace Craft_TZ.View
{
    internal class PlayerChip : MonoBehaviour, IPlayerChip
    {
        [SerializeField]
        private float speed = 1;

        [SerializeField]
        private float radius = 1;

        public float Radius => radius;

        public PlayerChipType ChipType => PlayerChipType.Circle;

        private GameCore gameLoopManager;

        private bool isConstructed = false;

        private Vector3 startPosition;

        private ICoordinateModifierManager coordinateModifierManager;

        private Action moveAction;

        [Inject]
        private void Construct(ICoordinateModifierManager coordinateModifierManager, GameCore gameLoopManager)
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

        public void StartFall()
        {
            moveAction = () =>
            {
                transform.position = coordinateModifierManager.TransformCoordinatesFall(transform.position, speed);
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