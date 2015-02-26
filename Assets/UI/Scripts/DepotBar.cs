using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DepotBar : MonoBehaviour {

	public GameObject depotObject;
	private depotAbsorb depotBehaviour;
	private Image bar;

	void Start() {
		//Set up depot bar
		bar = GetComponent<Image> ();
		RectTransform barRect = GetComponent<RectTransform> ();

		
		//Grab depot
		depotBehaviour = depotObject.GetComponent<depotAbsorb> ();
	}
	// Update is called once per frame
	void Update () {
		float currentSize = (float)(depotBehaviour.getCurrentSize ()) / depotBehaviour.getSize ();
		if (currentSize!=bar.fillAmount) {
			bar.fillAmount = Mathf.MoveTowards (bar.fillAmount, currentSize, Time.deltaTime * 0.8f);
		}
	}
}
