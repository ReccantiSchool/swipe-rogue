using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    GameObject ScoreCount;
    ScoreControl score;

	public static GameManager instance = null;
	[HideInInspector]
	// public FloorManager floorScript;

	public bool hasKey;

    /**
	 * This function is called before the Start() method.
	 * It creates a GameManager singleton and initializes
	 * the floor
	 */
    void Start () {
		instance = this;
		hasKey = false;

        ScoreCount = GameObject.Find("ScoreCount");
        score = ScoreCount.GetComponent<ScoreControl>();

		score.score = StatManager.instance.score;
        // if (instance == null)
        // 	instance = this;
        // else if (instance != this)
        // 	Destroy (gameObject);

        // DontDestroyOnLoad (gameObject);

        // add our OnSceneLoaded to the list of methods
        // that is called by the scenemanager on load
        // SceneManager.sceneLoaded += OnSceneLoaded;
    }
	
	void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
		// initialize the floor
		// floorScript = GetComponent<FloorManager> ();
		// floorScript.SetupFloor ();

		// initialize the key
		hasKey = false;
	}
}
