using Common.Scripts.Planet;
using UnityEngine;

namespace Common.Scripts.Rocket
{
    public class EndOfGameController: MonoBehaviour,IGameStateDependable
    {
        private EndOfGameUI _endOfGameUI;
        private EndOfGameModel _endOfGameModel;
        private IWindow _endOfGameWindow;
        public void OnGameStateSwitch(GameState gameState)
        {
            if (gameState == GameState.EndOfGame)
            {
                _endOfGameUI = new EndOfGameUI(CreateUI);
                _endOfGameWindow = _endOfGameUI.InstantiateUI();
                _endOfGameWindow.Show();
            }
        }
        private IWindow CreateUI()
        {
            IWindow window = Instantiate(_endOfGameModel.EndOfGameWindow).GetComponent<IWindow>();
            if (window != null)
            {
                return window;
            }
            return default;
        }
    }
}