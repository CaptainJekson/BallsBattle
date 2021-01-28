using UnityEngine;

namespace Camera
{
    public class CameraDragger : MonoBehaviour
    {
        [SerializeField] private Transform _joint;
        [SerializeField] private float _speedRotate;

        private Touch _inputTouch;
        private float _rotationY;
        
        private void LateUpdate()
        {
            foreach (var touch in Input.touches)
            {
                if (touch.phase == TouchPhase.Began)
                {
                    _inputTouch = touch;
                }
                else if (touch.phase == TouchPhase.Moved)
                {
                    var deltaX = _inputTouch.position.x - touch.position.x;
                    var jointAngles = _joint.transform.eulerAngles;
                    _rotationY -= deltaX * Time.deltaTime * _speedRotate;
                    _joint.transform.eulerAngles = new Vector3(jointAngles.x, _rotationY, jointAngles.z);
                }
                else if (touch.phase == TouchPhase.Ended)
                {
                    _inputTouch = new Touch();
                }
            }
        }
    }
}