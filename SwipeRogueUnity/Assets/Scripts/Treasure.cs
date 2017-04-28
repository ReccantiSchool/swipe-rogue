using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Treasure : MonoBehaviour {

    GameObject ScoreCount;
    ScoreControl score;
    
    public int pointsPerChest = 100;
	// Use this for initialization
	void Start () {
        ScoreCount = GameObject.Find("ScoreCount");
        score = ScoreCount.GetComponent<ScoreControl>();
	}

    private void OnMouseDown()
    {
        StatManager.instance.score += pointsPerChest;
        score.score = StatManager.instance.score;
        Debug.Log("Found a Treasure Chest!");
        Destroy(gameObject);
    }

}
