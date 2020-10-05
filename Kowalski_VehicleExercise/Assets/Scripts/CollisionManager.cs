using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionManager : MonoBehaviour
{
    private enum CollisionType { AABB, Circle };
    private CollisionType collisionType = CollisionType.AABB;

    [SerializeField]
    private GameObject player;

    [SerializeField]
    private GameObject[] obsticles;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Switches collision type on input
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            collisionType = CollisionType.AABB;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            collisionType = CollisionType.Circle;
        }

        // Keeps track of all objects that are colliding this frame
        List<GameObject> collidingObjects = new List<GameObject>();

        switch (collisionType)
        {
            case (CollisionType.AABB):
                for (int i = 0; i < obsticles.Length; i++)
                {
                    // If there's a collision, add the colliding objects to the list
                    if (CollisionDetection.AABBCollision(player, obsticles[i]))
                    {
                        // Makes sure the player object doesn't get added multiple times
                        if (!collidingObjects.Contains(player)) { collidingObjects.Add(player); }

                        collidingObjects.Add(obsticles[i]);
                    }
                }
                break;
            case (CollisionType.Circle):
                for (int i = 0; i < obsticles.Length; i++)
                {
                    // If there's a collision, add the colliding objects to the list
                    if (CollisionDetection.CircleCollision(player, obsticles[i]))
                    {
                        // Makes sure the player object doesn't get added multiple times
                        if (!collidingObjects.Contains(player)) { collidingObjects.Add(player); }

                        collidingObjects.Add(obsticles[i]);
                    }
                }
                break;
        }

        if (collidingObjects.Contains(player))
        {
            player.GetComponent<SpriteRenderer>().color = Color.red;
        }
        else
        {
            player.GetComponent<SpriteRenderer>().color = Color.white;
        }

        for (int i = 0; i < obsticles.Length; i++)
        {
            if (collidingObjects.Contains(obsticles[i]))
            {
                obsticles[i].GetComponent<SpriteRenderer>().color = Color.red;
            }
            else
            {
                obsticles[i].GetComponent<SpriteRenderer>().color = Color.white;
            }
        }
    }
}
