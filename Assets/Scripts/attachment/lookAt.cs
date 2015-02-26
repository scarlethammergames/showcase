using UnityEngine;
using System.Collections;

/**
 * The object that has this script will be oriented towards the target.
 * Ideal for a camera to aim at the player.
 */
public class lookAt : MonoBehaviour {
	public Transform target;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	   transform.LookAt(target);
	}
}
