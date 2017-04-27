using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Enemy : MonoBehaviour {

	// the hit points of the enemy
	public int baseHP = 10;

	// the base strength of the enemy
	public int baseStrength = 0;

	// the computed HP. The takes the base hp and scales it according to the current floor level
	public int computedHP;

	// the initial hitpoints for the enemy, it will be set to the hp property on Start
	private int initHealth = 10;

	// the computed strength, scaled according to the floor level
	public int computedStrength;

	// a reference to the enemy's animator
	private Animator animator;

	// a reference to the health bar GUI image
	private Image healthBar;

	// attack intervals
	public bool shouldAttack = false;
	// public float restAttackInterval = 2.0f;
	// public float beginAttackInterval = 0.5f;
	// public float attackInterval = 1.0f;
	// public float endAttackInterval = 1.5f;

	public float restAttackInterval = 0.5f;
	public float beginAttackInterval = 1.0f;
	public float attackInterval = 1.5f;
	public float endAttackInterval = 2.0f;

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
	private AttackState currentAttackState = AttackState.Rest;
	
	// do initialization here
	void Start () {
		
		// calculate the HP
		computedHP = baseHP + Fibonacci(StatManager.instance.currentFloor);
		initHealth = computedHP;

		// calculate the strength
		computedStrength = baseStrength + StatManager.instance.currentFloor;

		// get access to the components that need to be animated
		animator = GetComponent<Animator>();
		healthBar = transform.Find("EnemyGUI/HealthFront").GetComponent<Image>();

		// set the attack positions and interval
		currentAttackInterval = 0.0f;
		beginAttackPosition = transform.position + new Vector3(0, 1, 0);
		attackPosition = transform.position + new Vector3(0, -1, 0);
		endAttackPosition = transform.position;
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
			// rest between attacks
			else if (currentAttackState == AttackState.Rest) {
				RestAttack();
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
		computedHP -= StatManager.instance.strength;
		float healthPercent = (float)computedHP / initHealth;
		healthBar.fillAmount = healthPercent;
		if (computedHP <= 0) {
			StatManager.instance.GiveExperience(1);
			Destroy(gameObject);
		}
	}

	/**
	 * control all the events that need to happen before the
	 * attack begins
	 */
	private void BeginAttack() {
		if (currentAttackInterval > restAttackInterval && currentAttackInterval <= beginAttackInterval) {
			float normalInterval = 1 - (beginAttackInterval - currentAttackInterval) / (beginAttackInterval - restAttackInterval);
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
			StatManager.instance.hp -= computedStrength;
			Debug.Log(StatManager.instance.hp);
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
			currentAttackInterval = 0.0f;
			currentAttackState = AttackState.Rest;
		}
	}

	/**
	 * Control the interval between attacks
	 */
	private void RestAttack() {
		if (currentAttackInterval <= restAttackInterval) {
			// 
		} else {
			currentAttackState = AttackState.BeforeAttack;
		}
	}


	/**
	 * an iterative fibonacci number generator
	 */
	private int Fibonacci(int startingNumber) {
		int x = 0;
		int y = 1;
		for (int i = 0; i < startingNumber; i++) {
			int temp = x;
			x = y;
			y = temp + y;
		}
		return y;
	}
}
