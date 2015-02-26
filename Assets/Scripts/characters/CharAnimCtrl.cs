using UnityEngine;
using System.Collections;
using GamepadInput;

public class CharAnimCtrl : MonoBehaviour {
	protected Animator anim;
	DeftPlayerController _controller;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
		anim.SetBool("isIdle", true);
		_controller = GameObject.FindGameObjectWithTag("Player").GetComponent<DeftPlayerController>();
	}
	
	// Update is called once per frame
	void Update () {
		//Tutorial: http://unity3d.com/learn/tutorials/modules/beginner/animation/animator-scripting
		if(anim)
		{
			Vector2 move = GamePad.GetAxis(GamePad.Axis.LeftStick, GamePad.Index.One);
			if(_controller.enabled && (move.x != 0 || move.y != 0)){
				anim.SetBool("isIdle", false);
				anim.SetBool("isRunning", true);
			}
			else{
				anim.SetBool("isIdle", true);
				anim.SetBool("isRunning", false);
			}

			/* //TEMPLATE FROM HERE:  http://docs.unity3d.com/Manual/AnimationParameters.html
			//get the current state
			AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
			
			//if we're in "Run" mode, respond to input for jump, and set the Jump parameter accordingly. 
			if(stateInfo.nameHash == Animator.StringToHash("Base Layer.RunBT"))
			{
				if(Input.GetButton("Fire1")) 
					animator.SetBool("Jump", true );
			}
			else
			{
				animator.SetBool("Jump", false);                
			}
			
			float h = Input.GetAxis("Horizontal");
			float v = Input.GetAxis("Vertical");
			
			//set event parameters based on user input
			animator.SetFloat("Speed", h*h+v*v);
			animator.SetFloat("Direction", h, DirectionDampTime, Time.deltaTime);
			*/
		}       
	}
}
