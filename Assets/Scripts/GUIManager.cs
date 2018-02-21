using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUIManager : MonoBehaviour {

	public int health = 100;
	public int energy = 0;
	public PlayerController.ElementType elementType = PlayerController.ElementType.Default;
	public int level = 0;
	public PlayerController playerController;
	public Text mainHealthDisplay;
	public Text mainEnergyDisplay;
	public Text mainElementDisplay;
	public Text mainLevelDisplay;
	public Text mainGameOverDisplay;

	public int counter = 1;

	// get a reference to the GameManager component for use by other scripts
	void Start () {

		// init health to 100
		mainHealthDisplay.text = "Health: 100";
		mainEnergyDisplay.text = "Energy: 0";
		mainElementDisplay.text = "Element: Default";
		mainLevelDisplay.text = "Level: 1";
		mainGameOverDisplay.text = "";
	}
	
	// Update is called once per frame
	// Update is used to check to run idle state
	void Update () {

		// check if health < 0
//		if (health <= 0) {
//			EndGame ();
//		}

		// check if energy > 100
//		if (energy > 100) {
//			energy = energy - 100;
//			mainEnergyDisplay.text = "Energy: " + energy.ToString ();
//			levelUp ();
//		}

		// check if energy < 0 to reduce the level 
//		if (energy < 0) {
//			if (level != 1) {
//				energy = 100;
//				mainEnergyDisplay.text = "Energy: " + energy.ToString ();
//				levelDown ();
//			} else {
//				energy = 0;
//				mainEnergyDisplay.text = "Energy: " + energy.ToString ();
//			}
//		}

		// energy will reduce by proportion to level with passing of time
		// energy decay in multiples of 0.5 instead of 1
//		double multiplier = 0.5;
//		if (counter % (50/(multiplier*level)) == 1){
//			energyDown (5);
//		}

		// counter for health decrement 
//		counter += 1;
	}

	public void register(GameObject player) {
		playerController = player.GetComponent<PlayerController> ();
	}

	void EndGame(){
		mainGameOverDisplay.text = "GAME OVER";
	}
		
	public void updateEnergy(int amount){
		energy = amount;
		mainEnergyDisplay.text = "Energy: " + energy.ToString();
	}
		
	public void updateHealth(int amount){
		health = amount;
		if (health <= 0) {
			health = 0;
			EndGame ();
		}
		mainHealthDisplay.text = "Health: " + health.ToString ();
	}
		
	public void updateElement(PlayerController.ElementType newElement){
		elementType = newElement;
		mainElementDisplay.text = "Element: " + elementType.ToString ();
	}

	public void updateLevel (int amount) {
		level = amount;
		mainLevelDisplay.text = "Level: " + level.ToString ();
	}

	public void updateAll() {
		updateEnergy (playerController.energy);
		updateHealth (playerController.health);
		updateElement (playerController.elementType);
		updateLevel (playerController.elementLevel);
	}
}
