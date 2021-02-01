using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorDirection : MonoBehaviour
{

    private void Awake() 
    {
        Cursor.visible = false;
    }

    private void FixedUpdate()
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;
        if(Physics.Raycast(ray, out hit, 1000))
        {
            transform.position = new Vector3(hit.point.x, 0.5f, hit.point.z);
        }

        transform.LookAt(Camera.main.transform.position);
    }
}
