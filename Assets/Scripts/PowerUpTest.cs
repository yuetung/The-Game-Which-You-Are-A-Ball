using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using UnityEngine.Networking;

public class PowerUpTest : MonoBehaviour {

	// A UnityTest behaves like a coroutine in PlayMode
	// and allows you to yield null to skip a frame in EditMode
	[UnityTest]
	public IEnumerator TestSelfDestruct() {
		GameObject gm = (GameObject)Instantiate(Resources.Load("Tests/gamemanager"));
		Vector3 currPos = new Vector3(0.0f,0.0f,0.0f);
		var powerupPrefab = Resources.Load ("Tests/powerup");
		GameObject powerup = (GameObject)Instantiate(powerupPrefab, currPos, new Quaternion());
		var playerPrefab = Resources.Load ("Tests/player");
		GameObject player = (GameObject)Instantiate (playerPrefab, currPos,new Quaternion());
		NetworkServer.Listen (7777);

		yield return new WaitForSeconds(5);

		GameObject[] spawnedPowerUp = GameObject.FindGameObjectsWithTag("PowerUp");
		Assert.AreEqual(spawnedPowerUp.Length,0);
	}

	[UnityTest]
	public IEnumerator TestEnergyPowerUp() {
		GameObject gm = (GameObject)Instantiate(Resources.Load("Tests/gamemanager"));
		var powerupPrefab = Resources.Load ("Tests/powerup");
		GameObject powerup = (GameObject)Instantiate(powerupPrefab);
		powerup.GetComponent<PowerUpPickup> ().energy = 90;
		NetworkServer.Listen (7777);

		yield return new WaitForSeconds(5);

		GameObject[] spawnedPowerUp = GameObject.FindGameObjectsWithTag("PowerUp");
		Assert.AreEqual(spawnedPowerUp[0].GetComponent<PowerUpPickup>().energy,90);
	}

	[UnityTest]
	public IEnumerator TestSizePowerUp() {
		GameObject gm = (GameObject)Instantiate(Resources.Load("Tests/gamemanager"));
		var powerupPrefab = Resources.Load ("Tests/powerup");
		GameObject powerup = (GameObject)Instantiate(powerupPrefab);
		powerup.GetComponent<PowerUpPickup> ().setEnergy (90);
		NetworkServer.Listen (7777);

		yield return new WaitForSeconds(5);

		GameObject[] spawnedPowerUp = GameObject.FindGameObjectsWithTag("PowerUp");

		float scaleFactor = Mathf.Log10 (90)/2;

		Assert.AreEqual(spawnedPowerUp[0].transform.localScale.x,scaleFactor);
	}

	[TearDown]
	public void afterEveryTest(){
		foreach(GameObject i in Object.FindObjectsOfType<GameObject>()) {
			Destroy (i);
		}
		NetworkServer.dontListen = true;
	}
}
