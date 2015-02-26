using UnityEngine;
using System.Collections;

/**
* Smoothly follow a target object.
*/
public class interpolatedFollow : MonoBehaviour {
	public GameObject target;
	public Vector3 offset;
	public float positionSmooth = 5.0f;
	public float rotationSmoothing;
	public float rotationFactor = 30.0f;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = Vector3.Lerp(transform.position, target.transform.position + offset, positionSmooth * Time.deltaTime);
//		transform.eulerAngles = Vector3.Lerp(transform.up, target.transform.up * rotationFactor, rotationSmoothing * Time.deltaTime);
//		transform.rotation = Vector3.Slerp(transform.rotation, target.transform.rotation, rotationSmoothing * Time.deltaTime);
	}
}
