﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpPickup : MonoBehaviour {

	public PlayerControler.ElementType elementType;
	public float energy=10.0f;
	[Tooltip("explosion particle effect")]
	public GameObject explosion;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	// Collision with player
	void OnTriggerEnter2D(Collider2D other) {
		if (other.tag == "Player") {
			other.gameObject.GetComponent<PlayerControler> ().gainPowerUp (elementType, energy);
			if (explosion) {
				Instantiate (explosion, transform.position, transform.rotation);
			}
			DestroyObject (this.gameObject);
		}

	}

}
