using UnityEngine;
using System.Collections;

public class DepotBarLocation : MonoBehaviour {

	public int depotNum = 1;
	private RectTransform depot;

	// Use this for initialization
	void Start () {
		depot = gameObject.GetComponent<RectTransform> ();
		depot.sizeDelta = new Vector2 (Screen.width*0.2f, Screen.width*0.03f);
		if (depotNum==1) {
			depot.anchoredPosition = new Vector2 (-Screen.width * 0.01f, -Screen.height * 0.05f);
		} else {
			depot.anchoredPosition = new Vector2 (-Screen.width * 0.01f, -1.0f*(depot.sizeDelta.y + Screen.height*0.07f));
		}
	}
}
