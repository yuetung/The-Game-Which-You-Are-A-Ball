using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HintsBoard : MonoBehaviour {

	public GameObject speechBubble;
	public GameObject textMeshPro;
	void Start () {
		speechBubble.SetActive (false);
		textMeshPro.SetActive (false);
	}

	// Update is called once per frame
	void Update () {

	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.tag == "Player") {
			Debug.Log("Displaying Hint");
			speechBubble.SetActive (true);
			textMeshPro.SetActive (true);
		}
	}

	void OnTriggerExit2D(Collider2D other){
		if (other.tag == "Player") {
			Debug.Log("Removing Hint");
			speechBubble.SetActive (false);
			textMeshPro.SetActive (false);
		}
	}
}
