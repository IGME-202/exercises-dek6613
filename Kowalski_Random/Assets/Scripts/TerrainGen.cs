using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TerrainGeneration class
// Placed on a terrain game object
// Generates a Perlin noise-based heightmap

public class TerrainGen : MonoBehaviour
{
	public GameObject gaussCloseupCamera;

	private TerrainData myTerrainData;
	public Vector3 worldSize;
	public int resolution = 129;
	float[,] heightArray;

	[SerializeField]
	[Range(0f, 1f)]
	private float raiseFactor = 0.05f;


	void Start()
	{
		myTerrainData = gameObject.GetComponent<TerrainCollider>().terrainData;
		worldSize = new Vector3(200, 50, 200);
		myTerrainData.size = worldSize;
		myTerrainData.heightmapResolution = resolution;
		heightArray = new float[resolution, resolution];

		Perlin();

		// Assign values from heightArray into the terrain object's heightmap
		myTerrainData.SetHeights(0, 0, heightArray);

		AdjustCameras();
	}


	void Update()
	{

	}

	/// <summary>
	/// Perlin()
	/// Assigns heightsArray values using Perlin noise
	/// </summary>
	void Perlin()
	{
		// Randomized "origin" values on the perlin array
		float xOrg = Random.Range(0f, 100f);
		float yOrg = Random.Range(0f, 100f);

		// Loop through terrain coords
		for (int i = 0; i < resolution; i++)
		{
			for (int j = 0; j < resolution; j++)
			{
				// For each iteration, get the x and y coords on the perlin plane based on the current terrain coords and the origin values
				float xCoord = i * raiseFactor + xOrg;
				float yCoord = j * raiseFactor + yOrg;

				// Assign the terrain height based on the perlin coords
				heightArray[i, j] = Mathf.PerlinNoise(xCoord, yCoord);
			}
		}
	}

	void AdjustCameras()
    {
		Vector3 pos = gaussCloseupCamera.transform.position;

		// Adjust camera's Y pos based on the terrain's
		pos.y = Terrain.activeTerrain.SampleHeight(pos) + Terrain.activeTerrain.transform.position.y + 3;
		gaussCloseupCamera.transform.position = pos;
	}
}
