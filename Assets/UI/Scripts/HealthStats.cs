using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HealthStats : MonoBehaviour {

	public GameObject gameManager;
	private InGameStats gameManagerStats;
	private int playerID;
	private Text stats;
	private bool characterSelected;

	// Use this for initialization
	void Start () {
		gameManagerStats = gameManager.GetComponent<InGameStats> ();
		characterSelected = false;
		stats = this.GetComponent<Text> ();
	}
	public void StartStats(int playerID) {
		this.playerID = playerID;
		characterSelected = true;
	}
	// Update is called once per frame
	void Update () {
		if (characterSelected) {
			stats.text = gameManagerStats.playerCurrentHealth[playerID].ToString() + "/" + gameManagerStats.playerTotalHealth[playerID].ToString();
		}
	}
}
