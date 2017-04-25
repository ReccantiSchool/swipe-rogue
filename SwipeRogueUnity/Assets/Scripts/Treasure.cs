using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Treasure : MonoBehaviour {

    GameObject ScoreCount;
    ScoreControl score;
    

	// Use this for initialization
	void Start () {
        ScoreCount = GameObject.Find("ScoreCount");
        score = ScoreCount.GetComponent<ScoreControl>();
	}

    private void OnMouseDown()
    {
        score.score += 100;
        Debug.Log("Found a Treasure Chest!");
        this.gameObject.SetActive(false);
    }

}
