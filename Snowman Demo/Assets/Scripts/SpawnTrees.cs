using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTrees : MonoBehaviour
{
    public GameObject treePrefab;

    // Start is called before the first frame update
    void Start()
    {
        Instantiate(treePrefab, new Vector3(45, 7, 55), Quaternion.identity);
        Instantiate(treePrefab, new Vector3(49, 7, 30), Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
