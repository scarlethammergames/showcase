using UnityEngine;
using System.Collections;

public class ForceConditions : MonoBehaviour {
	public bool pushable = true;
	public bool pullable = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public bool canPush(){
		return pushable;
	}

	public bool canPull(){
		return pullable;
	}

	public void setPushable( bool canPush ){
		pushable = canPush;
	}
	
	public void setPullable( bool canPull ){
		pullable = canPull;
	}
}
