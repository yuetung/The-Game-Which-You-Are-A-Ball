using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterRoomTrigger : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	// Start spawning if player enters the field
	void OnTriggerEnter2D(Collider2D other) {
		if (other.tag == "Player") {
			MonsterRoomSpawner spawner = GetComponentInParent<MonsterRoomSpawner> ();
			spawner.Spawn ();
		}
	}

}
