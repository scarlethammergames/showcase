using UnityEngine;
using System.Collections;

public class TimedLifespan : MonoBehaviour {
	public float lifespan = 1000.0f;
	public float decayRate = 1.0f;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		lifespan -= decayRate * Time.deltaTime;
		if(lifespan < 0){
			Destroy(this.gameObject);
		}
	}
}
