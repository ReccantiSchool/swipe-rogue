using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatManager : MonoBehaviour {

	// a reference to this singleton
	public static StatManager instance = null;

	// the player's score. This can be increased by collecting gold
	public int score = 0;

	// the current level of the player
	public int playerLevel = 1;

	// the current floor that the player is on
	public int currentFloor = 1;

	// the maximum hp of the player
	public int maxHP = 10;

	// the current hp of the player
	public int hp = 10;

	// the player's strength
	public int strength = 1;

	// the experience needed to get to the next level
	public int experienceToNextLevel = 1;

	// determines whether or not the player is still alive
	public bool isAlive = true;

	// Use this for initialization
	void Awake () {
		// initialize singleton instance
		if (instance == null) {
			instance = this;
		} else if (instance != this) {
			Destroy(gameObject);
		}
		DontDestroyOnLoad(gameObject);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	/**
	 * Destroy the current GameObject
	 */
	public void DestroySelf() {
		if (instance != null) {
			Destroy(gameObject);
		}
	}

	/**
	 * Gives experience to the player. If it's less than
	 * zero, it will level the player up
	 */
	public void GiveExperience(int exp) {
		experienceToNextLevel -= exp;
		if (experienceToNextLevel <= 0) {
			LevelUp();
		}
	}

	/**
	 * Move to an end state. Disables the player's movement
	 * and display the end screen.
	 */
	public void KillPlayer() {
		this.isAlive = false;
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

	/**
	 * Levels up the player stats are based on the Fibonacci sequence
	 */
	private void LevelUp() {
		playerLevel++;
		maxHP = 10 + Fibonacci(playerLevel);
		strength = playerLevel;
		experienceToNextLevel = Fibonacci(playerLevel);
		if (hp < maxHP) {
			hp = maxHP;
		}
	}
}
