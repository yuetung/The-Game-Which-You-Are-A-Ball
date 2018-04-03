using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class EarthProjectileSpawner : NetworkBehaviour {

	public float regenerationTime = 50.0f;
	public float rotationSpeed = 1.0f;
	private float radius = 1.0f;
	public float minRadius = 1.0f;
	public float maxRadius = 1.5f;
	public GameObject earthProjectilePrefab;
	public int maxSpawn = 3;
	public GameObject[] earthProjectiles;
	public bool belongToPlayer= false;
	private float angleDifference;
	public float timeCounter = 0.0f;
	public float expansionTime = 0.3f;
	private bool expanding = false;
	private bool contracting = false;
	[SyncVar]
	public GameObject shooter;

	// Use this for initialization
	void Start () {
        Debug.Log("Start");
		if (earthProjectiles.Length != maxSpawn) {
			earthProjectiles = new GameObject[maxSpawn];
			angleDifference = 360 / maxSpawn;
			timeCounter = regenerationTime;
			radius = minRadius;
            Debug.Log("Start if");
		}
		transform.parent = null;
    }

    // Update is called once per frame
    void Update()
    {
        //follow parent's transform
        if (shooter)
            transform.position = shooter.transform.position;

        // Rotate around self
        transform.Rotate(Vector3.forward * Time.deltaTime * rotationSpeed);

        // expand if expanding
        if (expanding)
        {
            expand();
        }

        if (contracting)
        {
            contract();
        }

        // Spawns rock based on regeneration rate
        timeCounter -= Time.deltaTime;

        if (timeCounter <= 0)
        {
	        CmdSpawnProjectile(1);
            timeCounter = regenerationTime;
        }
    }

    [Command]
	public void CmdSpawnProjectile(int number) {
        Debug.Log("spawn?");
		if (earthProjectiles.Length != maxSpawn) {
			// not started properlly, simulate Start() again
			earthProjectiles = new GameObject[maxSpawn];
			angleDifference = 360 / maxSpawn;
			timeCounter = regenerationTime;
			radius = minRadius;
		}
		if (number <= 0)
			return;
		int spawned = 0;
		Debug.Log ("earthProjectiles length = " + earthProjectiles.Length + " max spawn = " + maxSpawn);
		for (int i = 0; i < maxSpawn; i++) {
			Debug.Log (i + " Max spawn = " + maxSpawn);
			if (earthProjectiles [i] == null) {
				float angle = i * angleDifference + transform.eulerAngles.z;
				Vector3 spawnPosition = this.transform.position+ new Vector3 (radius * Mathf.Cos (Mathf.Deg2Rad*angle), radius * Mathf.Sin (Mathf.Deg2Rad*angle));
				earthProjectiles [i] = Instantiate (earthProjectilePrefab, spawnPosition, transform.rotation, this.transform);
				if (shooter)
					earthProjectiles [i].GetComponent<ProjectileController> ().shooter = this.shooter;
				if (belongToPlayer)
					earthProjectiles [i].GetComponent<ProjectileController> ().belongsToPlayer();
                earthProjectiles[i].GetComponent<SetParent>().nId = netId;
				earthProjectiles [i].transform.SetParent (gameObject.transform);
				if (shooter) {
					NetworkServer.SpawnWithClientAuthority (earthProjectiles [i], shooter);
				}
				else NetworkServer.Spawn(earthProjectiles [i]);
				spawned++;
			}
			if (spawned >= number)
				break;
		}
	}

	private void expand(){
		if (radius >= maxRadius) {
			radius = maxRadius;
			expanding = false;
			contracting = true;
			return;
		}
		else {
			for (int i = 0; i < maxSpawn; i++) {
				if (earthProjectiles [i] != null) {
					Vector3 moveVector = Vector3.Normalize (earthProjectiles [i].transform.position - this.transform.position) * (maxRadius - minRadius) / expansionTime * Time.deltaTime;
					earthProjectiles [i].transform.position = earthProjectiles [i].transform.position + moveVector;
					radius = Vector3.Magnitude (earthProjectiles [i].transform.position - this.transform.position);
				}
			}
		}
	}

	private void contract(){
		if (radius <= minRadius) {
			radius = minRadius;
			contracting = false;
			return;
		}
		else {
			for (int i = 0; i < maxSpawn; i++) {
				if (earthProjectiles [i] != null) {
					Vector3 moveVector = Vector3.Normalize (earthProjectiles [i].transform.position - this.transform.position) * (maxRadius - minRadius) / expansionTime * Time.deltaTime;
					earthProjectiles [i].transform.position = earthProjectiles [i].transform.position - moveVector;
					radius = Vector3.Magnitude (earthProjectiles [i].transform.position - this.transform.position);
				}
			}
		}
	}

	public void startExpand() {
		expanding = true;
		contracting = false;
	}

	public void belongsToPlayer() {
		belongToPlayer = true;
	}

	public bool getBelongsToPlayer() {
		return belongToPlayer;
	}

	public int getNumRock(){
		int numRock = 0;
		for (int i=0; i<maxSpawn; i++) {
			if (earthProjectiles [i] != null) {
				numRock++;
			}
		}
		return numRock;
	}

}
