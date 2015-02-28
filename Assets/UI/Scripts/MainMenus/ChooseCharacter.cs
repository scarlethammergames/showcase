using UnityEngine;
using System.Collections;

public class ChooseCharacter : MonoBehaviour {
	public bool Syphen=false;
	public bool Blitz=false;

	// Use this for initialization
	void Start () {
		GlobalVariables.syphen = this.Syphen;
		GlobalVariables.blitz = this.Blitz;
	}

}
