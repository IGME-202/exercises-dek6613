using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vehicle : MonoBehaviour
{
    private Vector3 position;
    private Vector3 direction = Vector3.up;
    private Vector3 velocity;
    private Vector3 acceleration;
    public float mass;

    public float mu;

    public float mouseForceModifier = 1;
    public bool hasFriction = true;
     
    // Start is called before the first frame update
    void Start()
    {
        position = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        acceleration = Vector3.zero;

        ApplyMouseForce();
        UpdatePosition();
        BounceScreen();
        ApplyTransforms();
    }

    void UpdatePosition()
    {
        velocity += acceleration * Time.deltaTime;

        position += velocity * Time.deltaTime;

        direction = velocity.normalized;
    }

    void BounceScreen()
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

        if (position.x > xBound)
        {
            position.x = xBound;
            velocity.x *= -1;
        }
        else if (position.x < -xBound)
        {
            position.x = -xBound;
            velocity.x *= -1;
        }

        if (position.y > yBound)
        {
            position.y = yBound;
            velocity.y *= -1;
        }
        else if (position.y < -yBound)
        {
            position.y = -yBound;
            velocity.y *= -1;
        }
    }

    void ApplyTransforms()
    {
        transform.position = position;
        transform.rotation = Quaternion.LookRotation(Vector3.forward, direction);
    }

    void ApplyForce(Vector3 force)
    {
        acceleration += force / mass;
    }

    void ApplyFriction(float coeff)
    {
        Vector3 friction = velocity * -1;
        friction.Normalize();
        friction *= coeff;
        acceleration += friction;
    }

    void ApplyMouseForce()
    {
        if (Input.GetMouseButton(0))
        {
            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mouseWorldPos.z = transform.position.z;

            ApplyForce((position - mouseWorldPos) * mouseForceModifier);
        }
        else
        {
            ApplyFriction(mu);

            if (velocity.sqrMagnitude <= 0.0001f)
            {
                Vector3.ClampMagnitude(velocity, 0);
            }
        }
    }
}
