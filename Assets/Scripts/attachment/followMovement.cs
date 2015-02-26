using UnityEngine;
using System.Collections;

/**
 * The object that has this script will follow the target.
 * Ideal for a camera to follow the player.
 */
public class followMovement : MonoBehaviour {
    public GameObject target;
	Vector3 offset = new Vector3();
	
	// Use this for initialization
	void Start () {
		offset = transform.position - target.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
	   transform.position = target.transform.position + offset;
	}
}
