using Creators;
using Serializable;
using UnityEngine;

namespace Game
{
    [RequireComponent(typeof(GameListener), (typeof(BallsCreator)))]
    public class GameSaver : MonoBehaviour
    {
        private Save _save;
        private GameListener _gameListener;
        private BallsCreator _ballsCreator;
        
        private void Awake()
        {
            _gameListener = GetComponent<GameListener>();
            _ballsCreator = GetComponent<BallsCreator>();
        }

        public void Save()
        {
            _save = new Save();
            
            foreach (var enemyBall in _gameListener.EnemyBalls)
            {
                _save.BallSaveDatas.Add(enemyBall.GetSaveData());
            }
            
            foreach (var enemyBall in _gameListener.PlayerBalls)
            {
                _save.BallSaveDatas.Add(enemyBall.GetSaveData());
            }
            
            PlayerPrefs.SetString("Save", JsonUtility.ToJson(_save));
        }

        public void Load()
        {
            _gameListener.ClearAllBalls();
            
            _save = JsonUtility.FromJson<Save>(PlayerPrefs.GetString("Save"));

            foreach (var ballSaveData in _save.BallSaveDatas)
            {
                var spawnedBall = Instantiate(_ballsCreator.BallPrefab, transform.position, Quaternion.identity);

                spawnedBall.transform.position = ballSaveData.Position;
                spawnedBall.Init(ballSaveData.Speed, ballSaveData.Radius, ballSaveData.UnitDestroyRadius, 
                    ballSaveData.IsEnemy);
                spawnedBall.StartMoving(ballSaveData.Direction);
                
                _gameListener.AddBall(spawnedBall);
                
            }
        }
    }
}