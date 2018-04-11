using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using UnityEngine.Networking;

public class PlayerInteractionTest : MonoBehaviour {

	/*	@Description: Test if player increases in energy and changes element type based on powerup
	 * 	@Parameters: powerup's energy=[0, 50];powerup's element=[fire, water]
	 */
	[UnityTest]
	public IEnumerator TestPlayerWithPowerUp1() {
		GameObject gameManager = (GameObject)Instantiate(Resources.Load("Tests/gamemanager"));
		var playerPrefab = Resources.Load ("Tests/player");
		GameObject player = (GameObject)Instantiate(playerPrefab);
		gameManager.GetComponent<GUIManager> ().register (player);
		player.GetComponent<PlayerController> ().testMode = true;

		NetworkServer.Listen (7777);

		yield return new WaitForSeconds (2);
		// we now spawn the power up for the player's consumption
		var powerupPrefab = Resources.Load("Tests/powerup");
		GameObject powerup = (GameObject)Instantiate (powerupPrefab);
		powerup.transform.position = new Vector2 (0.0f, 0.0f);
		int setEnergy = 50;
		PlayerController.ElementType setElement = PlayerController.ElementType.Fire;
		powerup.GetComponent<PowerUpPickup> ().energy = setEnergy;
		powerup.GetComponent<PowerUpPickup> ().elementType = setElement;

		yield return new WaitForSeconds (1);
		float lossRate = player.GetComponent<PlayerController> ().energyLossRate;
		yield return new WaitForSeconds(4);
		GameObject newplayer = GameObject.FindGameObjectWithTag("Player");
		int newEnergy = newplayer.GetComponent<PlayerController> ().energy;
		PlayerController.ElementType newElement = newplayer.GetComponent<PlayerController> ().elementType;
		
		Debug.Log (newEnergy);
		Debug.Log (setEnergy-(lossRate*5));
		Debug.Log (newElement);
		Debug.Log (setElement);
		var check = (newEnergy == (setEnergy-(lossRate*5)) && newElement == setElement);
		Assert.That (check);
	}

	[UnityTest]
	public IEnumerator TestPlayerWithPowerUp2() {
		GameObject gameManager = (GameObject)Instantiate(Resources.Load("Tests/gamemanager"));
		var playerPrefab = Resources.Load ("Tests/player");
		GameObject player = (GameObject)Instantiate(playerPrefab);
		gameManager.GetComponent<GUIManager> ().register (player);
		player.GetComponent<PlayerController> ().testMode = true;
		NetworkServer.Listen (7777);

		yield return new WaitForSeconds (2);
		// we now spawn the power up for the player's consumption
		var powerupPrefab = Resources.Load("Tests/powerup");
		GameObject powerup = (GameObject)Instantiate (powerupPrefab);
		powerup.transform.position = new Vector2 (0.0f, 0.0f);
		int setEnergy = 50;
		PlayerController.ElementType setElement = PlayerController.ElementType.Water;
		powerup.GetComponent<PowerUpPickup> ().energy = setEnergy;
		powerup.GetComponent<PowerUpPickup> ().elementType = setElement;

		yield return new WaitForSeconds (1);
		float lossRate = player.GetComponent<PlayerController> ().energyLossRate;
		yield return new WaitForSeconds(4);
		GameObject newplayer = GameObject.FindGameObjectWithTag("Player");
		int newEnergy = newplayer.GetComponent<PlayerController> ().energy;
		PlayerController.ElementType newElement = newplayer.GetComponent<PlayerController> ().elementType;
		var check = (newEnergy == (setEnergy-(lossRate*5)) && newElement == setElement);
		Assert.That (check);
	}

	[UnityTest]
	public IEnumerator TestPlayerWithPowerUp3() {
		GameObject gameManager = (GameObject)Instantiate(Resources.Load("Tests/gamemanager"));
		var playerPrefab = Resources.Load ("Tests/player");
		GameObject player = (GameObject)Instantiate(playerPrefab);
		gameManager.GetComponent<GUIManager> ().register (player);
		player.GetComponent<PlayerController> ().testMode = true;
		NetworkServer.Listen (7777);

		yield return new WaitForSeconds (2);
		// we now spawn the power up for the player's consumption
		var powerupPrefab = Resources.Load("Tests/powerup");
		GameObject powerup = (GameObject)Instantiate (powerupPrefab);
		powerup.transform.position = new Vector2 (0.0f, 0.0f);
		int setEnergy = 0;
		PlayerController.ElementType setElement = PlayerController.ElementType.Fire;
		powerup.GetComponent<PowerUpPickup> ().energy = setEnergy;
		powerup.GetComponent<PowerUpPickup> ().elementType = setElement;

		yield return new WaitForSeconds (1);
		float lossRate = player.GetComponent<PlayerController> ().energyLossRate;
		yield return new WaitForSeconds(4);
		GameObject newplayer = GameObject.FindGameObjectWithTag("Player");
		int newEnergy = newplayer.GetComponent<PlayerController> ().energy;
		PlayerController.ElementType newElement = newplayer.GetComponent<PlayerController> ().elementType;
		var check = (newEnergy == (setEnergy-(lossRate*5)) && newElement == PlayerController.ElementType.Default);
		Assert.That (check);
	}

