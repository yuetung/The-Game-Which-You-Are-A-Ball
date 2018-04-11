using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterRoomTrigger : MonoBehaviour {

	public bool hasBossMonster = false;
	public GameObject[] bossEnemies;
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
			if (hasBossMonster){
				for (int i = 0; i < bossEnemies.Length; i++) {
					bossEnemies [i].GetComponent<BossEnemy> ().openEye ();
				}
			}
		}
	}


}
