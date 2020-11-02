using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : Vehicle
{
    public GameObject targetHuman;

    protected override void CalcSteeringForces()
    {
        // Always seeks the human
        ApplyForce(Seek(targetHuman));
    }
}
