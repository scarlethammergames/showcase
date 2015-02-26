using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HealthBar : MonoBehaviour {

	public GameObject player1;
	private PlayerHealth player1Health;
	private Image healthbar;
	private bool firstFill;
	void Start() {
		firstFill = true;
		//Set up health bar
		healthbar = GetComponent<Image> ();
		RectTransform healthRect = GetComponent<RectTransform> ();
		healthRect.sizeDelta = new Vector2 (Screen.width*0.3f, Screen.height*0.04f);
		healthRect.anchoredPosition = new Vector2 (Screen.width*0.08f, -Screen.height*0.05f);
		//Grab player
		player1Health = player1.GetComponent<PlayerHealth> ();
	}
	// Update is called once per frame
	void Update () {
		if (firstFill) {
			healthbar.fillAmount = Mathf.MoveTowards (healthbar.fillAmount, 1.0f, Time.deltaTime * 0.8f);
			if (healthbar.fillAmount == 1f) firstFill=false;
		}
		float currentHealth = (float)(player1Health.getHealth ()) / player1Health.getMaxHealth ();
		if (currentHealth!=healthbar.fillAmount) {
			healthbar.fillAmount = Mathf.MoveTowards (healthbar.fillAmount, currentHealth, Time.deltaTime * 0.5f);
		}
	}
}
