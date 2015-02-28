﻿using UnityEngine;
using System.Collections;

public class PlayerSelect : MonoBehaviour
{

  public bool active;


  public GameObject playerTypeA;
  public GameObject playerTypeB;

  private GameObject[] selectablePlayers;
  public GameObject selectedPlayer = null;

  void OnGUI()
  {
      for (int i = 0; i < selectablePlayers.Length; i++)
        if (GUI.Button(new Rect(400, 100 + (110 * i), 300, 100), selectablePlayers[i].name))
        {
          selectedPlayer = selectablePlayers[i];
        }
  }

  // Use this for initialization
  void Start()
  {
    selectablePlayers = new GameObject[] { playerTypeA, playerTypeB };
    this.enabled = false;
  }

  // Update is called once per frame
  void Update()
  {

  }

  public void SpawnPlayer()
  {
	TutorialManager tutorialManager = gameObject.GetComponent<TutorialManager> ();
	tutorialManager.StartTutorial (selectedPlayer.name);
		int playerID;
		if (selectedPlayer.name.Contains("Syphen")) {
			playerID=0;
		} else {
			playerID=1;
		}
	HealthBar hb = GameObject.Find ("HealthBar").GetComponent<HealthBar> ();
	hb.StartHealthBar (playerID);
	HealthStats hs = GameObject.Find ("HealthStats").GetComponent<HealthStats> ();
	hs.StartStats(playerID);
    Vector3 spawnPoint = Camera.main.transform.position + Camera.main.transform.forward * 10;
    Network.Instantiate(selectedPlayer, spawnPoint, Quaternion.identity, 0);
    this.enabled = false;
  }

}
