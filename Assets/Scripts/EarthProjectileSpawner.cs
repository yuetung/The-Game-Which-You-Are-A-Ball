using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class EarthProjectileSpawner : NetworkBehaviour {

	public float regenerationTime = 50.0f;
	public float rotationSpeed = 1.0f;
	public float radius = 1.0f;
	public GameObject earthProjectilePrefab;
	public int maxSpawn = 3;
	public GameObject[] earthProjectiles;
	public bool belongToPlayer= false;
	private float angleDifference;
	public float timeCounter = 0.0f;

	[SyncVar]
	public GameObject shooter;

	// Use this for initialization
	void Start () {
		earthProjectiles = new GameObject[maxSpawn];
		angleDifference = 360 / maxSpawn;
		timeCounter = regenerationTime;
	}
	
	// Update is called once per frame
	void Update () {
		// follow parent's transform
		if (shooter)
			transform.position = shooter.transform.position;

		// Rotate around self
		transform.Rotate(Vector3.forward*Time.deltaTime*rotationSpeed);

		// Spawns rock based on regeneration rate
		timeCounter -= Time.deltaTime;

		if (timeCounter <= 0) {
			spawnProjectile (1);
			timeCounter = regenerationTime;
		}
	}

	void spawnProjectile(int number) {
		int spawned = 0;
		for (int i = 0; i < maxSpawn; i++) {
			if (earthProjectiles [i] == null) {
				float angle = i * angleDifference+transform.eulerAngles.z;
				Vector3 spawnPosition = this.transform.position+ new Vector3 (radius * Mathf.Cos (Mathf.Deg2Rad*angle), radius * Mathf.Sin (Mathf.Deg2Rad*angle));
				earthProjectiles [i] = Instantiate (earthProjectilePrefab, spawnPosition, transform.rotation, this.transform);
				if (shooter)
					earthProjectiles [i].GetComponent<ProjectileController> ().shooter = this.shooter;
				if (belongToPlayer)
					earthProjectiles [i].GetComponent<ProjectileController> ().belongsToPlayer();
				NetworkServer.Spawn (earthProjectiles [i]);
				spawned++;
			}
			if (spawned >= number)
				break;
		}
	}

	public void belongsToPlayer() {
		belongToPlayer = true;
	}

	public bool getBelongsToPlayer() {
		return belongToPlayer;
	}

}
