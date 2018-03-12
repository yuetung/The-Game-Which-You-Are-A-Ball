using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour {
	
	// Use this for initialization
	LevelSelect levelSelect;
	void Start () {
		levelSelect = gameObject.GetComponent<LevelSelect> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.tag == "Player") {
			Debug.Log ("level up");
			levelSelect.WinLevel ();
		}
	}
}
