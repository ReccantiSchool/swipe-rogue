using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatManager : MonoBehaviour {

	// a reference to this singleton
	public static StatManager instance = null;

	// the current level of the player
	public int playerLevel = 1;

	// the current floor that the player is on
	public int currentFloor = 1;

	// Use this for initialization
	void Awake () {
		// initialize singleton instance
		if (instance == null) {
			instance = this;
		} else if (instance != this) {
			Destroy(gameObject);
		}
		DontDestroyOnLoad(gameObject);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	/**
	 * an iterative fibonacci number generator
	 */
	private int fibonacci(int startingNumber) {
		int x = 0;
		int y = 1;
		for (int i = 0; i < startingNumber; i++) {
			int temp = x;
			x = y;
			y = temp + y;
		}
		return y;
	}
}
