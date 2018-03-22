using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using UnityEngine.Networking;

public class PlayerInteractionTest : MonoBehaviour {

	[UnityTest]
	public IEnumerator TestPlayerWithPowerUp() {
		GameObject gm = (GameObject)Instantiate(Resources.Load("Tests/gamemanager"));
		var playerPrefab = Resources.Load ("Tests/player");
		GameObject player = (GameObject)Instantiate(playerPrefab);
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

		yield return new WaitForSeconds(5);
		GameObject newplayer = GameObject.FindGameObjectWithTag("Player");
		int newEnergy = player.GetComponent<PlayerController> ().energy;
		PlayerController.ElementType newElement = player.GetComponent<PlayerController> ().elementType;
		var check = (newEnergy == setEnergy && newElement == setElement);
		Assert.That (check);
	}
		
	[UnityTest]
	public IEnumerator TestPlayerWithProjectileHit() {
		GameObject gm = (GameObject)Instantiate(Resources.Load("Tests/gamemanager"));
		var playerPrefab = Resources.Load ("Tests/player");
		GameObject player = (GameObject)Instantiate(playerPrefab);
		int startHealth = 100;
		player.GetComponent<PlayerController> ().health = startHealth;
		player.GetComponent<PlayerController> ().testMode = true;
		gm.GetComponent<GUIManager> ().playerController = player.GetComponent<PlayerController> ();
		NetworkServer.Listen (7777);

		yield return new WaitForSeconds (2);
		ProjectilePatternFactory factory = gm.GetComponent<ProjectilePatternFactory> ();
		string pattern = "basicFireball";
		Vector3 startPosition = new Vector3(5,5,0);
		Vector2 shootDirection = new Vector2 (-1,-1);
		bool belongToPlayer = false;
		factory.projectileFactory = gm.GetComponent<ProjectileFactory> ();
		factory.createProjectilePattern (pattern, startPosition, shootDirection, belongToPlayer);
		GameObject projectile = GameObject.FindGameObjectWithTag("Projectile");
		int damage = projectile.GetComponent<ProjectileController> ().projectileDamage;
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
