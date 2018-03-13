using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

public class PowerUpSpawnerTest {

	// A UnityTest behaves like a coroutine in PlayMode
	// and allows you to yield null to skip a frame in EditMode
	[UnityTest]
	public IEnumerator PowerUpSpawnerTestWithEnumeratorPasses() {
		// Use the Assert class to test conditions.
		// yield to skip a frame
		//var powerupPreFab = Resources.Load("Prefab/");
		var powerupPrefab = Resources.Load("Tests/powerup");
		var powerupSpawner = new GameObject ().AddComponent<PowerUpSpawner> ();

		powerupSpawner.Construct ((GameObject)powerupPrefab);

		yield return new WaitForSeconds(5);

		GameObject[] spawnedPowerUp = GameObject.FindGameObjectsWithTag("PowerUp");
		//Debug.Log ("spawnedPowerup:" + spawnedPowerup);
		//var prefabOfTheSpawnedPowerup = PrefabUtility.GetPrefabParent(spawnedPowerup);
		//Debug.Log ("prefab:" + prefabOfTheSpawnedPowerup);

		Assert.AreEqual (spawnedPowerUp.Length, 3);
	}
}
