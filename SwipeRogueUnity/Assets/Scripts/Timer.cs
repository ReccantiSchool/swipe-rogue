using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Timer : MonoBehaviour {

    public float currentTime;
    public float startTime = 10f;
    public float timePercent;
    private Image image;
    GameObject sample;
    UIControl uiControl;

    //private GameState gameState;

	// Use this for initialization
	void Start () {
        currentTime = startTime;
        image = GetComponent<Image>();

        sample = GameObject.Find("timerFill");
        uiControl = sample.GetComponent<UIControl>();
        //gameState = GameObject.Find("GameState").GetComponent<GameState>();
	}
	
	// Update is called once per frame
	void Update () {
        currentTime -= Time.deltaTime;
        timePercent = currentTime / startTime;

        image.fillAmount = timePercent;

        if (image.fillAmount == 0)
        {
            uiControl.EndShow();
        }

	}
}
