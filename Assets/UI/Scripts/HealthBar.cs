using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HealthBar : MonoBehaviour {
	public GameObject gameManager;
	private InGameStats gameManagerStats;
	private int playerID;
	private Image healthbar;
	private bool playerSelected;
	private bool firstFill;

	void Start() {
		firstFill = true;
		playerSelected = false;
		gameManagerStats = gameManager.GetComponent<InGameStats> ();
		//Set up health bar
		healthbar = GetComponent<Image> ();
		RectTransform healthRect = GetComponent<RectTransform> ();
		healthRect.sizeDelta = new Vector2 (Screen.width*0.3f, Screen.height*0.04f);
		healthRect.anchoredPosition = new Vector2 (Screen.width*0.08f, -Screen.height*0.05f);
	}
	public void StartHealthBar(int playerNumber) {
		this.playerID = playerNumber;
		playerSelected = true;
	}
	// Update is called once per frame
	void Update () {
		if (playerSelected && firstFill) {
			healthbar.fillAmount = Mathf.MoveTowards (healthbar.fillAmount, 1.0f, Time.deltaTime * 0.8f);
			if (healthbar.fillAmount == 1f) firstFill=false;
		}
		if (playerSelected) {
			float currentHealth = (float)gameManagerStats.playerCurrentHealth[playerID]/gameManagerStats.playerTotalHealth[playerID];
			if (currentHealth != healthbar.fillAmount) {
				healthbar.fillAmount = Mathf.MoveTowards (healthbar.fillAmount, currentHealth, Time.deltaTime * 0.5f);
			}
		}
	}
}
