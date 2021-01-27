using Game;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class ProgressBarBattleView : View
    {
        [SerializeField] private Image _playerFiller;
        [SerializeField] private Image _enemyFiller;

        private float _maxValue;

        public override void Init()
        {
            GameListener.GameStarted += ShowStartingValues;
            GameListener.QuantityOfBallsChanged += ShowValue;
        }

        private void ShowStartingValues(float playerBalls, float enemyBalls)
        {
            _maxValue = playerBalls > enemyBalls ? playerBalls : enemyBalls;
            _playerFiller.fillAmount = playerBalls / _maxValue;
            _enemyFiller.fillAmount = enemyBalls / _maxValue;
        }

        private void ShowValue(float playerBalls, float enemyBalls)
        {
            _playerFiller.fillAmount = playerBalls / _maxValue;
            _enemyFiller.fillAmount = enemyBalls / _maxValue;
        }
    }
}