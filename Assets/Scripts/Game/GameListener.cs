using System;
using System.Collections.Generic;
using Unit;
using UnityEngine;

namespace Game
{
    public class GameListener : MonoBehaviour
    {
        private List<Ball> _balls;

        private float _sumOfPlayerRadius;
        private float _sumOfEnemyRadius;

        private int _playerBalls;
        private int _enemyBalls;
        private DateTime _startGameTime;

        public IEnumerable<Ball> Balls => _balls;

        public static event Action<float, float> QuantityOfBallsChanged;
        public static event Action<float, float> GameStarted;
        public static event Action<bool, TimeSpan> GameOver;

        private void Awake()
        {
            _startGameTime = DateTime.Now;
            _balls = new List<Ball>();
        }

        public void OnStartGame()
        {
            GameStarted?.Invoke(_sumOfPlayerRadius, _sumOfEnemyRadius);
        }

        public void AddBall(Ball ball)
        {
            if (ball.IsEnemy)
            {
                _sumOfEnemyRadius += ball.Radius;
                _enemyBalls++;
            }
            else
            {
                _sumOfPlayerRadius += ball.Radius;
                _playerBalls++;
            }

            _balls.Add(ball);
        }

        public void RemoveBall(Ball ball)
        {
            if (ball.IsEnemy)
            {
                _sumOfEnemyRadius -= ball.Radius;
                _enemyBalls--;
            }
            else
            {
                _sumOfPlayerRadius -= ball.Radius;
                _playerBalls--;
            }

            _balls.Remove(ball);
            
            QuantityOfBallsChanged?.Invoke(_sumOfPlayerRadius, _sumOfEnemyRadius);
            
            Debug.Log($"Player ball - {_playerBalls} Enemy ball - {_enemyBalls}");
            
            if(_enemyBalls <= 0 || _playerBalls <= 0)
                GameOver?.Invoke(_enemyBalls <= 0, DateTime.Now - _startGameTime);
        }
    }
}