using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterRoomSpawner : MonoBehaviour {

	[Tooltip("Include enemies here (each has equal chance of spawning)")]
	public GameObject[] monsterPrefabs;
	public GameObject[] steelWalls;
	[HideInInspector]
	public float height_y = 6.0f;
	[HideInInspector]
	public float width_x = 14.0f;
	[Tooltip ("how many waves of monster to spawn")]
	public int maxWave = 1;
	private int waveCount = 0;
	public int numberOfEnemies = 3;
	public int monsterCountLeft = 0;

	void Start() {
		Vector2 dimension = gameObject.GetComponent<BoxCollider2D>().size;
		height_y = dimension.y;
		width_x = dimension.x;
		monsterCountLeft = numberOfEnemies;
	}

	void Update() {
		if (monsterCountLeft <= 0) {
			for (int i = 0; i < steelWalls.Length; i++) {
				steelWalls [i].GetComponent<BreakableWall> ().destroyNow ();
			}
			Destroy (this.gameObject);
		}
	}

	public void Spawn() {
		for (int i = 0; i < steelWalls.Length; i++) {
			steelWalls [i].SetActive (true);
		}
		if (waveCount >= maxWave)
			return;
		waveCount++;
		for (int i = 0; i < numberOfEnemies; i++) {
			SpawnOne ();
		}
	}

	void SpawnOne()
	{
		Vector3 location;
		int i = 0;//i is only for testing, in case the while loop goes to infinity

		//spawn check if it's near player, try to avoid spawning too close to player
		//INSTEAD OF THIS, WE CAN CHECK IF THE SPAWNED POWERUP WOULD BE IN THE PATH OF THE PLAYER
		//OR WE COULD GIVE POWERUP SPAWNS AN ANIMATION SO THAT PEOPLE WOULD AVOID IT
		bool nearPlayer;
		do
		{
			nearPlayer = false;
			location = new Vector3(transform.position.x+Random.Range(-width_x / 2, width_x / 2), transform.position.y+Random.Range(-height_y / 2, height_y / 2), 0);
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

		GameObject monsterPrefab = monsterPrefabs[Random.Range(0, monsterPrefabs.Length)];
		GameObject monster = Instantiate(monsterPrefab, location, new Quaternion()) as GameObject;
		monster.GetComponentInChildren<Enemy> ().setSpawnerParent (gameObject);

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

	public void reduceMonsterCount(){
		monsterCountLeft--;
	}
		
}
