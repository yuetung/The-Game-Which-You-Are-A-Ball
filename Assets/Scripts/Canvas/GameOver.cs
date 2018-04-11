using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameOver : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public void Retry(){
		NetworkManager_Custom nw = GameObject.FindGameObjectWithTag("NetworkManager").GetComponent<NetworkManager_Custom>();
		nw.ServerChangeScene ("Level" + (SceneManager.GetActiveScene ().buildIndex));
		Time.timeScale = 1f;
	}
	public void MainMenu(){
		GameObject networkManager = GameObject.FindGameObjectWithTag("NetworkManager");
		GameObject.Destroy(networkManager);
		NetworkManager_Custom.Shutdown();
		SceneManager.LoadScene (0);
	}
}
