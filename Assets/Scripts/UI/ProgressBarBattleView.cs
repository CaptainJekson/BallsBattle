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
        }

        private void Update()
        {
            UpdateValue();
        }

        private void ShowStartingValues(float playerBalls, float enemyBalls)
        {
            _maxValue = playerBalls > enemyBalls ? playerBalls : enemyBalls;
            _playerFiller.fillAmount = playerBalls / _maxValue;
            _enemyFiller.fillAmount = enemyBalls / _maxValue;
            
            Open();
        }

        private void UpdateValue()
        {
            _playerFiller.fillAmount = GameListener.SumOfPlayerRadius / _maxValue;
            _enemyFiller.fillAmount = GameListener.SumOfEnemyRadius / _maxValue;
            
            //Debug.LogError($" RedRadiusBalls = {GameListener.SumOfEnemyRadius} BlueRadiusBalls = {GameListener.SumOfPlayerRadius} MaxValue = {_maxValue}");
        }
    }
}