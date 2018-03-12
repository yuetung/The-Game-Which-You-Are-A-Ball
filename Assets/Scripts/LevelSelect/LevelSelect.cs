using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelect : MonoBehaviour {
	//When Level won, transition PlayerPrefs to next level...
	//Levels get unlock according. Level is automatically faded to new Level. 
	public void WinLevel(){
		int currentLevel = SceneManager.GetActiveScene ().buildIndex;
		PlayerPrefs.SetInt ("LevelReached", currentLevel+1);
		Debug.Log (PlayerPrefs.GetInt ("LevelReached"));
		//NetworkManager_Custom nw = GameObject.FindGameObjectWithTag("NetworkManager").GetComponent<NetworkManager_Custom>();
		NetworkManager_Custom.StartSinglePlayer();
		SceneManager.LoadScene(currentLevel+1);
	}
}

