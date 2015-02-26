using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour {
	
	public int maxHealth=100;
	private int currentHealth;
	public GameObject gameManager;
	private InGameStats gameStats;
	private bool dead;
	
	void Start() {
		currentHealth = maxHealth;
		gameStats = gameManager.GetComponent<InGameStats> ();
		dead = false;
	}

	public void decreaseHealth(int amount) {
		currentHealth -= amount;
		//If player runs out of health, tell game manager player died
		if (!dead && currentHealth <= 0) {
			gameStats.playerDied();
			dead = true;
		}
	}

	public void increaseHealth(int amount) {
		currentHealth += amount;
	}

	public int getHealth() {
		return currentHealth;
	}

	public int getMaxHealth() {
		return maxHealth;
	}
}
