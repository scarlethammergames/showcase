using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HealthStats : MonoBehaviour {

	private GameObject player;
	private PlayerHealth playerHealth;
	private Text stats;
	private bool characterSelected;

	// Use this for initialization
	void Start () {
		characterSelected = false;
		stats = this.GetComponent<Text> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (!characterSelected) {
			player = GameObject.FindGameObjectWithTag("Player");
			if (player!=null) {
				playerHealth = player.GetComponent<PlayerHealth> ();
				characterSelected = true;
			}
		}
		if (characterSelected) {
			stats.text = playerHealth.getHealth ().ToString () + "/" + playerHealth.getMaxHealth ().ToString ();
		}
	}
}
