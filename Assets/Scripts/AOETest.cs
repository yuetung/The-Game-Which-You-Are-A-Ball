using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using UnityEngine.Networking;

public class AOETest : MonoBehaviour {

	[UnityTest]
	public IEnumerator TestAoeOnStaticEnemy() {
		GameObject maincanvas = Instantiate (Resources.Load ("Tests/maincanvas")) as GameObject;
		GameObject maincamera = Instantiate (Resources.Load ("Tests/maincamera")) as GameObject;
		GameObject gm= Instantiate (Resources.Load ("Tests/GameManager")) as GameObject;

		var enemy = Resources.Load ("Tests/Enemy_Bat 1");
		GameObject enemy1 = Instantiate (enemy, new Vector2 (0.0f, 10.0f), new Quaternion()) as GameObject;
		//GameObject enemy2 = Instantiate (enemy, new Vector2 (0.0f, 20.0f), new Quaternion()) as GameObject;
		int startHealth = enemy1.GetComponentInChildren<Enemy>().hp;
		NetworkServer.Listen(7777);

		yield return new WaitForSeconds (1);

		ProjectileFactory projectileFactory = gm.GetComponent<ProjectileFactory> ();
		PlayerController.ElementType element = PlayerController.ElementType.Water;
		int level = 2;
		GameObject projectileClone = Instantiate (Resources.Load ("Tests/WaterProjectile2"), new Vector2(0.0f,0.0f), new Quaternion()) as GameObject;
		projectileClone.GetComponent<ProjectileController>().belongsToPlayer ();
		Vector2 shootDirection = new Vector2 (0.0f, 1.0f);
		Vector2 velocity = shootDirection.normalized * projectileClone.GetComponent<ProjectileController> ().projectileSpeed;
		float rotation = Mathf.Atan2 (shootDirection.y, shootDirection.x) * Mathf.Rad2Deg;
		projectileClone.GetComponent<ProjectileController> ().setVelocityAndRotation (velocity, rotation);

		int projectiledamage = projectileClone.GetComponent<ProjectileController> ().projectileDamage;
		int aoedamage = projectileClone.GetComponent<ProjectileController> ().AOEExplosionPrefab.GetComponent<AOEExplosion> ().AOEDamage;
		Debug.Log (projectiledamage);
		Debug.Log (aoedamage);

		yield return new WaitForSeconds(5);
		int totaldamage = projectiledamage + aoedamage;
		int expectedhealth = startHealth - totaldamage;
		GameObject newenemy = GameObject.FindGameObjectWithTag("Enemy");
		int actualhealth = newenemy.GetComponent<Enemy> ().hp;
		Assert.AreEqual (expectedhealth, actualhealth);
	}

	[UnityTest]
	public IEnumerator TestAoeOnMovingEnemy() {
		GameObject maincanvas = Instantiate (Resources.Load ("Tests/maincanvas")) as GameObject;
		GameObject maincamera = Instantiate (Resources.Load ("Tests/maincamera")) as GameObject;
		GameObject gm= Instantiate (Resources.Load ("Tests/GameManager")) as GameObject;

		var enemy = Resources.Load ("Tests/Enemy_Bat 1");
		Vector2 startposition = new Vector2 (0.0f, 0.0f);
		Vector2 endposition = new Vector2 (0.0f, 10.0f);
		GameObject enemy1 = Instantiate (enemy, endposition, new Quaternion()) as GameObject;
		//GameObject enemy2 = Instantiate (enemy, new Vector2 (0.0f, 20.0f), new Quaternion()) as GameObject;
		int startHealth = enemy1.GetComponentInChildren<Enemy>().hp;
		NetworkServer.Listen(7777);

		yield return new WaitForSeconds (1);

		ProjectileFactory projectileFactory = gm.GetComponent<ProjectileFactory> ();
		PlayerController.ElementType element = PlayerController.ElementType.Water;
		int level = 2;
		GameObject projectileClone = Instantiate (Resources.Load ("Tests/WaterProjectile2"), startposition , new Quaternion()) as GameObject;
		projectileClone.GetComponent<ProjectileController>().belongsToPlayer ();
		Vector2 shootDirection = new Vector2 (0.0f, 1.0f);
		Vector2 velocity = shootDirection.normalized * projectileClone.GetComponent<ProjectileController> ().projectileSpeed;
		float rotation = Mathf.Atan2 (shootDirection.y, shootDirection.x) * Mathf.Rad2Deg;

		int projectiledamage = projectileClone.GetComponent<ProjectileController> ().projectileDamage;
		int aoedamage = projectileClone.GetComponent<ProjectileController> ().AOEExplosionPrefab.GetComponent<AOEExplosion> ().AOEDamage;

		float endTime = Time.time +5.0f;
		bool once = true;
		bool once2 = true;

		enemy1.GetComponentInChildren<Enemy> ().moveSpeed = 0.1f;
		while (Time.time < endTime) {
			//if (enemy1.GetComponentInChildren<Enemy>().hp<startHealth && once){
			if (once){
				GameObject waypoint = GameObject.FindGameObjectWithTag ("WayPoint");
				waypoint.transform.position = new Vector2 (0.0f, 20.0f);
				once = false;
			}
			if (endTime-Time.time < 4.0f && once2) {
				projectileClone.GetComponent<ProjectileController> ().setVelocityAndRotation (velocity, rotation);
				once2 = false;
			}
			yield return new WaitForEndOfFrame();
		}

		int totaldamage = projectiledamage + aoedamage;
		int expectedhealth = startHealth - totaldamage;
		GameObject newenemy = GameObject.FindGameObjectWithTag("Enemy");
		int actualhealth = newenemy.GetComponent<Enemy> ().hp;
		Assert.AreEqual (expectedhealth, actualhealth);
	}

	[TearDown]
	public void afterEveryTest(){
		foreach(GameObject i in Object.FindObjectsOfType<GameObject>()) {
			Destroy (i);
		}
		NetworkServer.dontListen = true;
	}
}