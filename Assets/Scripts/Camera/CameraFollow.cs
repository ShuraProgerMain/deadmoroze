using UnityEngine;

namespace Camera
{
    public class CameraFollow : MonoBehaviour
    {
        [SerializeField] private Transform _target;
        [SerializeField] private float _smoothSpeed = 0.125f;
        [SerializeField] private Vector3 _offset;


        private void LateUpdate()
        {
            Vector3 desirePosition = _target.position + _offset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desirePosition, _smoothSpeed);
            transform.position = smoothedPosition;

            transform.LookAt(_target);
        }
    }
}
