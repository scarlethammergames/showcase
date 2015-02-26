using System.Collections;
using System.Reflection;
using GamepadInput;
using UnityEngine;


public enum PlayerState { aiming, walking, running, sprinting, jumping };


/// <summary>
/// 
/// The player state is checked with the following precedence:
///   1. Aiming
///   2. Running
///   3. Sprinting
///   4. Walking
/// 
/// </summary>
public class DeftPlayerController : MonoBehaviour
{
	
	public float speed_aim = 0.25f;
	public float speed_walk = 0.25f;
	public float speed_run = 1.0f;
	public float speed_sprint = 2.0f;
	public float jump_height = 5.0f;
	public float jump_cooldown = 1.0f;
	public float smoothing_turn = 2.0f;
	public float smoothing_aim = 5.0f;
	
	public float smooth = 20f;
	
	public float player_height;
	public float player_width;
	
	public bool debug;
	public bool use_gamepad;
	
	public bool is_grounded;
	public PlayerState state;
	
	public bool inverted = false;
	float invert_timer = 0;
	
	GamePad.Index pad_index = GamePad.Index.One;
	
	float speed_current;
	Vector3 move_direction;
	Vector3 forward;
	Vector3 last_input;
	
	Animator animator;
	
	bool isAiming = false;
	
	void Awake()
	{
		animator = this.GetComponent<Animator>();
		Camera.main.GetComponent<DeftPlayerCamera>().player = this.gameObject;
		Debug.Log("PLAYER IS AWAKE");
		Camera.main.GetComponent<DeftPlayerCamera>().Reset();
	}
	
	
	
	/// <summary>
	/// Variables stored here for performance purposes and if anothere class would like to observe.
	/// </summary>
	public bool controller_jump;
	public bool controller_run;
	public bool controller_sprint;
	public float controller_aim;
	public Vector2 controller_move_direction;
	public Vector2 controller_look_direction;
	public Vector2 dpad_down;
	
	void Update()
	{
		if (use_gamepad) {
			controller_jump = GamePad.GetButtonDown (GamePad.Button.A, pad_index);
			controller_run = GamePad.GetButton (GamePad.Button.LeftStick, pad_index);
			controller_sprint = GamePad.GetButton (GamePad.Button.RightStick, pad_index);
			//controller_aim = GamePad.GetTrigger (GamePad.Trigger.LeftTrigger, pad_index);
			dpad_down = GamePad.GetAxis (GamePad.Axis.Dpad, pad_index);
		} else {
			controller_jump = Input.GetKeyDown (KeyCode.Space);
			controller_run = Input.GetKey (KeyCode.LeftShift);
			controller_sprint = Input.GetKey (KeyCode.C);
			//controller_aim = Input.GetMouseButton(1);
		}
		
		invert_timer += Time.deltaTime;
		
		// invert y axis if down on dpad is pressed
		if (dpad_down.y < 0 && invert_timer > 1) {
			if(inverted)
				inverted = false;
			else
				inverted = true;
			
			invert_timer = 0;
		}
		this.controller_move_direction = new Vector3 (0, 0, 0);
		this.controller_look_direction = new Vector3 (0, 0, 0);
		if (use_gamepad)
		{
			this.controller_move_direction = GamePad.GetAxis(GamePad.Axis.LeftStick, pad_index);
			this.controller_look_direction = GamePad.GetAxis(GamePad.Axis.RightStick, pad_index);
		}
		else
		{
			this.controller_move_direction = new Vector2(Input.GetAxis("Horizontal"), -Input.GetAxis("Vertical"));
			this.controller_look_direction = new Vector2(Mathf.Clamp(Input.GetAxis("Mouse X"), -1, 1), Mathf.Clamp(Input.GetAxis("Mouse Y"), -1, 1));
		}
		
		
		if(GamePad.GetButtonDown(GamePad.Button.X, pad_index)){
			if(isAiming)
				isAiming = false;
			else
				isAiming = true;
		}
		
		/*if(isAiming && (this.controller_move_direction.x > 0 || this.controller_move_direction.y > 0)){
			isAiming = false;
		}	*/
		
		if (!isAiming) {
			Camera.main.GetComponent<Crosshair>().enabled = false;
		}
		
		if (isAiming)
		{
			Camera.main.GetComponent<Crosshair> ().enabled = true;
			this.state = PlayerState.aiming;
		}
		else if (controller_jump)
		{
			this.state = PlayerState.jumping;
		}
		else if (controller_sprint && controller_run)
		{
			this.state = PlayerState.sprinting;
		}
		else if (controller_run)
		{
			this.state = PlayerState.running;
		}
		else
		{
			this.state = PlayerState.walking;
		}
		
		if (inverted)
			this.controller_look_direction.y *= -1;
		
	}
	
	void FixedUpdate()
	{
		if (debug)
		{
			foreach (FieldInfo info in this.gameObject.GetComponent<DeftPlayerController>().GetType().GetFields())
			{
				if (info.Name.Contains("controller_"))
					Debug.Log(info.Name + ": " + info.GetValue(this.gameObject.GetComponent<DeftPlayerController>()));
			}
			Debug.Log("CURRENT STATE: " + this.state.ToString());
		}
		
		Animate();
		// last jump
		
		// get forward direction
		forward = Camera.main.transform.TransformDirection(Vector3.forward);
		forward = forward.normalized;
		
		this.move_direction = this.controller_move_direction.y * forward + this.controller_move_direction.x * new Vector3(forward.z, 0, -forward.x);
		
		if (this.move_direction.x != 0 || this.move_direction.z != 0) {
			last_input = move_direction;
		}
		
		switch (this.state)
		{
		case PlayerState.aiming:
		{
			speed_current = speed_aim;
			break;
		}
		case PlayerState.jumping:
		{
			if (speed_current > 0)
			{
				//rigidbody.velocity = new Vector3(0, jump_height, 0);
				rigidbody.AddForce (new Vector3(0, jump_height, 0));
			}
			break;
		}
		case PlayerState.sprinting:
		{
			speed_current = speed_sprint;
			break;
		}
		case PlayerState.running:
		{
			speed_current = speed_run;
			break;
		}
		default:
		{
			speed_current = speed_walk;
			break;
		}
		}
		
		// change forward direction
		Vector3 last_input_without_y = new Vector3 (last_input.x, 0, last_input.z);
		Vector3 forward_without_y = new Vector3 (transform.forward.x, 0, transform.forward.z);
		
		transform.forward = Vector3.Lerp (forward_without_y, last_input_without_y, smooth * Time.deltaTime);
		
		//this.rigidbody.AddForce(this.move_direction * speed_current);
		//Vector3 move_without_y = new Vector3 (this.move_direction.x, 0, this.move_direction.z);
		//this.rigidbody.velocity = (move_without_y * speed_current);
		rigidbody.velocity = new Vector3 (move_direction.x * speed_current, rigidbody.velocity.y, move_direction.z * speed_current);
		
		// THIS IS A MASSIVE HACK VERY BAD WILL FIX SOON AFTER I GET SOME MILK!!
		if (this.controller_move_direction.x == 0 && this.controller_move_direction.y == 0) {
			this.rigidbody.velocity = new Vector3(0,rigidbody.velocity.y,0);
		}
	}
	
	public void Animate()
	{
		
	}
	
	
	public bool CalculateIsGrounuded()
	{
		return Physics.Raycast(transform.position, Vector3.down, (this.player_height / 2.0f) + 0.05f);
	}
	
}
