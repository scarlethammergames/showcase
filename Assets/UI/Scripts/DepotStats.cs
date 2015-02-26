using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DepotStats : MonoBehaviour {

	public GameObject depotA; 
	private depotAbsorb depotABehaviour;
	private Text stats;

	// Use this for initialization
	void Start () {
		depotABehaviour = depotA.GetComponent<depotAbsorb> ();
		stats = this.GetComponent<Text> ();
	}
	
	// Update is called once per frame
	void Update () {
		stats.text = depotABehaviour.getCurrentSize().ToString() + "/" + depotABehaviour.getSize().ToString();
	}
}
