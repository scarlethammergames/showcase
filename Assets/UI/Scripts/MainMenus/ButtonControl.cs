using UnityEngine;
using System.Collections;

public class ButtonControl : MonoBehaviour {

	protected Animator animator;

	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (animator) {
			Debug.Log("animator active");

		}
	}
}
