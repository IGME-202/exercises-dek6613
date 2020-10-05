using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetection : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Checks for collisions between two game objects using AABB detection
    /// </summary>
    /// <param name="one">First game object</param>
    /// <param name="two">Second game object</param>
    /// <returns>True if colliding</returns>
    public static bool AABBCollision(GameObject one, GameObject two)
    {
        SpriteRenderer oneRenderer = one.GetComponent<SpriteRenderer>();
        SpriteRenderer twoRenderer = two.GetComponent<SpriteRenderer>();

        bool collidingX = false;
        bool collidingY = false;

        // Checks if there could be a collision horizontally
        if (twoRenderer.bounds.min.x < oneRenderer.bounds.max.x &&
            twoRenderer.bounds.max.x > oneRenderer.bounds.min.x)
        {
            collidingX = true;
        }

        // Checks if there could be a collision vertically
        if (twoRenderer.bounds.min.y < oneRenderer.bounds.max.y &&
            twoRenderer.bounds.max.y > oneRenderer.bounds.min.y)
        {
            collidingY = true;
        }

        // If the boxes could be colliding vertically and horizontally, then the boxes are colliding
        return collidingX && collidingY;
    }

    /// <summary>
    /// Checks for collisions between two game objects using radius comparison
    /// </summary>
    /// <param name="one">First game object</param>
    /// <param name="two">Second game object</param>
    /// <returns>True if colliding</returns>
    public static bool CircleCollision(GameObject one, GameObject two)
    {
        SpriteRenderer oneRenderer = one.GetComponent<SpriteRenderer>();
        SpriteRenderer twoRenderer = two.GetComponent<SpriteRenderer>();

        float oneRad = oneRenderer.bounds.extents.magnitude;
        float twoRad = twoRenderer.bounds.extents.magnitude;

        float radiiSquared = Mathf.Pow(oneRad + twoRad, 2);

        // Since these are squared, the direction doesn't matter
        float xDiffSquared = Mathf.Pow(oneRenderer.bounds.center.x - twoRenderer.bounds.center.x, 2);
        float yDiffSquared = Mathf.Pow(oneRenderer.bounds.center.y - twoRenderer.bounds.center.y, 2);

        // If a^2 + b^2 < c, there's a collision
        return xDiffSquared + yDiffSquared < radiiSquared;
    }
}
