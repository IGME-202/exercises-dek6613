using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// RandomWalk class
// Placed on a walker prefab
// Moves a random walker using randomized values

public class RandomWalk : MonoBehaviour 
{

	// Class fields
	// Will any class fields help the walker start and stop moving?
	private bool walking;

	void Start () 
	{
		walking = false;
	}
	

	void Update () 
	{
		// If the user presses the "m" key, toggle walking state.
		if (Input.GetKeyDown("m"))
        {
			walking = !walking;
        }

		// Only call Walk() when a user presses the 'M' key
		if (walking)
        {
			Walk();
        }
	}


	/// <summary>
	/// Walk()
	/// Purpose: Calculates the position of a walker after he has taken a randomized step
	/// Params:  none
	/// Returns: none
	/// </summary>
	void Walk()
	{
		// Generate a random number
		float randX = Random.Range(-1f, 1f);

		// Based on the number generated, the walker will move positively, negatively, or remain in place on the X axis
		transform.Translate(randX, 0, 0);

		// Generate another random number
		float randZ = Random.Range(-1f, 1f);

		// Based on the number generated, the walker will move positively, negatively, or remain in place on the Z axis
		transform.Translate(0, 0, randZ);
	}


	/// <summary>
	/// OnGUI()
	/// Displays instructions to the user
	/// </summary>
	void OnGUI()
	{
		// Display instructions to the user:
		//   "Press 'm' key to start and stop the walker"

		GUI.Box(new Rect(10, 10, 120, 50), "Press 'm' key to start and stop the walker");
		GUI.skin.box.wordWrap = true;
	}
}
