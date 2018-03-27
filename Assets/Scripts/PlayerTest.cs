using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using UnityEngine.Networking;

public class PlayerTest : MonoBehaviour {

	/*	@Description: Test if the player can move to designated target upon command
	 * 	@Parameters: target position=[at exactly where the player currently is, a normal position, near the edge of the screen]
	 */ 
	[UnityTest]
	public IEnumerator TestMoveTarget1() {
		GameObject gm = (GameObject)Instantiate(Resources.Load("Tests/gamemanager"));
		var playerPrefab = Resources.Load ("Tests/player");
		GameObject player = (GameObject)Instantiate(playerPrefab);
		Vector2 newPos = new Vector2 (2.0f, 2.0f);
		NetworkServer.Listen (7777);

		yield return new WaitForSeconds(2);
		player.GetComponent<PlayerController> ().moveTarget = newPos;
		player.GetComponent<PlayerController> ().setMovementTarget(newPos);
		float endTime = Time.time + 2.0f;
		while (Time.time < endTime) {
			player.GetComponent<PlayerController> ().move ();
			yield return new WaitForEndOfFrame();
		}
		GameObject newplayer = GameObject.FindGameObjectWithTag("Player");
		float diff = (newPos - new Vector2(newplayer.transform.position.x, newplayer.transform.position.y)).magnitude;
		Assert.Less(Mathf.Abs(diff), 0.5);
	}

	[UnityTest]
	public IEnumerator TestMoveTarget2() {
		GameObject gm = (GameObject)Instantiate(Resources.Load("Tests/gamemanager"));
		var playerPrefab = Resources.Load ("Tests/player");
		GameObject player = (GameObject)Instantiate(playerPrefab);
		Vector2 newPos = new Vector2 (player.transform.position.x, player.transform.position.y);
		NetworkServer.Listen (7777);

		yield return new WaitForSeconds(2);
		player.GetComponent<PlayerController> ().moveTarget = newPos;
		player.GetComponent<PlayerController> ().setMovementTarget(newPos);
		float endTime = Time.time + 2.0f;
		while (Time.time < endTime) {
			player.GetComponent<PlayerController> ().move ();
			yield return new WaitForEndOfFrame();
		}
		GameObject newplayer = GameObject.FindGameObjectWithTag("Player");
		float diff = (newPos - new Vector2(newplayer.transform.position.x, newplayer.transform.position.y)).magnitude;
		Assert.Less(Mathf.Abs(diff), 0.5);
	}

	[UnityTest]
	public IEnumerator TestMoveTarget3() {
		GameObject gm = (GameObject)Instantiate(Resources.Load("Tests/gamemanager"));
		var playerPrefab = Resources.Load ("Tests/player");
		GameObject player = (GameObject)Instantiate(playerPrefab);
		Vector2 newPos = new Vector2 (50.0f, 50.0f);
		NetworkServer.Listen (7777);

		yield return new WaitForSeconds(2);
		player.GetComponent<PlayerController> ().moveTarget = newPos;
		player.GetComponent<PlayerController> ().setMovementTarget(newPos);
		float endTime = Time.time + 10.0f;
		while (Time.time < endTime) {
			player.GetComponent<PlayerController> ().move ();
			yield return new WaitForEndOfFrame();
		}
		GameObject newplayer = GameObject.FindGameObjectWithTag("Player");
		float diff = (newPos - new Vector2(newplayer.transform.position.x, newplayer.transform.position.y)).magnitude;
		Assert.Less(Mathf.Abs(diff), 0.5);
	}





