using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

	public void Play1(){
		SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex + 1);

	}
	public void Play2(){
        Debug.Log(SceneManager.GetActiveScene().buildIndex);
        SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex + 2);
        
	}
	public void BossAi(){
		SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex + 2);
	}
	public void Quit(){
		Debug.Log ("Quit");
		Application.Quit();
	}
}
