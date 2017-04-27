using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loader : MonoBehaviour {

	public GameObject statManager;
	// Use this for initialization

	/**
	 * Handle loading all of the necessary GameObjects and managers
	 * whenever the level loads
	 */
	void Start () {
		// instantiate the StatManager if it doesn't already exist
		if (StatManager.instance == null) {
			Instantiate(statManager);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
