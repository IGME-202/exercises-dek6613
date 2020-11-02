using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Human : Vehicle
{
    public GameObject targetZombie;
    public float fleeRange = 5f;

    public Treasure targetTreasure;

    protected override void CalcSteeringForces()
    {
        // Ultimate force
        Vector3 uForce = Vector3.zero;

        // Only flee from the zombie if close to it
        if (Vector3.Distance(position, targetZombie.transform.position) < fleeRange)
        {
            uForce += Flee(targetZombie);
        }

        // Always seek the treasure
        uForce += Seek(targetTreasure.gameObject);

        // Clamps the ultimate force to the human's max force such that multiple steering forces don't cause its acceleration to go higher than intended
        uForce = Vector3.ClampMagnitude(uForce, maxForce);

        ApplyForce(uForce);
    }

    protected override void Update()
    {
        base.Update();

        // If colliding with a treasure, activate the treasure's "on grab" method
        if(Vector3.Distance(position, targetTreasure.transform.position) < radius)
        {
            targetTreasure.OnGrab();
        }
    }
}