	[UnityTest]
	public IEnumerator TestPlayerWithPowerUp4() {
		GameObject gameManager = (GameObject)Instantiate(Resources.Load("Tests/gamemanager"));
		var playerPrefab = Resources.Load ("Tests/player");
		GameObject player = (GameObject)Instantiate(playerPrefab);
		gameManager.GetComponent<GUIManager> ().register (player);
		player.GetComponent<PlayerController> ().testMode = true;
		NetworkServer.Listen (7777);

		yield return new WaitForSeconds (2);
		// we now spawn the power up for the player's consumption
		var powerupPrefab = Resources.Load("Tests/powerup");
		GameObject powerup = (GameObject)Instantiate (powerupPrefab);
		powerup.transform.position = new Vector2 (0.0f, 0.0f);
		int setEnergy = 0;
		PlayerController.ElementType setElement = PlayerController.ElementType.Water;
		powerup.GetComponent<PowerUpPickup> ().energy = setEnergy;
		powerup.GetComponent<PowerUpPickup> ().elementType = setElement;

		yield return new WaitForSeconds (1);
		float lossRate = player.GetComponent<PlayerController> ().energyLossRate;
		yield return new WaitForSeconds(4);
		GameObject newplayer = GameObject.FindGameObjectWithTag("Player");
		int newEnergy = newplayer.GetComponent<PlayerController> ().energy;
		PlayerController.ElementType newElement = newplayer.GetComponent<PlayerController> ().elementType;
		var check = (newEnergy == (setEnergy-(lossRate*5)) && newElement == PlayerController.ElementType.Default);
		Assert.That (check);
	}




	/*	@Description: Test if the player's health is reduced after being hit by projectile
	 * 	@Parameters: projectile damage=[0,10, 19]
	 */ 
	[UnityTest]
	public IEnumerator TestPlayerWithProjectileHit1() {
		GameObject gm = (GameObject)Instantiate(Resources.Load("Tests/gamemanager"));
		var playerPrefab = Resources.Load ("Tests/player");
		GameObject player = (GameObject)Instantiate(playerPrefab);
		int startHealth = 20;
		player.GetComponent<PlayerController> ().health = startHealth;
		player.GetComponent<PlayerController> ().testMode = true;
		gm.GetComponent<GUIManager> ().playerController = player.GetComponent<PlayerController> ();
		NetworkServer.Listen (7777);

		yield return new WaitForSeconds (2);
		player.GetComponent<PlayerController> ().health = startHealth;
		ProjectilePatternFactory factory = gm.GetComponent<ProjectilePatternFactory> ();
		string pattern = "basicFireball";
		Vector3 startPosition = new Vector3(5,5,0);
		Vector2 shootDirection = new Vector2 (-1,-1);
		bool belongToPlayer = false;
		factory.projectileFactory = gm.GetComponent<ProjectileFactory> ();
		factory.createProjectilePattern (pattern, startPosition, shootDirection, belongToPlayer,player);
		GameObject projectile = GameObject.FindGameObjectWithTag("Projectile");
		int damage = 0;
		projectile.GetComponent<ProjectileController> ().projectileDamage = damage;
		yield return new WaitForSeconds(5);
		GameObject newplayer = GameObject.FindGameObjectWithTag("Player");
		int currHealth = newplayer.GetComponent<PlayerController> ().health;
		int expectedHealth = startHealth - damage;
		Assert.AreEqual (expectedHealth, currHealth);
	}

	[UnityTest]
	public IEnumerator TestPlayerWithProjectileHit2() {
		GameObject gm = (GameObject)Instantiate(Resources.Load("Tests/gamemanager"));
		var playerPrefab = Resources.Load ("Tests/player");
		GameObject player = (GameObject)Instantiate(playerPrefab);
		int startHealth = 20;

		player.GetComponent<PlayerController> ().testMode = true;
		gm.GetComponent<GUIManager> ().playerController = player.GetComponent<PlayerController> ();
		NetworkServer.Listen (7777);

		yield return new WaitForSeconds (2);
		player.GetComponent<PlayerController> ().health = startHealth;
		ProjectilePatternFactory factory = gm.GetComponent<ProjectilePatternFactory> ();
		string pattern = "basicFireball";
		Vector3 startPosition = new Vector3(5,5,0);
		Vector2 shootDirection = new Vector2 (-1,-1);
		bool belongToPlayer = false;
		factory.projectileFactory = gm.GetComponent<ProjectileFactory> ();
		factory.createProjectilePattern (pattern, startPosition, shootDirection, belongToPlayer,player);
		GameObject projectile = GameObject.FindGameObjectWithTag("Projectile");
		int damage = 10;
		projectile.GetComponent<ProjectileController> ().projectileDamage = damage;
		yield return new WaitForSeconds(5);
		GameObject newplayer = GameObject.FindGameObjectWithTag("Player");
		int currHealth = newplayer.GetComponent<PlayerController> ().health;
		int expectedHealth = startHealth - damage;
		Assert.AreEqual (expectedHealth, currHealth);
	}

