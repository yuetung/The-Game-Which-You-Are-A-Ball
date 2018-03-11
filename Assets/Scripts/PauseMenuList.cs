using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuList : MonoBehaviour {

	public void MainMenu(){
		SceneManager.LoadScene (0);

	}
	public void Quit(){
		Debug.Log ("Quit");
		Application.Quit();
	}
}
