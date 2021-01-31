using System;
using System.Collections.Generic;
using Unit;
using UnityEngine;

namespace Game
{
    public class GameListener : MonoBehaviour
    {
        private List<Ball> _playerBalls;
        private List<Ball> _enemyBalls;

        private float _sumOfPlayerRadius;
        private float _sumOfEnemyRadius;
        private DateTime _startGameTime;

        public IEnumerable<Ball> PlayerBalls => _playerBalls;
        public IEnumerable<Ball> EnemyBalls => _enemyBalls;
        public static float SumOfEnemyRadius { get; private set; } = 0.0f;
        public static float SumOfPlayerRadius { get; private set; } = 0.0f;
        
        public event Action<float, float> GameStarted;
        public event Action<bool, TimeSpan> GameOver;

        private void Awake()
        {
            _startGameTime = DateTime.Now;
            _enemyBalls = new List<Ball>();
            _playerBalls = new List<Ball>();
        }

        private void Update()
        {
            RefreshRadius();
        }

        private void RefreshRadius()
        {
            SumOfEnemyRadius = 0.0f;
            SumOfPlayerRadius = 0.0f;
            
            foreach (var ball in _enemyBalls)
            {
                SumOfEnemyRadius += ball.Radius;
            }

            foreach (var ball in _playerBalls)
            {
                SumOfPlayerRadius += ball.Radius;
            }
        }

        public void OnStartGame()
        {
            var startRedRadiusBall = 0.0f;
            var startBlueRadiusBall = 0.0f;
            
            foreach (var ball in _enemyBalls)
            {
                if (ball.IsEnemy)
                    startRedRadiusBall += ball.Radius;
                else
                    startBlueRadiusBall += ball.Radius;
            }

            GameStarted?.Invoke(startBlueRadiusBall, startRedRadiusBall);
        }

        public void AddBall(Ball ball)
        {
            if(ball.IsEnemy)
                _enemyBalls.Add(ball);
            else
                _playerBalls.Add(ball);
        }

        public void RemoveBall(Ball ball)
        {
            if(ball.IsEnemy)
                _enemyBalls.Remove(ball);
            else
                _playerBalls.Remove(ball);

            if (_enemyBalls.Count > 0 && _playerBalls.Count > 0) return;
            GameOver?.Invoke(_enemyBalls.Count <= 0, DateTime.Now - _startGameTime);
            ClearAllBalls();
        }

        public void ClearAllBalls()
        {
            foreach (var ball in _playerBalls)
            {
                Destroy(ball.gameObject);
            }

            foreach (var ball in _enemyBalls)
            {
                Destroy(ball.gameObject);
            }
            
            _playerBalls.Clear();
            _enemyBalls.Clear();
        }
    }
}