﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour {

	public static bool GameIsPaused = false;
	public GameObject pauseMenuUI;
	public GameObject shopMenuUI;
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Escape)) {
			if (GameIsPaused) {
				Resume ();
			} else {
				Pause();
			}
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
	}
	public void Pause(){
		pauseMenuUI.SetActive (true);
		Time.timeScale = 0f;
		GameIsPaused = true;
	}
}

