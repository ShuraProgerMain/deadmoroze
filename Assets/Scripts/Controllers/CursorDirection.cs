using UnityEngine;

namespace Controllers
{
    public class CursorDirection : MonoBehaviour
    {

        private void Awake() 
        {
            Cursor.visible = false;
        }

        private void FixedUpdate()
        {
            var ray = UnityEngine.Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;
            if(Physics.Raycast(ray, out hit, 1000))
            {
                transform.position = new Vector3(hit.point.x, 0.5f, hit.point.z);
            }

            transform.LookAt(UnityEngine.Camera.main.transform.position);
        }
    }
}
