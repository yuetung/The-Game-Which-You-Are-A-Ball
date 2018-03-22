using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using UnityEngine.Networking;

public class PlayerTest : MonoBehaviour {

	// A UnityTest behaves like a coroutine in PlayMode
	// and allows you to yield null to skip a frame in EditMode
	[UnityTest]
	public IEnumerator TestMoveTarget() {
		// Use the Assert class to test conditions.
		// yield to skip a frame

		GameObject gm = (GameObject)Instantiate(Resources.Load("Tests/gamemanager"));
		var playerPrefab = Resources.Load ("Tests/player");
		GameObject player = (GameObject)Instantiate(playerPrefab);

		Vector2 newPos = new Vector2 (2.0f, 2.0f);
//		player.GetComponent<PlayerController> ().moveTarget = newPos;
//		player.rigidbody = player.GetComponent<Rigidbody2D> ();

		NetworkServer.Listen (7777);

		yield return new WaitForSeconds(5);
		player.GetComponent<PlayerController> ().moveTarget = newPos;
		player.GetComponent<PlayerController> ().setMovementTarget(newPos);
		float endTime = Time.time + 2.0f;
		while (Time.time < endTime) {
			player.GetComponent<PlayerController> ().move ();
			yield return new WaitForEndOfFrame();
		}
		GameObject newplayer = GameObject.FindGameObjectWithTag("Player");
		float diff = (newPos - new Vector2(newplayer.transform.position.x, newplayer.transform.position.y)).magnitude;
		Assert.Less(Mathf.Abs(diff), 0.1);

	}

	[UnityTest]
	public IEnumerator TestMoveSpeed() {
		GameObject gm = (GameObject)Instantiate(Resources.Load("Tests/gamemanager"));
		var playerPrefab = Resources.Load ("Tests/player");
		GameObject player = (GameObject)Instantiate(playerPrefab);

		Vector2 oldPos = player.transform.position;
		// set a direction which the player moves towards
		Vector2 targetPos = new Vector2 (100.0f, 100.0f);
		NetworkServer.Listen (7777);

		yield return new WaitForSeconds(5);
		player.GetComponent<PlayerController> ().moveTarget = targetPos;
		player.GetComponent<PlayerController> ().setMovementTarget(targetPos);
		float timeForMove = 2.0f;
		float expecteddistance = player.GetComponent<PlayerController> ().moveSpeed*timeForMove;
		float endTime = Time.time + timeForMove;

		while (Time.time < endTime) {
			player.GetComponent<PlayerController> ().move ();
			yield return new WaitForEndOfFrame();
		}
		GameObject newplayer = GameObject.FindGameObjectWithTag("Player");
		float dist = (new Vector2(newplayer.transform.position.x, newplayer.transform.position.y)-oldPos).magnitude;
		Assert.Less(Mathf.Abs(expecteddistance-dist),  0.5);
	}

	[UnityTest]
	public IEnumerator TestRotation() {
		GameObject gm = (GameObject)Instantiate(Resources.Load("Tests/gamemanager"));
		var playerPrefab = Resources.Load ("Tests/player");
		GameObject player = (GameObject)Instantiate(playerPrefab);

		Vector2 targetPos = new Vector2 (0.00f, 100.0f);
		Vector2 expectedDir = targetPos - new Vector2(player.transform.position.x,player.transform.position.y);
		NetworkServer.Listen (7777);

		yield return new WaitForSeconds(5);
		player.GetComponent<PlayerController> ().moveTarget = targetPos;
		player.GetComponent<PlayerController> ().setMovementTarget(targetPos);
		float timeForMove = 2.0f;
		float endTime = Time.time + timeForMove;
		while (Time.time < endTime) {
			player.GetComponent<PlayerController> ().move ();
			yield return new WaitForEndOfFrame();
		}
		GameObject newplayer = GameObject.FindGameObjectWithTag("Player");
		Vector2 newplayerpos = new Vector2 (player.transform.rotation.x, player.transform.rotation.y);
		Assert.AreEqual(expectedDir, targetPos-newplayerpos);
	}

	[UnityTest]
	public IEnumerator TestReduceHealthAndDie() {
		GameObject gm = (GameObject)Instantiate(Resources.Load("Tests/gamemanager"));
		var playerPrefab = Resources.Load ("Tests/player");
		GameObject player = (GameObject)Instantiate(playerPrefab);
		gm.GetComponent<GUIManager> ().playerController = player.GetComponent<PlayerController> ();
		NetworkServer.Listen (7777);

		yield return new WaitForSeconds(5);
		player.GetComponent<PlayerController> ().depleteHealth (100);

		yield return new WaitForSeconds (1);
		GameObject newplayer = GameObject.FindGameObjectWithTag("Player");
		Assert.AreEqual(newplayer,null);
	}

