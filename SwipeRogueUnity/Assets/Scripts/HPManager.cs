using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class HPManager : MonoBehaviour {

    Image image;
    float hpPercent;
    float maxHP;

	// Use this for initialization
	void Start () {

        image = GetComponent<Image>();
    }
	
	// Update is called once per frame
	void Update () {
        maxHP = StatManager.instance.maxHP;

        hpPercent = StatManager.instance.hp / maxHP;

        image.fillAmount = hpPercent;

	}
}
