using UnityEngine;
using System.Collections;

public class MainMenu3Title : MonoBehaviour {

	// Use this for initialization
	void Start () {
		RectTransform rt = gameObject.GetComponent<RectTransform> ();
		rt.sizeDelta = new Vector2 (Screen.height*0.8f, Screen.height*0.8f);
		Debug.Log (Screen.height);
	}

}