	[UnityTest]
	public IEnumerator TestReduceEnergy() {
		GameObject gm = (GameObject)Instantiate(Resources.Load("Tests/gamemanager"));
		var playerPrefab = Resources.Load ("Tests/player");
		GameObject player = (GameObject)Instantiate(playerPrefab);
		gm.GetComponent<GUIManager> ().playerController = player.GetComponent<PlayerController> ();

		int startEnergy = 50;
		float lossRate = 1.0f;
		int timeElapsed = 5;

		player.GetComponent<PlayerController> ().testMode = true;

		NetworkServer.Listen (7777);

		yield return new WaitForSeconds(2);
		player.GetComponent<PlayerController> ().energy = startEnergy;
		player.GetComponent<PlayerController> ().elementType = PlayerController.ElementType.Fire;
		player.GetComponent<PlayerController> ().elementLevel = 1;
		player.GetComponent<PlayerController> ().energyLossRate = lossRate;
		yield return new WaitForSeconds (timeElapsed);

		GameObject newplayer = GameObject.FindGameObjectWithTag("Player");
		int expectedEnergy = startEnergy-(int)lossRate * timeElapsed;
		int newEnergy = player.GetComponent<PlayerController> ().energy;
		Assert.AreEqual(expectedEnergy,newEnergy);
	}

	[UnityTest]
	public IEnumerator TestLevelIncrement() {
		GameObject gm = (GameObject)Instantiate(Resources.Load("Tests/gamemanager"));
		var playerPrefab = Resources.Load ("Tests/player");
		GameObject player = (GameObject)Instantiate(playerPrefab);
		gm.GetComponent<GUIManager> ().playerController = player.GetComponent<PlayerController> ();

		int startEnergy = 99;
		int startLevel = 1;
		int energyincrement = 50;

		player.GetComponent<PlayerController> ().testMode = true;

		NetworkServer.Listen (7777);

		yield return new WaitForSeconds(2);
		player.GetComponent<PlayerController> ().energy = startEnergy;
		player.GetComponent<PlayerController> ().elementType = PlayerController.ElementType.Fire;
		player.GetComponent<PlayerController> ().elementLevel = startLevel;
		player.GetComponent<PlayerController> ().gainEnergy (energyincrement);
		yield return new WaitForSeconds (5);

		GameObject newplayer = GameObject.FindGameObjectWithTag("Player");
		int currentLevel = newplayer.GetComponent<PlayerController> ().elementLevel;
		int expectedLevel = 2;
		Assert.AreEqual(expectedLevel,currentLevel);
	}

	[UnityTest]
	public IEnumerator TestLevelCappedAtThree() {
		GameObject gm = (GameObject)Instantiate(Resources.Load("Tests/gamemanager"));
		var playerPrefab = Resources.Load ("Tests/player");
		GameObject player = (GameObject)Instantiate(playerPrefab);
		gm.GetComponent<GUIManager> ().playerController = player.GetComponent<PlayerController> ();

		int startEnergy = 99;
		int startLevel = 3;
		int energyincrement = 50;

		player.GetComponent<PlayerController> ().testMode = true;

		NetworkServer.Listen (7777);

		yield return new WaitForSeconds(2);
		player.GetComponent<PlayerController> ().energy = startEnergy;
		player.GetComponent<PlayerController> ().elementType = PlayerController.ElementType.Fire;
		player.GetComponent<PlayerController> ().elementLevel = startLevel;
		player.GetComponent<PlayerController> ().gainEnergy (energyincrement);
		yield return new WaitForSeconds (5);

		GameObject newplayer = GameObject.FindGameObjectWithTag("Player");
		int currentLevel = newplayer.GetComponent<PlayerController> ().elementLevel;
		int expectedLevel = 3;
		Assert.AreEqual(expectedLevel,currentLevel);
	}

	[UnityTest]
	public IEnumerator TestLevelDecrement() {
		GameObject gm = (GameObject)Instantiate(Resources.Load("Tests/gamemanager"));
		var playerPrefab = Resources.Load ("Tests/player");
		GameObject player = (GameObject)Instantiate(playerPrefab);
		gm.GetComponent<GUIManager> ().playerController = player.GetComponent<PlayerController> ();

		int startEnergy = 10;
		int startLevel = 2;
		int energydecrement = 50;

		player.GetComponent<PlayerController> ().testMode = true;

		NetworkServer.Listen (7777);

		yield return new WaitForSeconds(5);
		player.GetComponent<PlayerController> ().energy = startEnergy;
		player.GetComponent<PlayerController> ().elementType = PlayerController.ElementType.Fire;
		player.GetComponent<PlayerController> ().elementLevel = startLevel;
		player.GetComponent<PlayerController> ().depletesEnergy (energydecrement);
		yield return new WaitForSeconds (5);

		GameObject newplayer = GameObject.FindGameObjectWithTag("Player");
		int currentLevel = newplayer.GetComponent<PlayerController> ().elementLevel;
		int expectedLevel = 1;
		Assert.AreEqual(expectedLevel,currentLevel);
	}

