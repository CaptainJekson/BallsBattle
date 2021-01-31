using UnityEngine;

namespace Creators
{
    [RequireComponent(typeof(InitGame))]
    public class WorldCreator : MonoBehaviour
    {
        [SerializeField] private GameObject _groundPrefab;

        private GameObject _ground;
        private InitGame _initGame;

        private void Awake()
        {
            _initGame = GetComponent<InitGame>();
        }

        private void Start()
        {
            SpawnGround();
        }

        private void SpawnGround()
        {
            _ground = Instantiate(_groundPrefab, transform);
            _ground.transform.localScale = new Vector3(_initGame.GameConfig.gameAreaHeight,
                _initGame.GameConfig.gameAreaWidth, 1.0f);
        }
    }
}