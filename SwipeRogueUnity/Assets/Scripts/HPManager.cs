using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class HPManager : MonoBehaviour {

    StatManager hp;
    GameObject stats;
    Image image;
    float hpPercent;
    float maxHP;

	// Use this for initialization
	void Start () {

        stats = GameObject.Find("Main UI");
        hp = stats.GetComponent<StatManager>();

        image = GetComponent<Image>();
    }
	
	// Update is called once per frame
	void Update () {
        maxHP = hp.maxHP;

        hpPercent = hp.hp / maxHP;

        image.fillAmount = hpPercent;

	}
}