	[UnityTest]
	public IEnumerator TestEnergyResetToDefault() {
		GameObject gm = (GameObject)Instantiate(Resources.Load("Tests/gamemanager"));
		var playerPrefab = Resources.Load ("Tests/player");
		GameObject player = (GameObject)Instantiate(playerPrefab);
		gm.GetComponent<GUIManager> ().playerController = player.GetComponent<PlayerController> ();

		int startEnergy = 10;
		int startLevel = 1;
		int energydecrement = 50;
		PlayerController.ElementType startType = PlayerController.ElementType.Fire;

		player.GetComponent<PlayerController> ().testMode = true;

		NetworkServer.Listen (7777);

		yield return new WaitForSeconds(5);
		player.GetComponent<PlayerController> ().energy = startEnergy;
		player.GetComponent<PlayerController> ().elementType = startType;
		player.GetComponent<PlayerController> ().elementLevel = startLevel;
		yield return new WaitForSeconds (5);
		player.GetComponent<PlayerController> ().depletesEnergy (energydecrement);
		yield return new WaitForSeconds (5);

		GameObject newplayer = GameObject.FindGameObjectWithTag("Player");
		PlayerController.ElementType currentType = newplayer.GetComponent<PlayerController> ().elementType;
		PlayerController.ElementType expectedType = PlayerController.ElementType.Default;
		Assert.AreEqual(expectedType,currentType);
	}

	[UnityTest]
	public IEnumerator TestLevelResetToOne() {
		GameObject gm = (GameObject)Instantiate(Resources.Load("Tests/gamemanager"));
		var playerPrefab = Resources.Load ("Tests/player");
		GameObject player = (GameObject)Instantiate(playerPrefab);
		gm.GetComponent<GUIManager> ().playerController = player.GetComponent<PlayerController> ();

		int startEnergy = 50;
		int startLevel = 3;
		PlayerController.ElementType startType = PlayerController.ElementType.Fire;
		int energyAmount = 30;

		player.GetComponent<PlayerController> ().testMode = true;

		NetworkServer.Listen (7777);

		yield return new WaitForSeconds(5);
		player.GetComponent<PlayerController> ().energy = startEnergy;
		player.GetComponent<PlayerController> ().elementType = startType;
		player.GetComponent<PlayerController> ().elementLevel = startLevel;
		yield return new WaitForSeconds (5);
		player.GetComponent<PlayerController> ().gainPowerUp (PlayerController.ElementType.Water, energyAmount);
		yield return new WaitForSeconds (5);

		GameObject newplayer = GameObject.FindGameObjectWithTag("Player");
		int currentLevel = newplayer.GetComponent<PlayerController> ().elementLevel;
		int expectedLevel = 1;
		Assert.AreEqual(expectedLevel, currentLevel);
	}

	[UnityTest]
	public IEnumerator TestEnergyResetToNewPowerup() {
		GameObject gm = (GameObject)Instantiate(Resources.Load("Tests/gamemanager"));
		var playerPrefab = Resources.Load ("Tests/player");
		GameObject player = (GameObject)Instantiate(playerPrefab);
		gm.GetComponent<GUIManager> ().playerController = player.GetComponent<PlayerController> ();

		int startEnergy = 50;
		int startLevel = 3;
		PlayerController.ElementType startType = PlayerController.ElementType.Fire;
		int energyAmount = 30;

		player.GetComponent<PlayerController> ().testMode = true;

		NetworkServer.Listen (7777);

		yield return new WaitForSeconds(5);
		player.GetComponent<PlayerController> ().energy = startEnergy;
		player.GetComponent<PlayerController> ().elementType = startType;
		player.GetComponent<PlayerController> ().elementLevel = startLevel;
		yield return new WaitForSeconds (5);
		player.GetComponent<PlayerController> ().gainPowerUp (PlayerController.ElementType.Water, energyAmount);
		GameObject newplayer = GameObject.FindGameObjectWithTag("Player");
		int currentAmount = newplayer.GetComponent<PlayerController> ().energy;
		int expectedAmount = energyAmount;
		Assert.AreEqual(expectedAmount, currentAmount);
	}

	[TearDown]
	public void afterEveryTest(){
		foreach(GameObject i in Object.FindObjectsOfType<GameObject>()) {
			Destroy (i);
		}
		NetworkServer.dontListen = true;
	}
		
		
}
