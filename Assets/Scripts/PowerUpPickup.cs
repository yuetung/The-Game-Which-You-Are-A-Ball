﻿using UnityEngine;

public class PowerUpPickup : MonoBehaviour {

	public PlayerController.ElementType elementType;
	public int energy=10;
	[Tooltip("explosion particle effect")]
	public GameObject explosion;

	// Use this for initialization
	void Start () {
		transform.SetParent (GameObject.Find("PowerUps").transform);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	// Collision with player
	void OnTriggerEnter2D(Collider2D other) {
		if (other.tag == "Player") {
			other.gameObject.GetComponent<PlayerController> ().gainPowerUp (elementType, energy);
			if (explosion) {
				Instantiate (explosion, transform.position, transform.rotation);
			}
			DestroyObject (this.gameObject);
		}

	}

	public void setEnergy(int amount){
		energy = amount;
	}

}
