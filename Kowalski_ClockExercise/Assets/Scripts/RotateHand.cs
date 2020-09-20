using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateHand : MonoBehaviour
{
    public bool rotate = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (rotate)
        {
            RotateToMouse();
        }
    }

    void RotateToMouse()
    {
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        float angle = Mathf.Atan2(mouseWorldPos.y, mouseWorldPos.x);

        // Offset by pi radians, or 180 degrees
        angle -= Mathf.PI;

        // Convert from radians to degrees
        angle *= 180 / Mathf.PI;

        transform.rotation = Quaternion.Euler(0, 0, angle);
    }
}
