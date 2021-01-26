using System;
using System.Collections.Generic;
using Unit;
using UnityEngine;

namespace Game
{
    [RequireComponent(typeof(BallsCreator))]
    public class GameListener : MonoBehaviour
    {
        private BallsCreator _ballsCreator;
        private List<Ball> _balls;

        private int _playerBalls;
        private int _enemyBalls;

        public IEnumerable<Ball> Balls => _balls;

        public event Action<int, int> QuantityOfBallsChanged;

        private void Awake()
        {
            _balls = new List<Ball>();
            _ballsCreator = GetComponent<BallsCreator>();
        }

        private void OnEnable()
        {
            this.QuantityOfBallsChanged += ShowQuantityOfBalls;
        }

        private void OnDisable()
        {
            this.QuantityOfBallsChanged -= ShowQuantityOfBalls;
        }

        public void AddBall(Ball ball)
        {
            if (ball.IsEnemy)
                _enemyBalls++;
            else
                _playerBalls++;

            _balls.Add(ball);
            
            QuantityOfBallsChanged?.Invoke(_playerBalls, _enemyBalls);
        }

        public void RemoveBall(Ball ball)
        {
            if (ball.IsEnemy)
                _enemyBalls--;
            else
                _playerBalls--;

            _balls.Remove(ball);
            
            QuantityOfBallsChanged?.Invoke(_playerBalls, _enemyBalls);
        }

        private void ShowQuantityOfBalls(int playerBalls, int enemyBalls)
        {
            Debug.LogError($"playerBalls = {playerBalls} | enemyBalls = {enemyBalls}");
        }
    }
}