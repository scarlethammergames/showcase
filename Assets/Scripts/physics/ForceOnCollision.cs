﻿using UnityEngine;
using System.Collections;

public class ForceOnCollision: MonoBehaviour {
	public float _magnitudeBirth = 100;
	public float _magnitudeDeath = 0;
	public float _magRatio = 1;
	public float _radiusBirth = 100;
	public float _radiusDeath = 0;
	public float _radiusRatio = 1;
	public int _maxDuration = 100;

	public enum ForceType {Push, Pull, Lift};
	public ForceType _forceType = ForceType.Push;
	
	private int _duration;
	private float _currentRadius;
	private float _currentMagnitude;
	private int _maxMagnitude = 1000000;

	public bool debug = true;


	// Use this for initialization
	void Start () {
		_currentMagnitude = _magnitudeBirth;
		_currentRadius = _radiusBirth;
		_duration = _maxDuration;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if( _duration > 0 ){
			_duration--;
			_currentRadius = Mathf.Lerp( _magnitudeBirth, _magnitudeDeath, _radiusRatio * Time.deltaTime );
			_currentMagnitude = Mathf.Lerp( _radiusBirth, _radiusDeath, _radiusRatio * Time.deltaTime );
		}
	}

	/// <summary>
	/// Raises the draw gizmos selected event.
	/// </summary>
	void OnDrawGizmosSelected() {
		//Debugging
		if(debug){
			Gizmos.color = Color.yellow;
			Gizmos.DrawSphere(transform.position, _currentRadius);
		}
	}

	void OnTriggerStay(Collider other){
		if (other.attachedRigidbody){
			ForceConditions fc = (ForceConditions) other.GetComponent(typeof(ForceConditions));
			if( fc ){
				switch( _forceType ){
				case ForceType.Push:
					if( fc.canPush() ){
						Vector3 direction = Vector3.Normalize( other.transform.position - this.transform.position );
						other.attachedRigidbody.AddForce( direction * Mathf.Clamp(_currentMagnitude/Vector3.SqrMagnitude(  other.transform.position - this.transform.position ), 0, _maxMagnitude) , ForceMode.Impulse);
						other.GetComponent<ForceConditions>().setPullable(true);
					}				
					break;
				case ForceType.Pull:
					if( fc.canPull() ){
						Vector3 direction = Vector3.Normalize( this.transform.position - other.transform.position );
						other.rigidbody.AddForce( direction * Mathf.Clamp(_currentMagnitude/Vector3.Magnitude(this.transform.position - other.transform.position), 0, _maxMagnitude) , ForceMode.Impulse);
					}		
					break;
				case ForceType.Lift:
					if( fc.canPush() ){
						other.rigidbody.AddForce( Vector3.up * Mathf.Clamp(_currentMagnitude/Vector3.Magnitude(this.transform.position - other.transform.position), 0, _maxMagnitude) , ForceMode.Impulse);
					}
					break;
				}
			}
		}
	}
}
