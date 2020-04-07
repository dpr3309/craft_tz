using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Craft_TZ.View
{
    internal class InfoManager : MonoBehaviour, IInfoManager
    {
        [SerializeField]
        private Text label = null;

        private const string END_GAME_MESSAGE = "Loose! score: ";
        private const string START_GAME_MESSAGE = "Tap to start";

        private IGameCore gameCore;

        [Inject]
        private void Construct(IGameCore gameCore)
        {
            this.gameCore = gameCore;
        }

        public void ClearMessage()
        {
            label.text = string.Empty;
        }

        public void EndOfGameMessage()
        {
            label.text = END_GAME_MESSAGE + gameCore.Score;
        }

        public void StartMessage()
        {
            label.text = START_GAME_MESSAGE;
        }
    }
}
