using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class InGameStats : MonoBehaviour {

	public GameObject eventSystemObject;
	public GameObject gameOverFirstSelected;
	public GameObject gameOverWindow;
	public GameObject winText;
	public GameObject loseText;
	public GameObject depotB;
	private int depotsFull;
	
	private EventSystem eventSystem;

	void Start() {
		gameOverWindow.SetActive (false);
		winText.SetActive (false);
		loseText.SetActive (false);
		eventSystem = eventSystemObject.GetComponent<EventSystem> ();
		depotsFull = 0;
		depotB.SetActive(false);
	}
	public void depotFull() {
		depotsFull++;
		if (depotsFull == 2) {
			depotBFull ();
		} else {
			//Activate second depot
			Debug.Log ("Depot A FULL");
			depotB.SetActive(true);
		}
	}
	public void depotBFull() {
		Debug.Log ("YOU WIN.");
		gameOverWindow.SetActive (true);
		winText.SetActive (true);
		eventSystem.SetSelectedGameObject(gameOverFirstSelected);
	}
	public void playerDied() {
		Debug.Log ("YOU DIED.");
		gameOverWindow.SetActive (true);
		loseText.SetActive (true);
		eventSystem.SetSelectedGameObject(gameOverFirstSelected);
	}


}
