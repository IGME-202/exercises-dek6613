using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClockNumbers : MonoBehaviour
{
    public GameObject numberPrefab;
    private GameObject[] numberObjects;

    // Start is called before the first frame update
    void Start()
    {
        numberObjects = new GameObject[12];

        for (int i=0; i<12; i++)
        {
            // Instantiates the number objects and stores them
            numberObjects[i] = Object.Instantiate(numberPrefab);

            // Moves the numbers such that they are positioned counter-clockwise starting at the top 
            numberObjects[i].transform.position = 
                new Vector3(
                    Mathf.Cos(Mathf.PI / 6f * (i) + (Mathf.PI / 2f)) * 2.3f, // Rotates in intervals of pi/6 (30 degrees), offset by pi/2 (90 degrees) so i=0 appears at the top
                    Mathf.Sin(Mathf.PI / 6f * (i) + (Mathf.PI / 2f)) * 2.3f,
                    0);

            // Makes the numbers display counting backwards from 12, since the unit circle typically goes counterclockwise
            numberObjects[i].GetComponentInChildren<TextMesh>().text = ""+ (12 - i);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
