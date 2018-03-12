using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {
	public Button level02Button, level03Button,level04Button;
    public Button startHost, joinGame;
	int levelReached;

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

        levelReached = PlayerPrefs.GetInt ("LevelReached")-1;
		level02Button.interactable = false;
		level03Button.interactable = false;
		level04Button.interactable = false;

		switch (levelReached) {
		case 2:
			level02Button.interactable=true;
			break;
		case 3:
			level02Button.interactable=true;
			level03Button.interactable = true;
			break;
		case 4:
			level02Button.interactable = false;
			level03Button.interactable = false;
			level04Button.interactable = false;
			break;
		}
	}

	public void levelToLoad(int level){
		SceneManager.LoadScene (level);
	}

	public void Play1(){
        NetworkManager_Custom.StartSinglePlayer();
        SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex + 2);

	}
	public void Play2(){
        Debug.Log(SceneManager.GetActiveScene().buildIndex);
        SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex + 3);
        
	}
	public void BossAi(){
		SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex + 4);
	}
	public void Quit(){
		Debug.Log ("Quit");
		Application.Quit();
	}
}
