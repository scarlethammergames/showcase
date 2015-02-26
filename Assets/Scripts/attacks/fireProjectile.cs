//Reference: http://docs.unity3d.com/ScriptReference/Object.Instantiate.html

using UnityEngine;
using System.Collections;
using GamepadInput;

public enum ProjectileAction { THROW, BEAM, ATTACH_TO_SELF, REMOTE_CTRL }
public enum ProjectileTriggerButton { LEFT, RIGHT }

/**
* Spawn a rigid body GameObject with an initial velocity when triggered. 
* Constraints: The projectile must contain a rigid body.
*/
public class fireProjectile: MonoBehaviour {
	//Launch properties
	public GameObject _projectile;
	public Vector3 _offset;
	public Vector3 _trajectory = Vector3.forward;
	public float _magnitude = 50;
	public float _drag = 5;
	public bool _makeChild = false;
	public ProjectileAction _projectileAction = ProjectileAction.THROW;

	//Rate of fire
	public float _cooldown = 1;
	float _cooldownTimer;

	//Controller properties
	public ProjectileTriggerButton _projectileButton = ProjectileTriggerButton.LEFT;
	GamePad.Index _padIndex = GamePad.Index.One;
	float _triggerThreshold = 0.20f;
	DeftPlayerController _controller;

	//Controllable Projectile
	bool _alreadyFired = false;
	GameObject _controlledTarget;
	GameObject _controlledProjectile;
	

	// Use this for initialization
	void Start () {
		_cooldownTimer = _cooldown;
		_controller = GameObject.FindGameObjectWithTag("Player").GetComponent<DeftPlayerController>();
	}
	
	// Update is called once per frame
	void Update () {
		_cooldownTimer -= Time.deltaTime;

		bool leftTriggerHeld = (GamePad.GetTrigger (GamePad.Trigger.LeftTrigger, _padIndex) > _triggerThreshold);
		bool rightTriggerHeld = (GamePad.GetTrigger (GamePad.Trigger.RightTrigger, _padIndex) > _triggerThreshold);
		if (_cooldownTimer <= 0.0f) 
		{
			//----FIRING----//
			if ( (leftTriggerHeld && _projectileButton == ProjectileTriggerButton.RIGHT) 
			|| (rightTriggerHeld && _projectileButton == ProjectileTriggerButton.LEFT) )
			{
				switch(_projectileAction)
				{
				case ProjectileAction.THROW:
					if(Network.isClient || Network.isServer){
						networkView.RPC("LaunchProjectile", RPCMode.All, _offset, _magnitude, _makeChild);
					}else{
						LaunchProjectile(_offset, _magnitude, _makeChild);
					}
					break;

				case ProjectileAction.BEAM:
					if(Network.isClient || Network.isServer){
						networkView.RPC("BeamAttack", RPCMode.All);
					}else{
						BeamAttack ();
					}
					break;

				case ProjectileAction.REMOTE_CTRL:
					if(!_alreadyFired){
						if(Network.isClient || Network.isServer){
							networkView.RPC("LaunchControllable", RPCMode.All);
						}else{
							LaunchControllable ();
						}
						_alreadyFired = true;
						_controller.enabled = false; // freeze the player
					}else{
						if(_controlledProjectile){
							if(Network.isClient || Network.isServer){
								networkView.RPC("MoveControllable", RPCMode.All);
							}else{
								MoveControllable ();
							}
						}
					}
					break;

				default: break;
				}
			_cooldownTimer = _cooldown;
			}
			//----NOT FIRING----//
			else{
				switch(_projectileAction)
				{
				case ProjectileAction.THROW:

					break;
					
				case ProjectileAction.BEAM:

					break;
					
				case ProjectileAction.REMOTE_CTRL:
					if ( _alreadyFired ) {
						_alreadyFired = false;
						if(_controlledProjectile){ Destroy(_controlledProjectile); }
						if(_controlledTarget){ Destroy(_controlledTarget); }
						_controller.enabled = true; // unfreeze the player
					}
					break;
					
				default: break;
				}
			}
		}
	}

