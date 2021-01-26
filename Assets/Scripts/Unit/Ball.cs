using System;
using Game;
using UnityEngine;

namespace Unit
{
    [RequireComponent(typeof(MeshRenderer)), RequireComponent(typeof(Collider))]
    public class Ball : MonoBehaviour
    {
        [SerializeField] private Material _blueMaterial;
        [SerializeField] private Material _redMaterial;
        [SerializeField] [Range(0.01f, 0.5f)] private float _decreaseSpeed;

        private bool _isEnemy;
        private float _speed;
        private float _radius;
        private float _unitDestroyRadius;
        private Vector3 _direction;
        private MeshRenderer _meshRenderer;

        public bool IsEnemy => _isEnemy;

        private void Awake()
        {
            _meshRenderer = GetComponent<MeshRenderer>();
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
                return;

            Bounce(normal);
        }

        private void OnCollisionStay(Collision other)
        {
            if (other != null)
            {
                var anotherBall = other.gameObject.GetComponent<Ball>();

                if (anotherBall != null && anotherBall.IsEnemy != IsEnemy)
                {
                    Reduce();
                }
            }
        }

        public void Init(float speed, float radius, float unitDestroyRadius,bool isEnemy = false)
        {
            _isEnemy = isEnemy;
            _speed = speed;
            _radius = radius;
            _unitDestroyRadius = unitDestroyRadius;

            _meshRenderer.material = isEnemy ? _redMaterial : _blueMaterial;
            transform.localScale = Vector3.one * _radius;
        }
        
        public void StartMoving(Vector3 direction)
        {
            _direction = direction;
        }

        private void Bounce(Vector3 normal)
        {
            _direction = Vector3.Reflect(_direction, normal);
        }

        private void Reduce()
        {
            transform.localScale -= new Vector3(_decreaseSpeed, _decreaseSpeed, _decreaseSpeed);

            if (transform.localScale.x <= _unitDestroyRadius)
            {
                FindObjectOfType<GameListener>().RemoveBall(this);
                Destroy(gameObject);
            }
        }

        private void Move()
        {
            transform.position = Vector3.MoveTowards(transform.position, transform.position + _direction,
                _speed * Time.deltaTime);
        }
    }
}