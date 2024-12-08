using UnityEngine;

namespace Player
{
    public class MouseTracking : MonoBehaviour
    {

        public Texture2D _cursor;
        public Vector3 tPos;
        public float speed = 5f;

        [SerializeField] private LayerMask _checkLayer;
        private UnityEngine.Camera m_Camera;
        private Transform m_Transform;

        private void Start() 
        {
            m_Camera = UnityEngine.Camera.main;
            m_Transform = GetComponent<Transform>();
        }

        private void Update()
        {
            Ray ray = m_Camera.ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast(ray, out RaycastHit hitInfo, 500.0f, _checkLayer))
            {
                Vector3 lookPosition = new Vector3(hitInfo.point.x, m_Transform.position.y, hitInfo.point.z);
                tPos = Vector3.Lerp(tPos, lookPosition, Time.deltaTime * speed);
                m_Transform.LookAt(tPos);
            }
        }
    }
}
