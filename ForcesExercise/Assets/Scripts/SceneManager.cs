using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
    public GameObject redMonster;
    public GameObject blueMonster;
    public GameObject greenMonster;

    private GameObject[] monsters;

    public float mu = 1;

    // Start is called before the first frame update
    void Start()
    {
        // Get the main camera
        Camera mainCam = Camera.main;

        // Screen height equals 2 * cam's orthographic size
        float screenHeight = mainCam.orthographicSize * 2f;

        // Multiply screen height by aspect ratio to get screen width
        float screenWidth = screenHeight * mainCam.aspect;

        // Sets the X and Y bounds since the camera is centered at 0, 0 rather than having its top corner at 0, 0
        float xBound = screenWidth / 2f;
        float yBound = screenHeight / 2f;

        GameObject red = Instantiate(redMonster, new Vector3(Random.Range(-xBound, xBound), Random.Range(-yBound, yBound)), Quaternion.identity);
        GameObject blue = Instantiate(blueMonster, new Vector3(Random.Range(-xBound, xBound), Random.Range(-yBound, yBound)), Quaternion.identity);
        GameObject green = Instantiate(greenMonster, new Vector3(Random.Range(-xBound, xBound), Random.Range(-yBound, yBound)), Quaternion.identity);

        red.GetComponent<Vehicle>().mass = 1;
        blue.GetComponent<Vehicle>().mass = 5;
        green.GetComponent<Vehicle>().mass = 20;

        monsters = new GameObject[3];
        monsters[0] = red;
        monsters[1] = blue;
        monsters[2] = green;

        for (int i = 0; i < monsters.Length; i++)
        {
            monsters[i].GetComponent<Vehicle>().mu = mu;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            for (int i = 0; i < monsters.Length; i++)
            {
                monsters[i].GetComponent<Vehicle>().hasFriction = !monsters[i].GetComponent<Vehicle>().hasFriction;
            }
        }
    }
}
