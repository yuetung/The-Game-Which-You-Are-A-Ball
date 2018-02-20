using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	public static GameManager gm;

	public enum MyElement {NONE, FIRE, WATER, LIGTHNING};

	public int health = 100;
	public int energy = 0;
	public MyElement element = MyElement.NONE;
	public int level = 1;

	public Text mainHealthDisplay;
	public Text mainEnergyDisplay;
	public Text mainElementDisplay;
	public Text mainLevelDisplay;
	public Text mainGameOverDisplay;

	public int counter = 1;

	// get a reference to the GameManager component for use by other scripts
	void Start () {
		if (gm == null)
			gm = this.gameObject.GetComponent<GameManager> ();

		// init health to 100
		mainHealthDisplay.text = "Health: 100";
		mainEnergyDisplay.text = "Energy: 0";
		mainElementDisplay.text = "Element: None";
		mainLevelDisplay.text = "Level: 1";
		mainGameOverDisplay.text = "";
	}
	
	// Update is called once per frame
	// Update is used to check to run idle state
	void Update () {

		// check if health < 0
		if (health <= 0) {
			EndGame ();
		}

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
		double multiplier = 0.5;
		if (counter % (50/(multiplier*level)) == 1){
			energyDown (5);
		}

		// counter for health decrement 
		counter += 1;
	}

	void EndGame(){
		mainGameOverDisplay.text = "GAME OVER";
	}

	//create energyUp function to be called when collecting
	public void energyUp(int energyAmount){
		if (energy + energyAmount > 100) {
			energy = (energy + energyAmount) % 100;
			levelUp ();
		} else {
			energy += energyAmount;
		}
		mainEnergyDisplay.text = "Energy: " + energy.ToString();
	}

	// create energyDown function to be called with passing of time
	public void energyDown(int energyAmount){
		if (energy - energyAmount < 0) {
			if (level != 1) {
				energy = 100;
				levelDown ();
			} else {
				energy = 0;
			}
		} else {
			energy -= energyAmount;
		}
		mainEnergyDisplay.text = "Energy: " + energy.ToString ();
	}

	// create healthUp function to be called when needed?
	public void healthUp(int healthAmount){
		health += healthAmount;
		mainHealthDisplay.text = "Health: " + health.ToString ();
	}

	// create healthDown function to be called when damaged by projectiles
	public void healthDown(int healthAmount){
		if (health - healthAmount < 0) {
			health = 0;
		} else {
			health -= healthAmount;
		}
		mainHealthDisplay.text = "Health: " + health.ToString ();
	}
		
	// create elementChange function to be called to update the element
	// only called when the energy of the player is different from the new pickup
	public void elementChange(MyElement newElement, int energyAmount){
		element = newElement;
		mainElementDisplay.text = "Element: " + element.ToString ();

		level = 1;
		mainLevelDisplay.text = "Level: " + level.ToString ();

		energy = energyAmount;
		mainEnergyDisplay.text = "Energy: " + energy.ToString ();
	}

	// create levelUp function to be called when element more than 100%
	public void levelUp(){
		level += 1;
		mainLevelDisplay.text = "Level: " + level.ToString ();
	}

	// create levelDown function to be called when element less than 0%
	public void levelDown(){
		level -= 1;
		mainLevelDisplay.text = "Level: " + level.ToString ();
	}
}
