using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;

public class PowerUpSpawnerTest : MonoBehaviour{

	// A UnityTest behaves like a coroutine in PlayMode
	// and allows you to yield null to skip a frame in EditMode
	[UnityTest]
	public IEnumerator TestSpawnPowerUp() {
		// Use the Assert class to test conditions.
		// yield to skip a frame

		//var powerupPrefab = Resources.Load("Tests/powerup");
		//powerupSpawner.Construct ((GameObject)powerupPrefab, 2.0f, 7777);
		var powerupSpawnerPrefab = Resources.Load ("Tests/powerupspawner");
		GameObject powerupSpawner = (GameObject)Instantiate(powerupSpawnerPrefab);
		NetworkServer.Listen (7777);

		yield return new WaitForSeconds(5);

		GameObject[] spawnedPowerUp = GameObject.FindGameObjectsWithTag("PowerUp");
		Assert.AreNotEqual(spawnedPowerUp.Length,0);
	}

	[UnityTest]
	public IEnumerator TestSpawnTime() {
		// Use the Assert class to test conditions.
		// yield to skip a frame

		var powerupSpawnerPrefab = Resources.Load ("Tests/powerupspawner");
		GameObject powerupSpawner = (GameObject)Instantiate(powerupSpawnerPrefab);
		powerupSpawner.GetComponent<PowerUpSpawner> ().secondsBetweenSpawn = 1.0f;
		NetworkServer.Listen (7777);

		yield return new WaitForSeconds(5);

		GameObject[] spawnedPowerUp = GameObject.FindGameObjectsWithTag("PowerUp");
		Assert.AreEqual(spawnedPowerUp.Length,5);
	}

	[UnityTest]
	public IEnumerator TestWithinMinAndMax() {
		// Use the Assert class to test conditions.
		// yield to skip a frame

		var powerupSpawnerPrefab = Resources.Load ("Tests/powerupspawner");
		GameObject powerupSpawner = (GameObject)Instantiate(powerupSpawnerPrefab);
		NetworkServer.Listen (7777);

		yield return new WaitForSeconds(5);

		GameObject[] spawnedPowerUp = GameObject.FindGameObjectsWithTag("PowerUp");
		int energy = spawnedPowerUp [0].GetComponent<PowerUpPickup> ().energy;
		int min = powerupSpawner.GetComponent<PowerUpSpawner> ().minEnergy;
		int max = powerupSpawner.GetComponent<PowerUpSpawner> ().maxEnergy;
		var withinMinMax = (energy >= min) && (energy <= max);
		Assert.That(withinMinMax);
	}

	[UnityTest]
	public IEnumerator TestSpawnWithinWidthAndHeight() {
		// Use the Assert class to test conditions.
		// yield to skip a frame

		var powerupSpawnerPrefab = Resources.Load ("Tests/powerupspawner");
		GameObject powerupSpawner = (GameObject)Instantiate(powerupSpawnerPrefab);
		NetworkServer.Listen (7777);

		yield return new WaitForSeconds(5);

		GameObject[] spawnedPowerUp = GameObject.FindGameObjectsWithTag("PowerUp");
		Vector3 spawnerPos = powerupSpawner.transform.position;

		float width = powerupSpawner.GetComponent<PowerUpSpawner> ().width_x;
		float height = powerupSpawner.GetComponent<PowerUpSpawner> ().height_y;

		var withinWidthAndHeight = (spawnedPowerUp [0].transform.position.x - spawnerPos.x)<=width&&(spawnedPowerUp [0].transform.position.y - spawnerPos.y)<=height;
		Assert.That(withinWidthAndHeight);
	}

	[UnityTest]
	public IEnumerator TestPlayerEnterAndSpawnInside() {
		// Use the Assert class to test conditions.
		// yield to skip a frame
		GameObject gm = (GameObject)Instantiate(Resources.Load("Tests/gamemanager"));

		var powerupSpawnerPrefab = Resources.Load ("Tests/powerupspawner");
		var playerPrefab = Resources.Load ("Tests/player");
		GameObject powerupSpawner = (GameObject)Instantiate(powerupSpawnerPrefab);
		var powerupspawnerScript = powerupSpawner.GetComponent<PowerUpSpawner> ();
		powerupspawnerScript.startSpawning = false;

		Vector3 spawnerPos = powerupSpawner.transform.position;
		//location = new Vector3(transform.position.x+Random.Range(-width_x / 2, width_x / 2), 
		//transform.position.y+Random.Range(-height_y / 2, height_y / 2), 0);
		Vector3 playerStartPos = new Vector3 (spawnerPos.x + Random.Range (-powerupspawnerScript.width_x / 2, powerupspawnerScript.width_x / 2),
			spawnerPos.y + Random.Range (-powerupspawnerScript.height_y / 2, powerupspawnerScript.height_y / 2), 0);

		GameObject player = (GameObject)Instantiate (playerPrefab, playerStartPos,new Quaternion());
		NetworkServer.Listen (7777);

		yield return new WaitForSeconds(5);

		GameObject[] spawnedPowerUp = GameObject.FindGameObjectsWithTag("PowerUp");
		Assert.Greater (spawnedPowerUp.Length, 0);
	}

	[UnityTest]
	public IEnumerator TestPlayerEnterAndSpawnOutside() {
		// Use the Assert class to test conditions.
		// yield to skip a frame
		GameObject gm = (GameObject)Instantiate(Resources.Load("Tests/gamemanager"));

		var powerupSpawnerPrefab = Resources.Load ("Tests/powerupspawner");
		var playerPrefab = Resources.Load ("Tests/player");
		GameObject powerupSpawner = (GameObject)Instantiate(powerupSpawnerPrefab);
		var powerupspawnerScript = powerupSpawner.GetComponent<PowerUpSpawner> ();
		powerupspawnerScript.startSpawning = false;

		Vector3 spawnerPos = powerupSpawner.transform.position;
		//location = new Vector3(transform.position.x+Random.Range(-width_x / 2, width_x / 2), 
		//transform.position.y+Random.Range(-height_y / 2, height_y / 2), 0);
		Vector3 playerStartPos = new Vector3 (spawnerPos.x + Random.Range (powerupspawnerScript.width_x / 2 + 1, 2 * (powerupspawnerScript.width_x + 1)),
			                         spawnerPos.y + Random.Range (powerupspawnerScript.height_y / 2 + 1, 2 * (powerupspawnerScript.height_y + 1)), 0);
		
		GameObject player = (GameObject)Instantiate (playerPrefab, playerStartPos,new Quaternion());
		NetworkServer.Listen (7777);

		yield return new WaitForSeconds(5);

		GameObject[] spawnedPowerUp = GameObject.FindGameObjectsWithTag("PowerUp");
		Assert.AreEqual(spawnedPowerUp.Length,0);

	}

	[TearDown]
	public void afterEveryTest(){
		foreach(GameObject i in Object.FindObjectsOfType<GameObject>()) {
			Destroy (i);
		}
		NetworkServer.dontListen = true;
	}
}
