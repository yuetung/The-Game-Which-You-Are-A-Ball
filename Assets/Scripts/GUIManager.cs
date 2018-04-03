﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class GUIManager : NetworkBehaviour {

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

	public Slider HealthBar;
	public Transform LoadingBar;
	public Text LevelText2;

	public int counter = 1;
    float maxHealth;

	// Possible element types (copied from PlayerController.cs
//	public enum ElementType {
//		Default,
//		Fire,
//		Water,
//		Lightning,
//		Earth,
//		Wind,
//		Antimatter
//	};

	// get a reference to the GameManager component for use by other scripts
	void Start () {

		// init health to 100
		mainHealthDisplay.text = "Health: 3";
		mainEnergyDisplay.text = "Energy: 0";
		mainElementDisplay.text = "Element: Default";
		mainLevelDisplay.text = "Level: 1";
		mainGameOverDisplay.text = "";
		HealthBar.value = 1.0f;
		LoadingBar.GetComponent<Image> ().fillAmount = 0.0f;
		LevelText2.text = "1";
        maxHealth = 8f;
	}

	public void register(GameObject player) {
		playerController = player.GetComponent<PlayerController> ();
	}

	public void EndGame(){
		mainGameOverDisplay.text = "GAME OVER";
	}

    public void WinGame()
    {
        mainGameOverDisplay.text = "YOU WIN";
    }

    public void updateEnergy(int amount){
		energy = amount;
		mainEnergyDisplay.text = "Energy: " + energy.ToString();

		// TODO: change the display of energy into a radial energy bar
		LoadingBar.GetComponent<Image>().fillAmount = energy/100.0f;
	}
		
	public void updateHealth(int amount){
		health = amount;
		if (health <= 0) {
			health = 0;
			EndGame ();
		}
		// Text UI
		mainHealthDisplay.text = "Health: " + health.ToString ();

		// Slidebar UI
		float health2 = health/maxHealth;
        
		HealthBar.value = health2;
	}

    public void updateHealth(int amount, int maxHealth)
    {
        this.maxHealth = maxHealth;
        updateHealth(amount);
    }
		
	public void updateElement(PlayerController.ElementType newElement){
		elementType = newElement;
		mainElementDisplay.text = "Element: " + elementType.ToString ();

		//TODO: 
		if (newElement == PlayerController.ElementType.Fire) {
			// set the color of the radial energy bar
			LoadingBar.GetComponent<Image>().color = Color.red;
		} else if (newElement == PlayerController.ElementType.Water) {
			// set the color of the radial energy bar
			LoadingBar.GetComponent<Image>().color = Color.blue;
		} else if (newElement == PlayerController.ElementType.Lightning) {
			// set the color of the radial energy bar
			LoadingBar.GetComponent<Image>().color = Color.yellow;
		} else if (newElement == PlayerController.ElementType.Earth) {
            // set the color of the radial energy bar
            LoadingBar.GetComponent<Image>().color = new Color(120 / 255f, 82 / 255f, 45 / 255f);
        }

    }

	public void updateLevel (int amount) {
		level = amount;
		mainLevelDisplay.text = "Level: " + level.ToString ();

		//TODO: display the level text in the middle of the radial energy bar
		LevelText2.text = level.ToString();
	}

	public void updateAll() {
		updateEnergy (playerController.energy);
		updateHealth (playerController.health);
		updateElement (playerController.elementType);
		updateLevel (playerController.elementLevel);
	}
}
