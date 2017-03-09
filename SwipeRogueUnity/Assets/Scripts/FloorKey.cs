using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorKey : MonoBehaviour {

	void OnMouseDown() {
		GameManager.instance.hasKey = true;
		Debug.Log("Got the floor key!");
		Destroy(gameObject);
	}
}
