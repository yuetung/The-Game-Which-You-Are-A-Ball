using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour {

	public static bool GameIsPaused = false;
	public bool isSinglePlayer = true;
	public GameObject pauseMenuUI;
	public GameObject shopMenuUI;
	private GameObject player;
	// Update is called once per frame
    void Start()
    {
        Resume();
    }
	void Update () {
		if (Input.GetKeyDown (KeyCode.Escape)) {
			Pausing ();
		}
	}
	public void Pausing(){
		
		if (GameIsPaused) {
			Debug.Log ("Resuming");
			Resume ();
		} else {
			Debug.Log ("Pausing");
			Pause ();
		}
	}
	public void Resume(){
		pauseMenuUI.SetActive (false);
        if (shopMenuUI != null)
        {
            shopMenuUI.SetActive(false);
        }
		Time.timeScale = 1f;
		GameIsPaused = false;
		if (player==null)
			player = GameObject.FindGameObjectWithTag ("Player");
		player.GetComponent<PlayerController> ().paused = false;
	}

	public void Pause(){
		pauseMenuUI.SetActive (true);
		if (isSinglePlayer)
			Time.timeScale = 0f;
		GameIsPaused = true;
		if (player==null)
			player = GameObject.FindGameObjectWithTag ("Player");
		player.GetComponent<PlayerController> ().paused = true;
	}
}

