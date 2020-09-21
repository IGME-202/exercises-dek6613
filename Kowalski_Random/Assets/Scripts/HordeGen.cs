using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HordeGen : MonoBehaviour
{
    public GameObject hordePrefab;

    [SerializeField]
    private float standardDeviationZ = 15f;

    private GameObject[] horde;

    // Start is called before the first frame update
    void Start()
    {
        horde = new GameObject[75];

        for (int i = 0; i < horde.Length; i++)
        {
            // Pseudo-randomizes the Z position of each horde member, favoring those in the center
            float gaussZ = Gaussian(0f, standardDeviationZ);

            // Flips all horde members with a positive Z coord to become negative (meaning they will all be on one side of the center
            gaussZ = 0f - Mathf.Abs(gaussZ);

            // Moves the horde to its actual Z position
            gaussZ += 95f;
            
            // Normal random X coord, since they can be scattered horizontally without need for clumping
            float randX = Random.Range(85f, 115f);

            // Combines random X and Z values
            Vector3 pos = new Vector3(randX, 0, gaussZ);

            // Moves the horde up/down to match with the terrain height
            pos.y = Terrain.activeTerrain.SampleHeight(pos) + Terrain.activeTerrain.transform.position.y;

            horde[i] = Object.Instantiate(hordePrefab, pos, Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Uses Gaussian randomization for pseudo-random effects
    /// </summary>
    /// <param name="mean">Average</param>
    /// <param name="stdDev">Standard Deviation</param>
    /// <returns>A pseudo-random number using the Gaussian formula</returns>
    float Gaussian(float mean, float stdDev)
    {
        float val1 = Random.Range(0f, 1f);
        float val2 = Random.Range(0f, 1f);
        float gaussValue =
                 Mathf.Sqrt(-2.0f * Mathf.Log(val1)) *
                 Mathf.Sin(2.0f * Mathf.PI * val2);
        return mean + stdDev * gaussValue;
    }
}
