using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelect : MonoBehaviour {

	//When Level won, transition PlayerPrefs to next level...
	//Levels get unlock according. Level is automatically faded to new Level. 
	public void WinLevel(){
		int currentLevel = SceneManager.GetActiveScene ().buildIndex;
		PlayerPrefs.SetInt ("LevelReached", currentLevel+1);
	}
	public void Reset(){
		PlayerPrefs.SetInt ("LevelReached", 1);
	}
}
