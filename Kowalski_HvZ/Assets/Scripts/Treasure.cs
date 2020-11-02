using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Treasure : MonoBehaviour
{
    public void OnGrab()
    {
        // Hard coded values based on floor plane size
        float xBound = 20f;
        float zBound = 20f;

        // Teleport to a random position within bounds
        transform.position = new Vector3(Random.Range(-xBound, xBound), transform.position.y, Random.Range(-zBound, zBound));
    }
}
