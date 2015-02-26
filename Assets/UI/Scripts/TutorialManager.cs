using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class TutorialManager : MonoBehaviour {

	public GameObject eventSystemObject;
	public GameObject tutorialStartButton;
	public List<GameObject> syphenTutorials;
	public List<GameObject> blitzTutorials;
	private EventSystem eventSystem;
	private List<GameObject> tutorials;

	// Use this for initialization
	void Start () {
		foreach (GameObject tut in syphenTutorials) {
			tut.SetActive(false);
		}
		foreach (GameObject tut in blitzTutorials) {
			tut.SetActive(false);
		}
		eventSystem = eventSystemObject.GetComponent<EventSystem> ();
		GameObject syphen = GameObject.Find ("Syphen");
		GameObject blitz = GameObject.Find ("Blitz");
		if (syphen != null) {
			tutorials = syphenTutorials;
		} else {
			tutorials = blitzTutorials;
		}
		//Start first tutorial panel
		tutorials[0].SetActive (true);
		eventSystem.SetSelectedGameObject(tutorialStartButton);
		GameObject.FindGameObjectWithTag("Player").GetComponent<DeftPlayerController>().enabled = false;
	}
	public void NextTutorial() {
		//Disable current menu and remove it from list
		tutorials [0].SetActive (false);
		tutorials.RemoveAt (0);
		//Check if there exists next tutorial
		if (tutorials.Count>0) {
			tutorials [0].SetActive (true);
			//Activate button on that panel so that the xbox controller can access it
			try {
				GameObject button = (tutorials[0].transform.FindChild("Panel")).FindChild ("YesButton").gameObject;
				eventSystem.SetSelectedGameObject(button);
			}
			catch(System.NullReferenceException e) {

			}
		}
	}
	public void ExitTutorial() {
		tutorials[0].SetActive (false);
		GameObject.FindGameObjectWithTag("Player").GetComponent<DeftPlayerController>().enabled = true;
	}
}
