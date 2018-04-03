﻿using UnityEngine;

public class PowerUpPickup : MonoBehaviour {

	public PlayerController.ElementType elementType;
	public int energy=10;
	[Tooltip("explosion particle effect")]
	public GameObject explosion;

	// Use this for initialization
	void Start () {
		//transform.SetParent (GameObject.Find("PowerUps").transform);
	}

	// Collision with player
	void OnTriggerEnter2D(Collider2D other) {
		if (other.tag == "Player") {
			other.gameObject.GetComponent<PlayerController> ().gainPowerUp (elementType, energy);
			Invoke ("DestroyNow", 0.02f);
		}
	}

	public void setEnergy(int amount){
		energy = amount;
		float scaleFactor = Mathf.Log10 (amount)/2;
		transform.localScale = new Vector3 (scaleFactor, scaleFactor, 1);
	}

	void DestroyNow(){
		if (explosion) {
			Instantiate (explosion, transform.position, transform.rotation);
		}
		DestroyObject (this.gameObject);

	}
}
