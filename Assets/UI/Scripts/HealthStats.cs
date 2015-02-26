using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HealthStats : MonoBehaviour {

	public GameObject player1;
	public GameObject player2;
	private PlayerHealth player1Health;
	private PlayerHealth player2Health;
	private Text stats;

	// Use this for initialization
	void Start () {
		stats = this.GetComponent<Text> ();
		player1Health = player1.GetComponent<PlayerHealth> ();
		if (player2) {
			player2Health = player2.GetComponent<PlayerHealth> ();
		}
	}
	
	// Update is called once per frame
	void Update () {
		stats.text = player1Health.getHealth().ToString() + "/" + player1Health.getMaxHealth().ToString();
	}
}
