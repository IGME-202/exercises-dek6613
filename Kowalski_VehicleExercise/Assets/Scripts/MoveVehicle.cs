using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveVehicle : MonoBehaviour
{
    Vector3 vehiclePosition;
    Vector3 direction = Vector3.right;
    Vector3 velocity = Vector3.zero;
    Vector3 acceleration = Vector3.zero;

    [SerializeField]
    float accelerationRate = 0.1f;

    [SerializeField]
    float decelerationRate = 0.5f;

    [SerializeField]
    float maxSpeed = 0.1f;

    [SerializeField]
    float turnSpeed = 1f;

    // Start is called before the first frame update
    void Start()
    {
        vehiclePosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // -- Player Input --

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            // Rotate counter-clockwise
            direction = Quaternion.Euler(0, 0, turnSpeed) * direction;
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            // Rotate clockwise
            direction = Quaternion.Euler(0, 0, -1f * turnSpeed) * direction;
        }

        if (Input.GetKey(KeyCode.UpArrow))
        {
            // Speed up

            // Calculate the acceleration vector
            acceleration = direction * accelerationRate;

            // Add acceleration to velocity
            velocity += acceleration;
        }
        else
        {
            // Slow down

            // Scale velocity by deceleration rate in a half-life curve
            velocity *= decelerationRate;
        }

        // Limit the velocity so it doesn’t move too quickly
        velocity = Vector3.ClampMagnitude(velocity, maxSpeed);

        // Add velocity to position
        vehiclePosition += velocity;

        // -- Screen wrap --

        // Get the main camera
        Camera mainCam = Camera.main;

        // Screen height equals 2 * cam's orthographic size
        float screenHeight = mainCam.orthographicSize * 2f;

        // Multiply screen height by aspect ratio to get screen width
        float screenWidth = screenHeight * mainCam.aspect;

        // Sets the X and Y bounds since the camera is centered at 0, 0 rather than having its top corner at 0, 0
        float xBound = screenWidth / 2f;
        float yBound = screenHeight / 2f;

        // Wrap horizontally
        if (vehiclePosition.x < -1f * xBound) { vehiclePosition.x = xBound; }
        else if (vehiclePosition.x > xBound) { vehiclePosition.x = -1f * xBound; }

        // Wrap vertically
        if (vehiclePosition.y < -1f * yBound) { vehiclePosition.y = yBound; }
        else if (vehiclePosition.y > yBound) { vehiclePosition.y = -1f * yBound; }

        // -- Finalize position --

        // Draw the vehicle at that position
        transform.position = vehiclePosition;

        // Set the vehicle’s rotation to match the direction
        transform.rotation = Quaternion.LookRotation(Vector3.forward, direction);
    }
}
