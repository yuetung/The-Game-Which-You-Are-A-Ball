using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PowerUpSpawner : NetworkBehaviour {

    public GameObject powerUpPrefab;
    public GameObject players;
    
	void Update () {

        //just some random code to spawn some stuff, time could be better coded than <0.01
		if(Time.time%1 < 0.005)
        {
            Spawn();
        }
	}

    void Spawn()
    {

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
            location = new Vector3(Random.Range(-7, 7), Random.Range(-3, 3), 0);
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

        var powerUp = (GameObject)Instantiate(powerUpPrefab, location, new Quaternion());
        
        NetworkServer.Spawn(powerUp);
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
}
