using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Enemy : MonoBehaviour {

	// the hit points of the enemy
	public int hp = 10;

	// the initial hitpoints for the enemy, it will be set to the hp property on Start
	private int initHealth = 10;

	// private Renderer render;

	// a reference to the enemy's animator
	private Animator animator;

	// a reference to the health bar GUI image
	private Image healthBar;

	
	// do initialization here
	void Start () {
		initHealth = hp;
		// render = GetComponent<SpriteRenderer>();
		animator = GetComponent<Animator>();
		// GameObject EnemyGUI = GameObject.Find("healthFront");
		healthBar = transform.Find("EnemyGUI/HealthFront").GetComponent<Image>();
	}

	/**
	 * Handles all the events that should happen when the user
	 * hovers over the enemy with their mouse or drags over it
	 * with their finger
	 */
	void OnMouseEnter() {
		handleHit();
	}

	/**
	 * Handles what happens when the enemy is hit
	 */
	private void handleHit() {
		// display the hit animation
		animator.SetTrigger("enemyHit");

		// decrease HP and destroy enemy if it reaches zero
		hp--;
		float healthPercent = (float)hp / initHealth;
		healthBar.fillAmount = healthPercent;
		Debug.Log(healthBar.fillAmount);
		if (hp == 0) {
			Destroy(gameObject);
		}
	}
}
