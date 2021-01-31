using Configs;
using Game;
using Serializable;
using UnityEngine;

namespace Unit
{
    [RequireComponent(typeof(MeshRenderer)), RequireComponent(typeof(Collider))]
    public class Ball : MonoBehaviour
    {
        [SerializeField] private Material _blueMaterial;
        [SerializeField] private Material _redMaterial;
        [SerializeField] private BallEffectsConfig _ballEffectsConfig;

        private bool _isEnemy;
        private float _speed;
        private float _unitDestroyRadius;
        private Vector3 _direction;
        private MeshRenderer _meshRenderer;
        private GameListener _gameListener;

        public bool IsEnemy => _isEnemy;
        public bool IsTriggered { get; private set; }
        public float Radius { get; private set; }

        private void Awake()
        {
            _meshRenderer = GetComponent<MeshRenderer>();
            _gameListener = FindObjectOfType<GameListener>();
        }

        private void Update()
        {
            Move();
        }

        private void OnCollisionEnter(Collision other)
        {
            var normal = new Vector3(other.contacts[0].normal.x, 0.0f, other.contacts[0].normal.z);

            var anotherBall = other.gameObject.GetComponent<Ball>();

            if (anotherBall != null && anotherBall.IsEnemy != IsEnemy)
            {
                this.PlayEffect(_ballEffectsConfig.Reduce);
                return;
            }

            Bounce(normal);
        }

        private void OnCollisionStay(Collision other)
        {
            if (other != null)
            {
                var anotherBall = other.gameObject.GetComponent<Ball>();

                if (anotherBall != null && anotherBall.IsEnemy != IsEnemy)
                {
                    Reduce(anotherBall);
                }
            }
        }

        public void Init(float speed, float radius, float unitDestroyRadius, bool isEnemy = false)
        {
            _isEnemy = isEnemy;
            _speed = speed;
            _unitDestroyRadius = unitDestroyRadius;
            Radius = radius;

            _meshRenderer.material = isEnemy ? _redMaterial : _blueMaterial;
            transform.localScale = Vector3.one * (radius * 2);
        }

        public void StartMoving(Vector3 direction)
        {
            _direction = direction;
        }

        public void PlaySpawnEffect()
        {
            this.PlayEffect(_ballEffectsConfig.Spawn);
        }

        private void Bounce(Vector3 normal)
        {
            _direction = Vector3.Reflect(_direction, normal);
            this.PlayEffect(_ballEffectsConfig.Collision);
        }

        public BallSaveData GetSaveData()
        {
            var newBallData = new BallSaveData(transform.position, Radius, _unitDestroyRadius,
                _speed, _direction, IsEnemy);

            return newBallData;
        }

        private void Reduce(Ball anotherBall)
        {
            var distance = Vector3.Distance(anotherBall.transform.position, transform.position);

            var currentRadius1 = anotherBall.transform.localScale.x / 2;
            var currentRadius2 = transform.localScale.x / 2;
            var halfRadius = ((currentRadius1 + currentRadius2) - distance) / 2;
            var newRadius1 = currentRadius1 - halfRadius;
            var newRadius2 = currentRadius2 - halfRadius;

            if (newRadius1 < currentRadius1)
            {
                anotherBall.transform.localScale = Vector3.one * newRadius1 * 2;
                anotherBall.Radius = anotherBall.transform.localScale.x / 2;
            }

            if (newRadius2 < currentRadius2)
            {
                transform.localScale = Vector3.one * newRadius2 * 2;
                Radius = anotherBall.transform.localScale.x / 2;
            }

            if (transform.localScale.x <= _unitDestroyRadius)
                Destroy();
        }

        private void Destroy()
        {
            this.PlayEffect(_ballEffectsConfig.Destroy, _meshRenderer.material.color);
            _gameListener.RemoveBall(this);
            Destroy(gameObject);
        }

        private void Move()
        {
            transform.position = Vector3.MoveTowards(transform.position, transform.position + _direction,
                _speed * Time.deltaTime);
        }
    }
}