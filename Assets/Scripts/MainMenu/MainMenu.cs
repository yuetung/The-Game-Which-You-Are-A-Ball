using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {
    public Button startHost, joinGame;

    void Start() {
        // assign starthost and joingame their onclicks
        // This is done programmically so we are able to assign a new networkmanager as onclick
        NetworkManager_Custom nw = GameObject.FindGameObjectWithTag("NetworkManager").GetComponent<NetworkManager_Custom>();
        Debug.Log(nw.ToString());
        if (startHost && joinGame)
        { 
            startHost.onClick.AddListener(delegate { nw.StartupHost(); });
            joinGame.onClick.AddListener(delegate { nw.joinGame(); });
        }
			
	}

	public void Play1(){
        NetworkManager_Custom.StartSinglePlayer();
        SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex + 1);

	}
	public void Play2(){
		NetworkManager_Custom.StartSinglePlayer();
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
