using Configs;
using Game;
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
        private float _radius;
        private float _unitDestroyRadius;
        private Vector3 _direction;
        private MeshRenderer _meshRenderer;
        private GameListener _gameListener;

        public bool IsEnemy => _isEnemy;
        public float Radius => _radius;

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
                PlayEffect(_ballEffectsConfig.Reduce);
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

        private void OnDestroy()
        {
            _gameListener.RemoveBall(this);
        }

        public void Init(float speed, float radius, float unitDestroyRadius, bool isEnemy = false)
        {
            _isEnemy = isEnemy;
            _speed = speed;
            _radius = radius;
            _unitDestroyRadius = unitDestroyRadius;

            _meshRenderer.material = isEnemy ? _redMaterial : _blueMaterial;
            transform.localScale = Vector3.one * (_radius * 2);

            PlayEffect(_ballEffectsConfig.Spawn);
        }

        public void StartMoving(Vector3 direction)
        {
            _direction = direction;
        }

        private void Bounce(Vector3 normal)
        {
            _direction = Vector3.Reflect(_direction, normal);
            PlayEffect(_ballEffectsConfig.Collision);
        }

        private void Reduce(Ball anotherBall)
        {
            var distance = Vector3.Distance(anotherBall.transform.position, transform.position);

            var halfRadius = ((_radius + anotherBall.Radius) - distance) / 2;

            var newRadius1 = _radius - halfRadius;
            var newRadius2 = anotherBall.Radius - halfRadius;

            if (newRadius1 < _radius)
                anotherBall.transform.localScale = Vector3.one * newRadius1 * 2;

            if (newRadius2 < anotherBall.Radius)
                transform.localScale = Vector3.one * newRadius2 * 2;

            if (transform.localScale.x <= _unitDestroyRadius)
                Destroy();
        }

        private void Destroy()
        {
            PlayColorEffect(_ballEffectsConfig.Destroy, _meshRenderer.material.color);
            Destroy(gameObject);
        }

        private void Move()
        {
            transform.position = Vector3.MoveTowards(transform.position, transform.position + _direction,
                _speed * Time.deltaTime);
        }

        //TODO Подумать над выделением класса
        private void PlayEffect(ParticleSystem effect)
        {
            var spawnedEffect = Instantiate(effect, transform.position,
                Quaternion.identity);
            spawnedEffect.transform.SetParent(transform);
            Destroy(spawnedEffect.gameObject, spawnedEffect.main.duration);
        }

        private void PlayColorEffect(ParticleSystem effect, Color color)
        {
            var spawnedEffect = Instantiate(effect, transform.position,
                Quaternion.identity);

            var mainModule = spawnedEffect.main;
            mainModule.startColor = color;

            Destroy(spawnedEffect.gameObject, mainModule.duration);
        }
    }
}