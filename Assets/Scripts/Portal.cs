using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour {
	
	// Use this for initialization
	LevelSelect levelSelect;
	float tier1Time = 30f;
	float tier2Time = 40f;
	float tier3Time = 50f;
	int tier1Reward = 15;
	int tier2Reward = 10;
	int tier3Reward = 5;
	private float startTime;
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
			int currentGold = GameManager.getGold ();
			Debug.Log ("current Gold is " + currentGold);
			if (timeTaken <= tier1Time) {
				Debug.Log ("Awarded " + tier1Reward+" Gold");
				GameManager.setGold (currentGold + tier1Reward);
			} else if (timeTaken <= tier2Time) {
				Debug.Log ("Awarded " + tier2Reward+" Gold");
				GameManager.setGold (currentGold + tier2Reward);
			} else if (timeTaken <= tier3Time) {
				Debug.Log ("Awarded " + tier3Reward+" Gold");
				GameManager.setGold (currentGold + tier3Reward);
			} 

			//TODO: assign reward here based on time taken
			levelSelect.WinLevel ();
		}
	}
}
