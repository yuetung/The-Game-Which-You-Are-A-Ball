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
        NetworkManager.singleton.StartMatchMaker();
        Debug.Log(nw.ToString());
        if (startHost && joinGame)
        { 
            startHost.onClick.AddListener(delegate { nw.CreateInternetMatch(); });
            joinGame.onClick.AddListener(delegate { nw.FindInternetMatch(); });
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
	public void Play3(){
		NetworkManager_Custom.StartSinglePlayer();
		SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex + 3);
	}
	public void testEnemy(){
		NetworkManager_Custom.StartSinglePlayer();
		SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex + 4);
	}
	public void Quit(){
		Debug.Log ("Quit");
		Application.Quit();
	}
}
