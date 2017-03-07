using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public static GameManager instance = null;
	[HideInInspector]
	private FloorManager floorScript;

	/**
	 * This function is called before the Start() method.
	 * It creates a GameManager singleton and initializes
	 * the floor
	 */
	void Awake () {
		if (instance == null)
			instance = this;
		else if (instance != this)
			Destroy (gameObject);

		DontDestroyOnLoad (gameObject);
		floorScript = GetComponent<FloorManager> ();
		floorScript.SetupFloor ();
	}
	
	// Update is called once per frame
	void Update () {

	}
}
