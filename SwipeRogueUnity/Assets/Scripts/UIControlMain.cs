using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class UIControlMain : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void LoadLevel(string lvl)
	{
		if (Time.timeScale == 0) {
			Time.timeScale = 1;
		}
		SceneManager.LoadSceneAsync(lvl);
	}
}
