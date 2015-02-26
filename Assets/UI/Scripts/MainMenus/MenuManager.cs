using UnityEngine;
using System.Collections;

public class MenuManager : MonoBehaviour {

	public string onPlayGameScene;
	public MainMenu2 CurrentMenu;

	public void Start() {
		Time.timeScale = 1;
		ShowMenu (CurrentMenu);
	}

	public void ShowMenu(MainMenu2 menu) {
		if (CurrentMenu != null) {
			CurrentMenu.IsOpen = false;
		}
		CurrentMenu = menu;
		CurrentMenu.IsOpen = true;
	}
	public void PlayGame() {
		Application.LoadLevel (onPlayGameScene);
	}
	public void QuitGame() {
		Application.Quit();
	}
}