	[UnityTest]
	public IEnumerator TestPlayerWithProjectileHit3() {
		GameObject gm = (GameObject)Instantiate(Resources.Load("Tests/gamemanager"));
		var playerPrefab = Resources.Load ("Tests/player");
		GameObject player = (GameObject)Instantiate(playerPrefab);
		int startHealth = 20;

		player.GetComponent<PlayerController> ().testMode = true;
		gm.GetComponent<GUIManager> ().playerController = player.GetComponent<PlayerController> ();
		NetworkServer.Listen (7777);

		yield return new WaitForSeconds (2);
		player.GetComponent<PlayerController> ().health = startHealth;
		ProjectilePatternFactory factory = gm.GetComponent<ProjectilePatternFactory> ();
		string pattern = "basicFireball";
		Vector3 startPosition = new Vector3(5,5,0);
		Vector2 shootDirection = new Vector2 (-1,-1);
		bool belongToPlayer = false;
		factory.projectileFactory = gm.GetComponent<ProjectileFactory> ();
		factory.createProjectilePattern (pattern, startPosition, shootDirection, belongToPlayer,player);
		GameObject projectile = GameObject.FindGameObjectWithTag("Projectile");
		int damage = 19;
		projectile.GetComponent<ProjectileController> ().projectileDamage = damage;
		yield return new WaitForSeconds(5);
		GameObject newplayer = GameObject.FindGameObjectWithTag("Player");
		int currHealth = newplayer.GetComponent<PlayerController> ().health;
		int expectedHealth = startHealth - damage;
		Assert.AreEqual (expectedHealth, currHealth);
	}






//	[UnityTest]
//	public IEnumerator TestPlayerWithProjectileShoot() {
//		GameObject gm = (GameObject)Instantiate(Resources.Load("Tests/gamemanager"));
//		var playerPrefab = Resources.Load ("Tests/player");
//		GameObject player = (GameObject)Instantiate(playerPrefab);
//		player.GetComponent<PlayerController> ().testMode = true;
//		gm.GetComponent<GUIManager> ().playerController = player.GetComponent<PlayerController> ();
//		Vector2 shootDirection = new Vector2 (1,1);
//		NetworkServer.Listen (7777);
//
//		yield return new WaitForSeconds (2);
	//		player.GetComponent<PlayerController>().CmdShoot(shootDirection);
//		yield return new WaitForSeconds(5);
//		GameObject newplayer = GameObject.FindGameObjectWithTag("Player");
//		int currHealth = newplayer.GetComponent<PlayerController> ().health;
//		int expectedHealth = startHealth - damage;
//		Assert.AreEqual (expectedHealth, currHealth);
//	}






	/*	@Description: Test if the player can pass through walls
	 * 	@Projectile: nil
	 */ 
	[UnityTest]
	public IEnumerator TestPlayerWithPlayerHitWall() {
		GameObject gm = (GameObject)Instantiate(Resources.Load("Tests/gamemanager"));

		var wallPrefab = Resources.Load ("Tests/wall");
		GameObject wall1 = (GameObject)Instantiate(wallPrefab);
		wall1.transform.position = new Vector2 (2, -1);
		GameObject wall2 = (GameObject)Instantiate(wallPrefab);
		wall2.transform.position = new Vector2 (2, 0);
		GameObject wall3 = (GameObject)Instantiate(wallPrefab);
		wall3.transform.position = new Vector2 (2, 1);

		yield return new WaitForSeconds (1);

		var playerPrefab = Resources.Load ("Tests/player");
		GameObject player = (GameObject)Instantiate(playerPrefab);
		player.GetComponent<PlayerController> ().testMode = true;
		gm.GetComponent<GUIManager> ().playerController = player.GetComponent<PlayerController> ();
			
		NetworkServer.Listen (7777);
	
		yield return new WaitForSeconds (2);
		Vector2 newPos = new Vector2 (10.0f, 0.0f);
		player.GetComponent<PlayerController> ().moveTarget = newPos;
		player.GetComponent<PlayerController> ().setMovementTarget(newPos);
		float endTime = Time.time + 2.0f;
		while (Time.time < endTime) {
			player.GetComponent<PlayerController> ().move ();
			yield return new WaitForEndOfFrame();
		}

		GameObject newplayer = GameObject.FindGameObjectWithTag("Player");
		Vector2 playerpos = newplayer.transform.position;
		var check = (playerpos.x - 2 < 0.1);
		Assert.That (check);

		}

	[TearDown]
	public void afterEveryTest(){
		foreach(GameObject i in Object.FindObjectsOfType<GameObject>()) {
			Destroy (i);
		}
		NetworkServer.dontListen = true;
	}

}
