using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PowerUpSpawner : NetworkBehaviour {

    [Tooltip("Include power ups here (each has equal chance of spawning)")]
    public GameObject[] powerUpPrefabs;
    public int minEnergy = 10;
    public int maxEnergy = 80;
    public float secondsBetweenSpawn = 1.0f;
    public bool startSpawning = true;
    private float height_y = 6.0f;
    private float width_x = 14.0f;
    private float nextSpawnTime = 0f;

    void Start() {
        Vector2 dimension = gameObject.GetComponent<BoxCollider2D>().size;
        height_y = dimension.y;
        width_x = dimension.x;
    }

    void Update() {

        //just some random code to spawn some stuff, time could be better coded than <0.01
        if (Time.time > nextSpawnTime && startSpawning)
        {
            Spawn();
            nextSpawnTime = Time.time + secondsBetweenSpawn;
        }
    }


    void Spawn()
    {
        
        //if(isServer) {
        //randomly generate spawn locations
        Vector3 location;

        int i = 0;//i is only for testing, in case the while loop goes to infinity

        //spawn check if it's near player, try to avoid spawning too close to player
        //INSTEAD OF THIS, WE CAN CHECK IF THE SPAWNED POWERUP WOULD BE IN THE PATH OF THE PLAYER
        //OR WE COULD GIVE POWERUP SPAWNS AN ANIMATION SO THAT PEOPLE WOULD AVOID IT
        bool nearPlayer;
        do
        {
            nearPlayer = false;
            location = new Vector3(Random.Range(-width_x / 2, width_x / 2), Random.Range(-height_y / 2, height_y / 2), 0);
            foreach (GameObject p in GameObject.FindGameObjectsWithTag("Player"))
            {
                if (Around(location, p.transform.localPosition))
                {
                    nearPlayer = true;
                }
            };
            i++;
        }
        while (nearPlayer && i < 100);

        //We need to randomly generate powerups too, currently it's spawning the fire powerup.
        //The strength/level of powerups should also be randomized.

        GameObject powerUpPrefab = powerUpPrefabs[Random.Range(0, powerUpPrefabs.Length)];
        GameObject powerUp = (GameObject)Instantiate(powerUpPrefab, location, new Quaternion());
        powerUp.GetComponent<PowerUpPickup>().setEnergy(Random.Range(minEnergy, maxEnergy));
        NetworkServer.Spawn(powerUp);
        Debug.Log(powerUp.GetComponent<PowerUpPickup>().elementType);

    //  }       
        //Debug.Log(Network.player.ipAddress);
    }

    //checks if v1 is around v2
    bool Around(Vector3 v1, Vector3 v2)
    {
        Vector3 difference = v1 - v2;
        if(difference.magnitude < 3)
        {
            return true;
        }
        return false;
    }

	// Start spawning if player enters the field
	void OnTriggerEnter2D(Collider2D other) {
		if (other.tag == "Player") {
			startSpawn ();
		}
	}

	// Stop spawning if player leaves the field
	void OnTriggerExit2D(Collider2D other) {
		if (other.tag == "Player") {
			stopSpawn ();
		}
	}

	public void startSpawn() {
		startSpawning = true;
	}

	public void stopSpawn() {
		startSpawning = false;
	}
}
