using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    // Should be for Single Player
	public void Play1(){
        NetworkManager_Custom.StartSinglePlayer();
		SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex + 1);
	}

    // Multiplayer
	public void Play2(){
        Debug.Log(SceneManager.GetActiveScene().buildIndex);
        SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex + 2);
        
	}
	public void BossAi(){
		SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex + 3);
	}
	public void Quit(){
		Debug.Log ("Quit");
		Application.Quit();
	}
}
