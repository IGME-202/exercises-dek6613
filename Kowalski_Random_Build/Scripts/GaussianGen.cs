using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GaussianGen : MonoBehaviour
{
    public GameObject leaderPrefab;

    [SerializeField]
    private float standardDeviation = 1;

    private GameObject[] leaders;

    // Start is called before the first frame update
    void Start()
    {
        leaders = new GameObject[8];

        for (int i = 0; i < leaders.Length; i++)
        {
            // Instantiates each leader in a line along the Z axis with a slight layer of randomness in their X position.
            leaders[i] = Object.Instantiate(leaderPrefab, new Vector3(100f + Random.Range(-1f, 1f), 0, 100f + i), Quaternion.identity);

            // Pseudo-randomizes their X/Z scale and Y scale
            float gaussXZ = Gaussian(1f, standardDeviation);
            float gaussY = Gaussian(1f, standardDeviation);

            // Applies the scale change
            leaders[i].transform.localScale = new Vector3(gaussXZ, gaussY, gaussXZ);

            // Moves leaders vertically to match with terrain
            Vector3 pos = leaders[i].transform.position;
            pos.y = Terrain.activeTerrain.SampleHeight(pos) + Terrain.activeTerrain.transform.position.y;
            leaders[i].transform.position = pos;
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
