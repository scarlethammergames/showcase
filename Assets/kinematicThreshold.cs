using UnityEngine;
using System.Collections;

public class kinematicThreshold : MonoBehaviour {
    public float contactThreshold;

    private Rigidbody rb;


	// Use this for initialization
	void Start () {
        rb = this.GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {

//	    if(){
//            rb.isKinematic = false;
//        }
	}

    void OnCollisionEnter(Collision col)
    {
        if( col.rigidbody ){
            float velocity = col.rigidbody.velocity.sqrMagnitude;
        }
//        float velocity = col.rigidbody.velocity.sqrMagnitude;
//        if(  ){
//        
//        }
    }
}
