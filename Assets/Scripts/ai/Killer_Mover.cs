using UnityEngine;
using System.Collections;

public class Killer_Mover : AI_Mover {

	public GameObject gameManager;
	private InGameStats gameManagerStats;
	
	// Use this for initialization
	void Start () {

		//setting agent
		this.agent = GetComponent<NavMeshAgent> ();

		this.prevWaypoint = this.waypoint;
		
		gameObject.renderer.material.color = Color.green;

		gameManagerStats = gameManager.GetComponent<InGameStats> ();
	}
	
	// Update is called once per frame
	void Update () 
	{

		move ();
		
	}

	protected override void react()
	{
//		Debug.Log ("reacting");
		updateWaypoint (GameObject.FindGameObjectWithTag ("Player").gameObject.transform);

	}

	public override void isInterested()
	{
		
		this.interested = true;
		
		gameObject.renderer.material.color = Color.red;
		
		react ();
		
	}

	protected void OnCollisionEnter(Collision other)
	{
		
		if (other.gameObject.tag.Equals ("projectile") || other.gameObject.tag.Equals ("Projectile")) {
			
			Destroy (other.gameObject);
			
			if (this.Health <= 0) {
				
				Destroy (gameObject);
				
				return;
				
			}	
			
			this.Health -= damageTaken;
			
		} else if (other.gameObject.tag.Equals ("Player")) {
//			Debug.Log("HIT!!!!!!!!!!!!!!!");
			gameManagerStats.decreaseHealth(other.gameObject.name, gameObject.name);
		}
		
	}

	public void notInterested()
	{
		
		this.interested = false;
		
		this.waypoint = this.prevWaypoint;
		
		gameObject.renderer.material.color = Color.green;
		
	}

}
