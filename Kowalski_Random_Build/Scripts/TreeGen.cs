using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeGen : MonoBehaviour
{
    public GameObject treePrefab;

    private GameObject[] trees;

    // Start is called before the first frame update
    void Start()
    {
        trees = new GameObject[40];

        for (int i = 0; i < trees.Length; i++)
        {
            // Randomly generates tree positions within the terrain's size
            Vector3 pos = new Vector3(Random.Range(0f, 200f), 0, Random.Range(0f, 200f));

            // Moves the tree up/down to match with the terrain height
            pos.y = Terrain.activeTerrain.SampleHeight(pos) + Terrain.activeTerrain.transform.position.y;

            Object.Instantiate(treePrefab, pos, Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
