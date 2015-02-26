using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {

	public GUISkin gSkin;
	public string sceneOnPlay;

	void Start() {
		float fontSize = Screen.height * 0.04f;
		gSkin.button.fontSize = (int) fontSize;
	}
	void OnGUI() {
		GUI.skin = gSkin;

		if (GUI.Button(new Rect(Screen.width*0.5f - (Screen.width*0.2f/2), Screen.height*0.4f,Screen.width*0.2f,Screen.height*0.1f), "Play")) {
			Application.LoadLevel(sceneOnPlay);
		}

		if (GUI.Button(new Rect(Screen.width*0.5f - (Screen.width*0.2f/2), Screen.height*0.6f,Screen.width*0.2f,Screen.height*0.1f), "Quit")) {
			Application.Quit();
		}
	}
}
