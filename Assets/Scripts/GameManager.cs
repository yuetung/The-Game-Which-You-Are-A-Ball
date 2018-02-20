using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public static GameManager gm;

	public Text mainHealthDisplay;
	public Text mainEnergyDisplay;
	public Text mainElementDisplay;

	// Use this for initialization
	void Start () {
		if (gm == null)
			gm = this.gameObject.GetComponent<GameManager> ();
		mainHealthDisplay.text = "Health: 0";
	}
	
	// Update is called once per frame
	void Update () {
		 
	}

	//create powerup function to be called to update the energy

	//create healthreduced function to be called to update the health bar

	//create powerreduced function to be called to update the energy

	// create elementchange function to be called to update the element
}
