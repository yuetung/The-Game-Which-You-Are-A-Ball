using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour {
	[Tooltip("explosion particle effect")]
	public GameObject explosion;
	private int value;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	// Collision with player
	void OnTriggerEnter2D(Collider2D other) {
		if (other.tag == "Player") {
			int currentGold = GameManager.getGold ();
			GameManager.setGold (currentGold + value);
			if (explosion) {
				Instantiate (explosion, transform.position, transform.rotation);
			}
			DestroyObject (this.gameObject);
		}

	}
	public void setValue(int value){
		this.value = value;
	}
}
	