using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateToggleButton : MonoBehaviour
{
    public GameObject clockHand;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnMouseDown()
    {
        Vector2 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // If the mouse is within the box, toggle rotation
        if (GetComponent<BoxCollider2D>().OverlapPoint(mouseWorldPos))
        {
            // A bit messy, but this just means "switch the clock hand's rotate script from on to off or vice versa"
            clockHand.GetComponent<RotateHand>().rotate = !clockHand.GetComponent<RotateHand>().rotate;
        }
    }
}