	[RPC]
	void LaunchProjectile(Vector3 offset, float magnitude, bool makeChild){
		GameObject clone;
		clone = Instantiate( _projectile, transform.position + offset, transform.rotation ) as GameObject;
		//clone.rigidbody.velocity = transform.TransformDirection( trajectory * magnitude );

		Vector3 forward = Camera.main.transform.TransformDirection(Vector3.forward);
		forward = forward.normalized;
		clone.rigidbody.velocity = (new Vector3(forward.x * magnitude, 0, forward.z * magnitude));
		
		if( makeChild ){
			clone.transform.parent = this.transform;
		}
	}

	[RPC]
	void BeamAttack(){
		GameObject clone;
		clone = Instantiate (_projectile, transform.position + _offset, transform.rotation) as GameObject;
		//clone.rigidbody.velocity = transform.TransformDirection( trajectory * magnitude );

		Vector3 forward = Camera.main.transform.TransformDirection(Vector3.forward);
		forward = forward.normalized;
		clone.rigidbody.velocity = (new Vector3(forward.x * _magnitude,0,forward.z * _magnitude));
		
		if( _makeChild ){
			clone.transform.parent = this.transform;
		}
	}

	[RPC]
	void LaunchControllable(){
		RaycastHit hit;
		float distance = 20;
		if(Physics.Raycast(transform.position + _offset, this.transform.forward, out hit, distance)){
			distance = hit.distance;
		}
		_controlledProjectile = Instantiate( _projectile, transform.position + _offset + (transform.forward*distance), transform.rotation ) as GameObject;
		//_controlledProjectile.GetComponent<TimedLifespan> ().enabled = false;
		_controlledTarget = Instantiate( _projectile, transform.position + _offset + (transform.forward*distance), transform.rotation ) as GameObject;
		_controlledTarget.renderer.enabled = false;
		_controlledTarget.transform.parent = Camera.main.transform;
	}

	[RPC]
	void MoveControllable(){
		_controller.controllerMoveDirection = Vector2.zero;
		//Get controller direction
		if (_controller.useGamepad){
			_controller.controllerMoveDirection = GamePad.GetAxis(GamePad.Axis.LeftStick, _padIndex);
			_controller.controllerLookDirection = GamePad.GetAxis(GamePad.Axis.RightStick, _padIndex);
		}else{
			_controller.controllerMoveDirection = new Vector2(Input.GetAxis("Horizontal"), -Input.GetAxis("Vertical"));
			_controller.controllerLookDirection = new Vector2(Mathf.Clamp(Input.GetAxis("Mouse X"), -1, 1), Mathf.Clamp(Input.GetAxis("Mouse Y"), -1, 1));
		}

		// get forward direction
		Vector3 forward = Camera.main.transform.TransformDirection(Vector3.forward);
		forward = forward.normalized;
		Vector3 move_direction = _controller.controllerMoveDirection.y * forward + _controller.controllerMoveDirection.x * new Vector3(forward.z, 0, -forward.x);

		//Vector3 targetPosition = forward * Vector3.Distance(_controlledProjectile.transform.position, this.transform.position);
		//apply movement
		//_controlledProjectile.transform.position = targetPosition;//Vector3.Lerp(_controlledProjectile.transform.position, targetPosition + (move_direction*_magnitude), _drag);//.constantForce.force = move_direction * _magnitude;//new Vector3 (move_direction.x * _magnitude, 0, move_direction.z * _magnitude);//
		//Debug.Log ("Forward:" + forward + "; myPosition:" + this.transform.position + "; projectilePosition" + _controlledProjectile.transform.position + "; lookDirection:" + _controller.controllerLookDirection);
		//Debug.Log("; targetPosition:" + targetPosition);
		_controlledTarget.transform.position = _controlledTarget.transform.position + (_controlledTarget.transform.right * _controller.controllerMoveDirection.x * _magnitude) + (_controlledTarget.transform.forward * _controller.controllerMoveDirection.y * _magnitude);
		_controlledProjectile.transform.position = Vector3.Lerp(_controlledProjectile.transform.position, _controlledTarget.transform.position, _drag * Time.deltaTime);
		Debug.Log("targetPosition:" + _controlledTarget.transform.position);
		Debug.Log("projectilePosition:" + _controlledProjectile.transform.position);
	}
}
