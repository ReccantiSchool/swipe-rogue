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

	// attack intervals
	public bool shouldAttack = false;
	public float beginAttackInterval = 0.5f;
	public float attackInterval = 1.0f;
	public float endAttackInterval = 1.5f;

	// attack positions
	private Vector3 beginAttackPosition;
	private Vector3 attackPosition;
	private Vector3 endAttackPosition;

	private float currentAttackInterval;

	// an enum to manage the different states
	private enum AttackState {
		BeforeAttack, 
		Attack, 
		EndAttack, 
		Rest 
	}

	// the current attack state
	private AttackState currentAttackState = AttackState.BeforeAttack;
	
	// do initialization here
	void Start () {
		initHealth = hp;
		// render = GetComponent<SpriteRenderer>();
		animator = GetComponent<Animator>();
		// GameObject EnemyGUI = GameObject.Find("healthFront");
		healthBar = transform.Find("EnemyGUI/HealthFront").GetComponent<Image>();
		currentAttackInterval = 0.0f;

		// set the attack positions
		beginAttackPosition = transform.position + new Vector3(0, 1, 0);
		attackPosition = transform.position + new Vector3(0, -1, 0);
		endAttackPosition = transform.position;

		Debug.Log(Vector3.Distance(beginAttackPosition, attackPosition));
	}

	void Update () {
		// handle attacking
		if (shouldAttack) {
			// begin attack if interval is less than beginAttackInterval
			if (currentAttackState == AttackState.BeforeAttack) {
				BeginAttack();
			}
			// attack if interval is less than attack interval
			else if (currentAttackState == AttackState.Attack) {
				Attack();
			}
			// end attack if interval is less than endAttackInterval
			else if (currentAttackState == AttackState.EndAttack) {
				EndAttack();
			}
			currentAttackInterval += Time.deltaTime;
		}
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

	/**
	 * control all the events that need to happen before the
	 * attack begins
	 */
	private void BeginAttack() {
		if (currentAttackInterval <= beginAttackInterval) {
			float normalInterval = 1 - (beginAttackInterval - currentAttackInterval) / beginAttackInterval;
			transform.position = Vector3.Lerp(endAttackPosition, beginAttackPosition, normalInterval);
		} else {
			currentAttackState = AttackState.Attack;
		}
	}

	/**
	 * control all the events that need to happen
	 * during the attack
	 */
	private void Attack() {
		if (currentAttackInterval > beginAttackInterval && currentAttackInterval <= attackInterval) {
			float normalInterval = 1 - (attackInterval - currentAttackInterval) / (attackInterval - beginAttackInterval);
			transform.position = Vector3.Lerp(beginAttackPosition, attackPosition, normalInterval);
		} else {
			currentAttackState = AttackState.EndAttack;
		}
	}

	/**
	 * control all the events that need to happen after
	 * the attack happens
	 */
	private void EndAttack() {
		if (currentAttackInterval > attackInterval && currentAttackInterval <= endAttackInterval) {
			float normalInterval = 1 - (endAttackInterval - currentAttackInterval) / (endAttackInterval - attackInterval);
			transform.position = Vector3.Lerp(attackPosition, endAttackPosition, normalInterval);
		} else {
			currentAttackState = AttackState.BeforeAttack;
			currentAttackInterval = 0.0f;
		}
	}
}
