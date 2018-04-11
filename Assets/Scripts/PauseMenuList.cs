using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuList : MonoBehaviour {

	private GameObject player;

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
	public void Retry(){
		NetworkManager_Custom nw = GameObject.FindGameObjectWithTag("NetworkManager").GetComponent<NetworkManager_Custom>();
		nw.ServerChangeScene ("Level" + (SceneManager.GetActiveScene ().buildIndex));
		Time.timeScale = 1f;
		if (player==null)
			player = GameObject.FindGameObjectWithTag ("Player");
		player.GetComponent<PlayerController> ().paused = false;
		PauseMenu.GameIsPaused = false;
	}
}
