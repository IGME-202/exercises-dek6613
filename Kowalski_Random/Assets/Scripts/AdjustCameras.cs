using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdjustCameras : MonoBehaviour
{
	public GameObject gaussCloseupCamera;
	public GameObject hordeCloseupCamera;
	public GameObject hordeMidviewCamera;

	// Start is called before the first frame update
	void Start()
    {
		// Gaussian Close-up
		Vector3 posGauss = gaussCloseupCamera.transform.position;

		// Adjust camera's Y pos based on the terrain's
		posGauss.y = Terrain.activeTerrain.SampleHeight(posGauss) + Terrain.activeTerrain.transform.position.y + 3;
		gaussCloseupCamera.transform.position = posGauss;

		// Horde Close-up
		Vector3 posHordeClose = hordeCloseupCamera.transform.position;

		// Adjust camera's Y pos based on the terrain's
		posHordeClose.y = Terrain.activeTerrain.SampleHeight(posHordeClose) + Terrain.activeTerrain.transform.position.y + 10;
		hordeCloseupCamera.transform.position = posHordeClose;

		// Horde Mid-view
		Vector3 posHordeMid = hordeMidviewCamera.transform.position;

		// Adjust camera's Y pos based on the terrain's
		posHordeMid.y = Terrain.activeTerrain.SampleHeight(posHordeMid) + Terrain.activeTerrain.transform.position.y + 25;
		hordeMidviewCamera.transform.position = posHordeMid;
	}

    // Update is called once per frame
    void Update()
    {
        
    }
}
