
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelectMenu : MonoBehaviour {
	public Button level02Button, level03Button,level04Button;
	int levelReached;

	void Start() {

		levelReached = PlayerPrefs.GetInt ("LevelReached");
		Debug.Log ("PlayerPref int is "+levelReached);
		level02Button.interactable = false;
		level03Button.interactable = false;
		level04Button.interactable = false;

		switch (levelReached) {
		case 1:
			level02Button.interactable = false;
			level03Button.interactable = false;
			level04Button.interactable = false;
			break;
		case 2:
			level02Button.interactable=true;
			break;
		case 3:
			level02Button.interactable=true;
			level03Button.interactable = true;
			break;
		case 4:
			level02Button.interactable = true;
			level03Button.interactable = true;
			level04Button.interactable = true;
			break;
		default:
			level02Button.interactable = true;
			level03Button.interactable = true;
			level04Button.interactable = true;
			break;
			
		}
	}

	public void Reset(){
		PlayerPrefs.SetInt ("LevelReached", 1);
	}

}



