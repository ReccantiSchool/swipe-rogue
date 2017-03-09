using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FloorExit : MonoBehaviour {

	void OnMouseDown() {
		if (!GameManager.instance.hasKey) {
			Debug.Log("You need to find the key first!");
		} else {
			SceneManager.LoadScene("Main");
			Debug.Log("Got the floor key!");
		}
	}
}
