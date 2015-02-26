using UnityEngine;
using System.Collections;

/**
* Attach this to the projectile to be fired.
*/
public class projectile : MonoBehaviour {
	public float lifespan = 100.0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		lifespan -= 1;
		if(lifespan < 0){
			Destroy(this);
		}
	}
}
