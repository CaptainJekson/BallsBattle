using System.Collections;
using Unit;
using UnityEngine;

namespace Game
{
    [RequireComponent(typeof(InitGame)), RequireComponent(typeof(GameListener))]
    public class BallsCreator : MonoBehaviour
    {
        [SerializeField] private Ball _ballPrefab;
        [SerializeField] [Range(0.5f, 5.0f)] float _ballHeight;
        
        private InitGame _initGame;
        private GameListener _gameListener;

        private void Awake()
        {
            _initGame = GetComponent<InitGame>();
            _gameListener = GetComponent<GameListener>();
        }

        private void Start()
        {
            StartCoroutine(UnitMoveDelay());
        }
        
        private IEnumerator UnitMoveDelay()
        {
            yield return StartCoroutine(UnitSpawnDelay(false));
            yield return StartCoroutine(UnitSpawnDelay(true));

            foreach (var ball in _gameListener.Balls)
            {
                var randomDirection = new Vector3(Random.Range(-1.0f, 1.0f), 0.0f, Random.Range(-1.0f, 1.0f));
                ball.StartMoving(randomDirection);
            }
            
            _gameListener.OnStartGame();
        }
        
        private IEnumerator UnitSpawnDelay(bool isEnemy)
        {
            for (var i = 0; i < _initGame.GameConfig.numUnitsToSpawn; i++)
            {
                yield return new WaitForSeconds(_initGame.GameConfig.unitSpawnDelay);

                var randomSpeed = Random.Range(_initGame.GameConfig.unitSpawnMinSpeed,
                    _initGame.GameConfig.unitSpawnMaxSpeed);

                var randomRadius = Random.Range(_initGame.GameConfig.unitSpawnMinRadius,
                    _initGame.GameConfig.unitSpawnMaxRadius);

                var height = _initGame.GameConfig.gameAreaHeight;
                var width = _initGame.GameConfig.gameAreaWidth;

                var randomSpawnPosition = new Vector3(Random.Range(-(height / 2), height / 2),
                    _ballHeight, Random.Range(-(width) / 2, width / 2));

                var spawnedBall = Instantiate(_ballPrefab, transform.position, Quaternion.identity);

                spawnedBall.transform.position = randomSpawnPosition;
                spawnedBall.Init(randomSpeed, randomRadius, _initGame.GameConfig.unitDestroyRadius, isEnemy);
                _gameListener.AddBall(spawnedBall);
            }
        }
    }
}