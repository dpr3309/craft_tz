using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Craft_TZ.View
{
    internal class InputManager : MonoBehaviour
    {
        [SerializeField]
        private Button button = null;

        private IGameCore gameCore;

        [Inject]
        private void Construct(IGameCore gameCore)
        {
            this.gameCore = gameCore;
        }

        private void Start()
        {
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(() => gameCore.HandleInputClick());
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                gameCore.HandleInputClick();
            }
        }
    }
}
