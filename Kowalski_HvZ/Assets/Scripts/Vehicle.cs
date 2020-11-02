using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Vehicle : MonoBehaviour
{
    protected Vector3 position;
    protected Vector3 direction;
    protected Vector3 velocity;
    protected Vector3 acceleration;

    [Min(0.0001f)]
    public float mass = 1f;
    public float radius = 1f;
    public float maxSpeed = 1f;
    public float maxForce = 1f;

    // Start is called before the first frame update
    void Start()
    {
        position = transform.position;
        direction = Vector3.right;
        velocity = Vector3.zero;
        acceleration = Vector3.zero;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        // Implemented in child classes, but this determines the vehicle's acceleration based on its goals
        CalcSteeringForces();

        // Kinematics calculations
        velocity += acceleration * Time.deltaTime;
        position += velocity * Time.deltaTime;

        BounceEdges();

        // Apply the calculated position to the game object's position
        transform.position = position;

        // Calculate direction (not currently used)
        direction = velocity.normalized;

        // Reset acceleration for next frame
        acceleration = Vector3.zero;
    }

    /// <summary>
    /// Adds a force to this vehicle
    /// </summary>
    /// <param name="force">Force to apply</param>
    protected void ApplyForce(Vector3 force)
    {
        acceleration += force / mass;
    }

    /// <summary>
    /// If the vehicle would go out of bounds, bounce off of invisible "walls" at the edges instead
    /// </summary>
    void BounceEdges()
    {
        // Hard-coded values based on the floor plane's size
        float xBound = 20f;
        float zBound = 20f;

        // Check for x bounds
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

        // Check for z bounds
        if (position.z > zBound)
        {
            position.z = zBound;
            velocity.z *= -1;
        }
        else if (position.z < -zBound)
        {
            position.z = -zBound;
            velocity.z *= -1;
        }
    }

    protected abstract void CalcSteeringForces();

    #region Seek Logic
    /// <summary>
    /// Gets the seeking force for this vehicle, targeting a position
    /// </summary>
    /// <param name="targetPos">Position to seek</param>
    /// <returns>Seeking force</returns>
    public Vector3 Seek(Vector3 targetPos)
    {
        Vector3 desiredVel = targetPos - position;

        desiredVel.y = 0;

        desiredVel = desiredVel.normalized * maxSpeed;

        Vector3 seekingForce = desiredVel - velocity;

        return seekingForce;
    }

    /// <summary>
    /// Gets the seeking force for this vehicle, targeting an object
    /// </summary>
    /// <param name="target">Object to seek</param>
    /// <returns>Seeking force</returns>
    public Vector3 Seek(GameObject target)
    {
        return Seek(target.transform.position);
    }
    #endregion

    #region Flee Logic
    /// <summary>
    /// Gets the fleeing force for this vehicle, targeting a position
    /// </summary>
    /// <param name="targetPos">Position to flee from</param>
    /// <returns>Fleeing force</returns>
    public Vector3 Flee(Vector3 targetPos)
    {
        Vector3 desiredVel = position - targetPos;

        desiredVel.y = 0;

        desiredVel = desiredVel.normalized * maxSpeed;

        Vector3 fleeingForce = desiredVel - velocity;

        return fleeingForce;
    }

    /// <summary>
    /// Gets the fleeing force for this vehicle, targeting an object
    /// </summary>
    /// <param name="target">Object to flee from</param>
    /// <returns>Fleeing force</returns>
    public Vector3 Flee(GameObject target)
    {
        return Flee(target.transform.position);
    }
    #endregion
}
