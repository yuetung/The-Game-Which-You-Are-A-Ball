using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Portal : MonoBehaviour {
	
	// Use this for initialization
	LevelSelect levelSelect;
	public float tier1Time = 30f;
	public float tier2Time = 40f;
	public float tier3Time = 50f;
	public int tier1Reward = 15;
	public int tier2Reward = 10;
	public int tier3Reward = 5;
	private float startTime;

	public GameObject levelClearedCanvas;
	void Start () {
		levelSelect = gameObject.GetComponent<LevelSelect> ();
		startTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.tag == "Player") {
			Debug.Log ("level up");
			float timeTaken = Time.time - startTime;
			Debug.Log ("Time taken = " + timeTaken);
			PlayerPrefs.SetFloat("Timetaken",timeTaken);
			int currentGold = GameManager.getGold ();
			Debug.Log ("current Gold is " + currentGold);
			if (timeTaken <= tier1Time) {
				Debug.Log ("Awarded " + tier1Reward+" Gold");
				PlayerPrefs.SetInt("Stars",3);
				GameManager.setGold (currentGold + tier1Reward);
			} else if (timeTaken <= tier2Time) {
				Debug.Log ("Awarded " + tier2Reward+" Gold");
				PlayerPrefs.SetInt("Stars",2);
				GameManager.setGold (currentGold + tier2Reward);
			} else if (timeTaken <= tier3Time) {
				Debug.Log ("Awarded " + tier3Reward+" Gold");
				PlayerPrefs.SetInt("Stars",1);
				GameManager.setGold (currentGold + tier3Reward);
			} 
			//TODO: Set LevelCleared Active
			levelClearedCanvas.SetActive(true);

			//TODO: assign reward here based on time taken
			levelSelect.WinLevel ();
		}
	}
}