	/*	@Description: Test if the player is moving at the right speed
	 * 	@Parameters: player speed=[+ve value, -ve value, 0]
	 */ 
	[UnityTest]
	public IEnumerator TestMoveSpeed1() {
		GameObject gm = (GameObject)Instantiate(Resources.Load("Tests/gamemanager"));
		var playerPrefab = Resources.Load ("Tests/player");
		GameObject player = (GameObject)Instantiate(playerPrefab);

		Vector2 oldPos = player.transform.position;
		// set a direction which the player moves towards
		Vector2 targetPos = new Vector2 (100.0f, 100.0f);
		NetworkServer.Listen (7777);

		yield return new WaitForSeconds(2);
		player.GetComponent<PlayerController> ().moveTarget = targetPos;
		player.GetComponent<PlayerController> ().setMovementTarget(targetPos);
		float timeForMove = 2.0f;
		float speed = 0.0f;
		player.GetComponent<PlayerController> ().moveSpeed = speed;
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
	public IEnumerator TestMoveSpeed2() {
		GameObject gm = (GameObject)Instantiate(Resources.Load("Tests/gamemanager"));
		var playerPrefab = Resources.Load ("Tests/player");
		GameObject player = (GameObject)Instantiate(playerPrefab);

		Vector2 oldPos = player.transform.position;
		// set a direction which the player moves towards
		Vector2 targetPos = new Vector2 (100.0f, 100.0f);
		NetworkServer.Listen (7777);

		yield return new WaitForSeconds(2);
		player.GetComponent<PlayerController> ().moveTarget = targetPos;
		player.GetComponent<PlayerController> ().setMovementTarget(targetPos);
		float timeForMove = 2.0f;
		float speed = player.GetComponent<PlayerController> ().moveSpeed;
		player.GetComponent<PlayerController> ().moveSpeed = speed;
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
	public IEnumerator TestMoveSpeed3() {
		GameObject gm = (GameObject)Instantiate(Resources.Load("Tests/gamemanager"));
		var playerPrefab = Resources.Load ("Tests/player");
		GameObject player = (GameObject)Instantiate(playerPrefab);

		Vector2 oldPos = player.transform.position;
		// set a direction which the player moves towards
		Vector2 targetPos = new Vector2 (100.0f, 100.0f);
		NetworkServer.Listen (7777);

		yield return new WaitForSeconds(2);
		player.GetComponent<PlayerController> ().moveTarget = targetPos;
		player.GetComponent<PlayerController> ().setMovementTarget(targetPos);
		float timeForMove = 2.0f;
		float speed = -player.GetComponent<PlayerController> ().moveSpeed;
		player.GetComponent<PlayerController> ().moveSpeed = speed;
		float expecteddistance = player.GetComponent<PlayerController> ().moveSpeed*timeForMove;
		float endTime = Time.time + timeForMove;

		while (Time.time < endTime) {
			player.GetComponent<PlayerController> ().move ();
			yield return new WaitForEndOfFrame();
		}
		GameObject newplayer = GameObject.FindGameObjectWithTag("Player");
		float dist = (new Vector2(newplayer.transform.position.x, newplayer.transform.position.y)-oldPos).magnitude;
		Assert.Less((Mathf.Abs(expecteddistance)-Mathf.Abs(dist)),  0.5);
	}






	/*	@Description: Test if the player is rotating correctly
	 * 	@Parameters: player rotation angle=[+ve value, -ve value, 0]
	 */ 
	[UnityTest]
	public IEnumerator TestRotation1() {
		GameObject gm = (GameObject)Instantiate(Resources.Load("Tests/gamemanager"));
		var playerPrefab = Resources.Load ("Tests/player");
		GameObject player = (GameObject)Instantiate(playerPrefab);

		Vector2 targetPos = new Vector2 (1.00f, 10.0f);
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
	public IEnumerator TestRotation2() {
		GameObject gm = (GameObject)Instantiate(Resources.Load("Tests/gamemanager"));
		var playerPrefab = Resources.Load ("Tests/player");
		GameObject player = (GameObject)Instantiate(playerPrefab);

		Vector2 targetPos = new Vector2 (0.00f,- 10.0f);
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
	public IEnumerator TestRotation3() {
		GameObject gm = (GameObject)Instantiate(Resources.Load("Tests/gamemanager"));
		var playerPrefab = Resources.Load ("Tests/player");
		GameObject player = (GameObject)Instantiate(playerPrefab);

		Vector2 targetPos = new Vector2 (-1.0f, 10.0f);
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







	/*	@Description: Test if the player dies after health is < 0
	 * 	@Parameters: player health = [0,1,-10]
	 */ 
	[UnityTest]
	public IEnumerator TestReduceHealthAndDie1() {
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
	public IEnumerator TestReduceHealthAndDie2() {
		GameObject gm = (GameObject)Instantiate(Resources.Load("Tests/gamemanager"));
		var playerPrefab = Resources.Load ("Tests/player");
		GameObject player = (GameObject)Instantiate(playerPrefab);
		gm.GetComponent<GUIManager> ().playerController = player.GetComponent<PlayerController> ();
		NetworkServer.Listen (7777);

		yield return new WaitForSeconds(5);
		player.GetComponent<PlayerController> ().depleteHealth (99);

		yield return new WaitForSeconds (1);
		GameObject newplayer = GameObject.FindGameObjectWithTag("Player");
		Assert.AreNotEqual(newplayer,null);
	}

	[UnityTest]
	public IEnumerator TestReduceHealthAndDie3() {
		GameObject gm = (GameObject)Instantiate(Resources.Load("Tests/gamemanager"));
		var playerPrefab = Resources.Load ("Tests/player");
		GameObject player = (GameObject)Instantiate(playerPrefab);
		gm.GetComponent<GUIManager> ().playerController = player.GetComponent<PlayerController> ();
		NetworkServer.Listen (7777);

		yield return new WaitForSeconds(5);
		player.GetComponent<PlayerController> ().depleteHealth (101);

		yield return new WaitForSeconds (1);
		GameObject newplayer = GameObject.FindGameObjectWithTag("Player");
		Assert.AreEqual(newplayer,null);
	}






	/*	@Description: Test if energy is lost with every second
	 * 	@Parameters: start energy = [0,+ve value];lossRate = [1.0, 0.0]
	 */ 
	[UnityTest]
	public IEnumerator TestReduceEnergy1() {
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
		int expectedEnergy = Mathf.Max (startEnergy - (int)lossRate * timeElapsed,0);
		int newEnergy = player.GetComponent<PlayerController> ().energy;
		Assert.AreEqual(expectedEnergy,newEnergy);
	}

	[UnityTest]
	public IEnumerator TestReduceEnergy2() {
		GameObject gm = (GameObject)Instantiate(Resources.Load("Tests/gamemanager"));
		var playerPrefab = Resources.Load ("Tests/player");
		GameObject player = (GameObject)Instantiate(playerPrefab);
		gm.GetComponent<GUIManager> ().playerController = player.GetComponent<PlayerController> ();

		int startEnergy = 0;
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
		int expectedEnergy = Mathf.Max (startEnergy - (int)lossRate * timeElapsed,0);
		int newEnergy = player.GetComponent<PlayerController> ().energy;
		Assert.AreEqual(expectedEnergy,newEnergy);
	}

	[UnityTest]
	public IEnumerator TestReduceEnergy3() {
		GameObject gm = (GameObject)Instantiate(Resources.Load("Tests/gamemanager"));
		var playerPrefab = Resources.Load ("Tests/player");
		GameObject player = (GameObject)Instantiate(playerPrefab);
		gm.GetComponent<GUIManager> ().playerController = player.GetComponent<PlayerController> ();

		int startEnergy = 50;
		float lossRate = 0.0f;
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
		int expectedEnergy = Mathf.Max (startEnergy - (int)lossRate * timeElapsed,0);
		int newEnergy = player.GetComponent<PlayerController> ().energy;
		Assert.AreEqual(expectedEnergy,newEnergy);
	}

	[UnityTest]
	public IEnumerator TestReduceEnergy4() {
		GameObject gm = (GameObject)Instantiate(Resources.Load("Tests/gamemanager"));
		var playerPrefab = Resources.Load ("Tests/player");
		GameObject player = (GameObject)Instantiate(playerPrefab);
		gm.GetComponent<GUIManager> ().playerController = player.GetComponent<PlayerController> ();

		int startEnergy = 0;
		float lossRate = 0.0f;
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
		int expectedEnergy = Mathf.Max (startEnergy - (int)lossRate * timeElapsed,0);
		int newEnergy = player.GetComponent<PlayerController> ().energy;
		Assert.AreEqual(expectedEnergy,newEnergy);
	}







	/*	@Description: Test if the level increases after the energy is more than 100%
	 * 	@Parameters: start energy = [50];energy increment=[51,49,50]
	 */ 
	[UnityTest]
	public IEnumerator TestLevelIncrement1() {
		GameObject gm = (GameObject)Instantiate(Resources.Load("Tests/gamemanager"));
		var playerPrefab = Resources.Load ("Tests/player");
		GameObject player = (GameObject)Instantiate(playerPrefab);
		gm.GetComponent<GUIManager> ().playerController = player.GetComponent<PlayerController> ();

		int startEnergy = 50;
		int startLevel = 1;
		int energyincrement = 71;

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
	public IEnumerator TestLevelIncrement2() {
		GameObject gm = (GameObject)Instantiate(Resources.Load("Tests/gamemanager"));
		var playerPrefab = Resources.Load ("Tests/player");
		GameObject player = (GameObject)Instantiate(playerPrefab);
		gm.GetComponent<GUIManager> ().playerController = player.GetComponent<PlayerController> ();

		int startEnergy = 50;
		int startLevel = 1;
		int energyincrement = 65;

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
	public IEnumerator TestLevelIncrement3() {
		GameObject gm = (GameObject)Instantiate(Resources.Load("Tests/gamemanager"));
		var playerPrefab = Resources.Load ("Tests/player");
		GameObject player = (GameObject)Instantiate(playerPrefab);
		gm.GetComponent<GUIManager> ().playerController = player.GetComponent<PlayerController> ();

		int startEnergy = 50;
		int startLevel = 1;
		int energyincrement = 49;

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
		int expectedLevel = 1;
		Assert.AreEqual(expectedLevel,currentLevel);
	}







	/*	@Description: Test if the level is capped at 3 even though energy increment to more than 100 and 
	 * 				  at level 3
	 * 	@Parameters: start energy=[99];energy increment=[50];level=[3]
	 */ 
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







	/*	@Description: Test if the level decreases after the energy goes below 100
	 * 	@Parameters: start energy=[50]; level=[2];energy decrement=[80, 0]
	 */ 
	[UnityTest]
	public IEnumerator TestLevelDecrement1() {
		GameObject gm = (GameObject)Instantiate(Resources.Load("Tests/gamemanager"));
		var playerPrefab = Resources.Load ("Tests/player");
		GameObject player = (GameObject)Instantiate(playerPrefab);
		gm.GetComponent<GUIManager> ().playerController = player.GetComponent<PlayerController> ();

		int startEnergy = 50;
		int startLevel = 2;
		int energydecrement = 80;

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
	public IEnumerator TestLevelDecrement2() {
		GameObject gm = (GameObject)Instantiate(Resources.Load("Tests/gamemanager"));
		var playerPrefab = Resources.Load ("Tests/player");
		GameObject player = (GameObject)Instantiate(playerPrefab);
		gm.GetComponent<GUIManager> ().playerController = player.GetComponent<PlayerController> ();

		int startEnergy = 50;
		int startLevel = 2;
		int energydecrement = 0;

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
		int expectedLevel = 2;
		Assert.AreEqual(expectedLevel,currentLevel);
	}

	/*	@Description: Test if the energy type resets to default when the energy reaches 0 and 
	 * 				  level = 1
	 * 	@Parameters: start energy=[50];level=[1];energy decrement=[80, 0]
	 */ 
	[UnityTest]
	public IEnumerator TestEnergyResetToDefault1() {
		GameObject gm = (GameObject)Instantiate(Resources.Load("Tests/gamemanager"));
		var playerPrefab = Resources.Load ("Tests/player");
		GameObject player = (GameObject)Instantiate(playerPrefab);
		gm.GetComponent<GUIManager> ().playerController = player.GetComponent<PlayerController> ();

		int startEnergy = 50;
		int startLevel = 1;
		int energydecrement = 80;
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
	public IEnumerator TestEnergyResetToDefault2() {
		GameObject gm = (GameObject)Instantiate(Resources.Load("Tests/gamemanager"));
		var playerPrefab = Resources.Load ("Tests/player");
		GameObject player = (GameObject)Instantiate(playerPrefab);
		gm.GetComponent<GUIManager> ().playerController = player.GetComponent<PlayerController> ();

		int startEnergy = 50;
		int startLevel = 1;
		int energydecrement = 0;
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
		PlayerController.ElementType expectedType = startType;
		Assert.AreEqual(expectedType,currentType);
	}






	/*	@Description: Test if the level resets to one when the player collects a powerup with a different
	 * 				  type. For example, if the player is Fire and collects Water powerup, 
	 * 				  it will turn Water
	 * 	@Parameters: start energy=[90];level=[3];energy type=[Fire];powerup collected[Fire, Water]
	 */ 
	[UnityTest]
	public IEnumerator TestLevelResetToOne1() {
		GameObject gm = (GameObject)Instantiate(Resources.Load("Tests/gamemanager"));
		var playerPrefab = Resources.Load ("Tests/player");
		GameObject player = (GameObject)Instantiate(playerPrefab);
		gm.GetComponent<GUIManager> ().playerController = player.GetComponent<PlayerController> ();

		int startEnergy = 90;
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
	public IEnumerator TestLevelResetToOne2() {
		GameObject gm = (GameObject)Instantiate(Resources.Load("Tests/gamemanager"));
		var playerPrefab = Resources.Load ("Tests/player");
		GameObject player = (GameObject)Instantiate(playerPrefab);
		gm.GetComponent<GUIManager> ().playerController = player.GetComponent<PlayerController> ();

		int startEnergy = 90;
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
		player.GetComponent<PlayerController> ().gainPowerUp (PlayerController.ElementType.Fire, energyAmount);
		yield return new WaitForSeconds (5);

		GameObject newplayer = GameObject.FindGameObjectWithTag("Player");
		int currentLevel = newplayer.GetComponent<PlayerController> ().elementLevel;
		int expectedLevel = startLevel;
		Assert.AreEqual(expectedLevel, currentLevel);
	}






	/*	@Description: Test if the energy resets to new powerup when the player collects a 
	 * 				  powerup with a type. For example, if the player is Fire and collects 
	 * 				  Water powerup, it will energy will reset to the value of the new powerup
	 * 	@Parameters: start energy=[90];level=[3];energy type=[Fire];powerup collected[Fire, Water]
	 */ 
	[UnityTest]
	public IEnumerator TestEnergyResetToNewPowerup1() {
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

	[UnityTest]
	public IEnumerator TestEnergyResetToNewPowerup2() {
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
		int tempEnergy = player.GetComponent<PlayerController> ().energy;
		player.GetComponent<PlayerController> ().gainPowerUp (PlayerController.ElementType.Fire, energyAmount);
		GameObject newplayer = GameObject.FindGameObjectWithTag("Player");
		int currentAmount = newplayer.GetComponent<PlayerController> ().energy;
		int expectedAmount = tempEnergy+energyAmount;
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
