using UnityEngine;
using System.Collections;

public class depotAbsorb : MonoBehaviour {
	public string supplyItemTag;

	public int capacity = 5000;
	public int resourceVal = 100;
	public int currentStock = 0;
	public float animGrowRate = 0.1f;
	//Link to game manager to update stats
	public GameObject gameManager;
	private InGameStats gameStats;
	private bool depotFull;

	private Animator myAnim;

	void Start(){
		myAnim = this.GetComponent<Animator>();
		myAnim.SetBool("isStopped", true);
		myAnim.SetBool("isPlaying", false);
		myAnim.speed = 0.0f;
		gameStats = gameManager.GetComponent<InGameStats> ();
		depotFull = false;
	}

	public int getCurrentSize() {
		return currentStock;
	}
	public int getSize() {
		return capacity;
	}

	void OnTriggerEnter(Collider other){

		/*
		 * replace 10 with other.gameObject.value
		 * 
		 *
		 */
		if(other.tag == supplyItemTag){
			currentStock = currentStock + resourceVal;

			if ((this.currentStock + resourceVal) <= capacity) {
				Debug.Log("Growing");
				this.currentStock += resourceVal;
				myAnim.SetBool("isPlaying", true);
				myAnim.SetBool("isStopped", false);
				myAnim.Play("depotGrow", 0, myAnim.GetCurrentAnimatorStateInfo(0).length * currentStock/capacity * animGrowRate);
			}else if (!depotFull) {
				Debug.Log ("Resource Depot FuLL");
				gameStats.depotFull();
				depotFull = true;
			}
			//this.transform.parent.transform.localScale += (Vector3.up * 0.1f);
			Destroy( other.gameObject );
		}

	}

}
