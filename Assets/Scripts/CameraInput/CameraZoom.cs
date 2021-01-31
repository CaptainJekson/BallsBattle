using UnityEngine;

namespace CameraInput
{
    [RequireComponent(typeof(UnityEngine.Camera))]
    public class CameraZoom : MonoBehaviour
    {
        [SerializeField] [Range(0.0f, 1.0f)] private float _zoomSpeed;
        [SerializeField] private float _minZoom;
        [SerializeField] private float _maxZoom;

        private UnityEngine.Camera _camera;

        private void Awake()
        {
            _camera = GetComponent<UnityEngine.Camera>();
        }

        private void Update()
        {
            ZoomCamera();
        }

        private void ZoomCamera()
        {
#if UNITY_EDITOR
            var inputDif = Input.GetAxis("Mouse ScrollWheel");

            if (inputDif != 0)
            {
                var size = _camera.fieldOfView + inputDif * 15;
                size = Mathf.Clamp(size, _minZoom, _maxZoom);
                _camera.fieldOfView = size;
            }
#endif

            if (Input.touchSupported && Input.touchCount == 2)
            {
                var touchZero = Input.GetTouch(0);
                var touchOne = Input.GetTouch(1);

                var zeroPrevious = touchZero.position - touchZero.deltaPosition;
                var onePrevious = touchOne.position - touchOne.deltaPosition;

                var oldTouchDistance = Vector2.Distance(zeroPrevious, onePrevious);
                var currentTouchDistance = Vector2.Distance(touchZero.position, touchOne.position);
                var deltaDistance = oldTouchDistance - currentTouchDistance;
                var size = _camera.fieldOfView + deltaDistance * _zoomSpeed;

                size = Mathf.Clamp(size, _minZoom, _maxZoom);
                _camera.fieldOfView = size;
            }
        }
    }
}