using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HealthBar : MonoBehaviour {

	private GameObject player;
	private PlayerHealth playerHealth;
	private Image healthbar;
	private bool firstFill;
	private bool playerSelected;
	void Start() {
		playerSelected = false;
		firstFill = true;
		//Set up health bar
		healthbar = GetComponent<Image> ();
		RectTransform healthRect = GetComponent<RectTransform> ();
		healthRect.sizeDelta = new Vector2 (Screen.width*0.3f, Screen.height*0.04f);
		healthRect.anchoredPosition = new Vector2 (Screen.width*0.08f, -Screen.height*0.05f);
	}
	// Update is called once per frame
	void Update () {
		if (!playerSelected) {
			player = GameObject.FindGameObjectWithTag("Player");
			if (player!=null) {
				//Grab player
				playerHealth = player.GetComponent<PlayerHealth> ();
				playerSelected = true;
			}
		}
		if (firstFill) {
			healthbar.fillAmount = Mathf.MoveTowards (healthbar.fillAmount, 1.0f, Time.deltaTime * 0.8f);
			if (healthbar.fillAmount == 1f) firstFill=false;
		}
		if (playerSelected) {
			float currentHealth = (float)(playerHealth.getHealth ()) / playerHealth.getMaxHealth ();
			if (currentHealth != healthbar.fillAmount) {
				healthbar.fillAmount = Mathf.MoveTowards (healthbar.fillAmount, currentHealth, Time.deltaTime * 0.5f);
			}
		}
	}
}
