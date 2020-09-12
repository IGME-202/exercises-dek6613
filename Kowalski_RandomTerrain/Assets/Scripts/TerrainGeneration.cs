using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TerrainGeneration class
// Placed on a terrain game object
// Generates a Perlin noise-based heightmap

public class TerrainGeneration : MonoBehaviour 
{

	private TerrainData myTerrainData;
	public Vector3 worldSize;
	public int resolution = 129;			// number of vertices along X and Z axes
	float[,] heightArray;

	[SerializeField]
	[Range(0f,1f)]
	private float raiseFactor = 0.05f;


	void Start () 
	{
		myTerrainData = gameObject.GetComponent<TerrainCollider> ().terrainData;
		worldSize = new Vector3 (200, 50, 200);
		myTerrainData.size = worldSize;
		myTerrainData.heightmapResolution = resolution;
		heightArray = new float[resolution, resolution];

		// Fill the height array with values!
		// Uncomment the Ramp and Perlin methods to test them out!
		Flat(1.0f);

		// Was uncertain of whether you wanted us to have the ramp's bottom/top to be a point or an edge, so I did both
		RampEdge();
		RampPoint();

		Perlin();

		// Assign values from heightArray into the terrain object's heightmap
		myTerrainData.SetHeights (0, 0, heightArray);
	}
	

	void Update () 
	{
		
	}

	/// <summary>
	/// Flat()
	/// Assigns heightArray identical values
	/// </summary>
	void Flat(float value)
	{
		// Fill heightArray with 1's
		for(int i = 0; i < resolution; i++)
		{
			for(int j = 0; j < resolution; j++)
			{
				heightArray [i, j] = value;
			}
		}
	}


	/// <summary>
	/// RampEdge()
	/// Assigns heightsArray values that form a linear ramp where one edge is 0 and the opposite edge is 1
	/// </summary>
	void RampEdge()
	{
		// Finds the mathematcial slope based on the size of the plane
		float increment = 1f / (float)resolution;

		float currentHeight = 0;

		// Loop through terrain coords
		for (int i = 0; i < resolution; i++)
        {
			for (int j = 0; j < resolution; j++)
            {
				// Assign ther terrain's height based on current height
				heightArray[i, j] = currentHeight;
            }

			// Increment current height by the calculated slope (only increases in one dimension)
			currentHeight += increment;
        }
	}

	/// <summary>
	/// RampPoint()
	/// Assigns heightsArray values that form a linear ramp where one point is 0 and the opposite point is 1
	/// </summary>
	void RampPoint()
    {
		// Loop through terrain coords
		for (int i = 0; i < resolution; i++)
        {
			for (int j = 0; j < resolution; j++)
            {
				// Calculates the height of any given coord as the average of the coords divided by the resolution
				// Ex: in a 100x100 array, (50, 50) would be 50% of the way up, so (50 + 50) / 2 / 100 = 0.5. Same goes for (100, 0) or (0, 100), etc.
				heightArray[i, j] = (i + j) / 2 / (float)resolution;
            }
        }
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
			for(int j = 0; j < resolution; j++)
			{
				// For each iteration, get the x and y coords on the perlin plane based on the current terrain coords and the origin values
				float xCoord = i * raiseFactor + xOrg;
				float yCoord = j * raiseFactor + yOrg;

				// Assign the terrain height based on the perlin coords
				heightArray[i, j] = Mathf.PerlinNoise(xCoord, yCoord);
			}
		}

	}
}
