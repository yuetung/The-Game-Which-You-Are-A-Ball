using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuList : MonoBehaviour {

	public void MainMenu(){
        GameObject networkManager = GameObject.FindGameObjectWithTag("NetworkManager");
        GameObject.Destroy(networkManager);
        NetworkManager_Custom.Shutdown();
		SceneManager.LoadScene (0);
	}

	public void Quit(){
		Debug.Log ("Quit");
		Application.Quit();
	}

    public static void MainMenuStatic()
    {
        GameObject networkManager = GameObject.FindGameObjectWithTag("NetworkManager");
        GameObject.Destroy(networkManager);
        NetworkManager_Custom.Shutdown();
        SceneManager.LoadScene(0);
    }
}
