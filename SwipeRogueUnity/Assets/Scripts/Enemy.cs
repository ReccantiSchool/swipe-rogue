using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Enemy : MonoBehaviour {

	private Vector3 inputDownPosition;
	private Vector3 inputDownPositionWorld;

	private bool isDragging = false;

	private bool isDown = false;
	private bool shouldBeListening = true;

	private Renderer render;
	private Animator animator;

	private Image healthBar;

	public int hp = 10;
	private int initHealth = 10;
	
	void Start () {
		initHealth = hp;
		render = GetComponent<SpriteRenderer>();
		animator = GetComponent<Animator>();
		// GameObject EnemyGUI = GameObject.Find("healthFront");
		healthBar = GameObject.Find("HealthFront").GetComponent<Image>();
	}

	void OnMouseEnter() {
		handleHit();
	}

	void OnMouseExit() {
	}

	/**
	 * Handles what happens when the enemy is hit
	 */
	private void handleHit() {
		// display the hit animation
		animator.SetTrigger("enemyHit");

		// decrease HP and destroy enemy if
		// it reaches zero
		hp--;
		float healthPercent = (float)hp / initHealth;
		healthBar.fillAmount = healthPercent;
		Debug.Log(healthBar.fillAmount);
		if (hp == 0) {
			Destroy(gameObject);
		}
	}
}
