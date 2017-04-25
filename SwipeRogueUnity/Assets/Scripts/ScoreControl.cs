using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ScoreControl : MonoBehaviour {

    public int score;
    Text ScoreCount;

    // Use this for initialization
    void Start () {
        ScoreCount = GetComponent<Text>();
        score = 0;
    }
	
	// Update is called once per frame
	void Update () {
        ScoreCount.text = "Score: " + score;
	}
}
