

using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using UnityEngine.Networking;

public class EnemyBat_Test: MonoBehaviour {
	
	/*	@Description: Test enemy's health will deplete upon being hit by projectile 
	 * 	@Parameters: projectile damage = [0, 5, 9]
	 */
	[UnityTest]
	public IEnumerator Enemy_BatHealthOnImpact1() {
		NetworkServer.Listen(7777);
		Instantiate (Resources.Load ("Tests/maincanvas"));
		Instantiate (Resources.Load ("Tests/maincamera"));
		Instantiate (Resources.Load ("Tests/gamemanager"));
		GameObject enemyBat = Instantiate (Resources.Load ("Tests/Enemy_Bat 1")) as GameObject;
		GameObject projectile = Instantiate (Resources.Load ("Tests/FireballProjectile1")) as GameObject;
		enemyBat.transform.position = new Vector2 (0, 0);
		projectile.transform.position = new Vector2 (5, 0);
		ProjectileController pc = projectile.GetComponent<ProjectileController> ();
		pc.setVelocityAndRotation (new Vector2 (-4, 0), 0);

		int startHp = enemyBat.GetComponentInChildren<Enemy> ().hp;
		int damage = 0;
		pc.projectileDamage = damage;

		yield return new WaitForSeconds(5);

		GameObject[] spawnedEnemyBatList = GameObject.FindGameObjectsWithTag("Enemy");
		GameObject  spawnedEnemyBat = spawnedEnemyBatList[0];
		Enemy enemyScript=spawnedEnemyBat.GetComponent<Enemy>();
		int expectedhealth = startHp - damage;
		Assert.AreEqual (expectedhealth, enemyScript.hp);
	}

	[UnityTest]
	public IEnumerator Enemy_BatHealthOnImpact2() {
		NetworkServer.Listen(7777);
		Instantiate (Resources.Load ("Tests/maincanvas"));
		Instantiate (Resources.Load ("Tests/maincamera"));
		Instantiate (Resources.Load ("Tests/gamemanager"));
		GameObject enemyBat = Instantiate (Resources.Load ("Tests/Enemy_Bat 1")) as GameObject;
		GameObject projectile = Instantiate (Resources.Load ("Tests/FireballProjectile1")) as GameObject;
		enemyBat.transform.position = new Vector2 (0, 0);
		projectile.transform.position = new Vector2 (5, 0);
		ProjectileController pc = projectile.GetComponent<ProjectileController> ();
		pc.setVelocityAndRotation (new Vector2 (-4, 0), 0);

		int startHp = enemyBat.GetComponentInChildren<Enemy> ().hp;
		int damage = 5;
		pc.projectileDamage = damage;

		yield return new WaitForSeconds(5);

		GameObject[] spawnedEnemyBatList = GameObject.FindGameObjectsWithTag("Enemy");
		GameObject  spawnedEnemyBat = spawnedEnemyBatList[0];
		Enemy enemyScript=spawnedEnemyBat.GetComponent<Enemy>();
		int expectedhealth = startHp - damage;
		Assert.AreEqual (expectedhealth, enemyScript.hp);
	}

	[UnityTest]
	public IEnumerator Enemy_BatHealthOnImpact3() {
		NetworkServer.Listen(7777);
		Instantiate (Resources.Load ("Tests/maincanvas"));
		Instantiate (Resources.Load ("Tests/maincamera"));
		Instantiate (Resources.Load ("Tests/gamemanager"));
		GameObject enemyBat = Instantiate (Resources.Load ("Tests/Enemy_Bat 1")) as GameObject;
		GameObject projectile = Instantiate (Resources.Load ("Tests/FireballProjectile1")) as GameObject;
		enemyBat.transform.position = new Vector2 (0, 0);
		projectile.transform.position = new Vector2 (5, 0);
		ProjectileController pc = projectile.GetComponent<ProjectileController> ();
		pc.setVelocityAndRotation (new Vector2 (-4, 0), 0);

		int startHp = enemyBat.GetComponentInChildren<Enemy> ().hp;
		int damage = startHp-1;
		pc.projectileDamage = damage;

		yield return new WaitForSeconds(5);

		GameObject[] spawnedEnemyBatList = GameObject.FindGameObjectsWithTag("Enemy");
		GameObject  spawnedEnemyBat = spawnedEnemyBatList[0];
		Enemy enemyScript=spawnedEnemyBat.GetComponent<Enemy>();
		int expectedhealth = startHp - damage;
		Assert.AreEqual (expectedhealth, enemyScript.hp);
	}




	/* 	@Description: Test if enemy is destroyed after being hit with projectil
	 * 	@Parameters: projectile damage = [equal to enemy's health, just below enemy's health, just above enemy's health]
	 */
	[UnityTest]
	public IEnumerator Enemy_BatDestroyOnImpact1() {
		NetworkServer.Listen(7777);
		Instantiate (Resources.Load ("Tests/maincanvas"));
		Instantiate (Resources.Load ("Tests/maincamera"));
		Instantiate (Resources.Load ("Tests/gamemanager"));
		GameObject enemyBat = Instantiate (Resources.Load ("Tests/Enemy_Bat 1")) as GameObject;
		enemyBat.transform.position = new Vector2 (0, 0);
		GameObject demoprojectile = Instantiate (Resources.Load ("Tests/FireballProjectile1")) as GameObject;
		int starthealth = enemyBat.GetComponentInChildren<Enemy> ().hp;
		int damage = demoprojectile.GetComponent<ProjectileController> ().getProjectileDamage();
		int count = starthealth / damage;
		Debug.Log (starthealth);
		Debug.Log (damage);
		Debug.Log (count);


		for( int i=0; i<count;i++)
		{
			GameObject projectile = Instantiate (Resources.Load ("Tests/FireballProjectile1")) as GameObject;
			projectile.transform.position = new Vector2 (5, 0);
			ProjectileController pc = projectile.GetComponent<ProjectileController> ();
			pc.setVelocityAndRotation (new Vector2 (-4, 0), 0);
			//projectile [i] = projectile;
		}
		yield return new WaitForSeconds(5);
		Assert.AreEqual (GameObject.FindGameObjectWithTag("Enemy"), null);
	}

	[UnityTest]
	public IEnumerator Enemy_BatDestroyOnImpact2() {
		NetworkServer.Listen(7777);
		Instantiate (Resources.Load ("Tests/maincanvas"));
		Instantiate (Resources.Load ("Tests/maincamera"));
		Instantiate (Resources.Load ("Tests/gamemanager"));
		GameObject enemyBat = Instantiate (Resources.Load ("Tests/Enemy_Bat 1")) as GameObject;
		enemyBat.transform.position = new Vector2 (0, 0);
		GameObject demoprojectile = Instantiate (Resources.Load ("Tests/FireballProjectile1")) as GameObject;
		int starthealth = enemyBat.GetComponentInChildren<Enemy> ().hp;
		int damage = demoprojectile.GetComponent<ProjectileController> ().getProjectileDamage();
		int count = starthealth / damage;


		for( int i=0; i<count+1;i++)
		{
			GameObject projectile = Instantiate (Resources.Load ("Tests/FireballProjectile1")) as GameObject;
			projectile.transform.position = new Vector2 (5, 0);
			ProjectileController pc = projectile.GetComponent<ProjectileController> ();
			pc.setVelocityAndRotation (new Vector2 (-4, 0), 0);
			//projectile [i] = projectile;
		}
		yield return new WaitForSeconds(5);
		Assert.AreEqual (GameObject.FindGameObjectWithTag("Enemy"), null);
	}

	[UnityTest]
	public IEnumerator Enemy_BatDestroyOnImpact3() {
		NetworkServer.Listen(7777);
		Instantiate (Resources.Load ("Tests/maincanvas"));
		Instantiate (Resources.Load ("Tests/maincamera"));
		Instantiate (Resources.Load ("Tests/gamemanager"));
		GameObject enemyBat = Instantiate (Resources.Load ("Tests/Enemy_Bat 1")) as GameObject;
		enemyBat.transform.position = new Vector2 (0, 0);
		GameObject demoprojectile = Instantiate (Resources.Load ("Tests/FireballProjectile1")) as GameObject;
		int starthealth = enemyBat.GetComponentInChildren<Enemy> ().hp;
		int damage = demoprojectile.GetComponent<ProjectileController> ().getProjectileDamage();
		int count = starthealth / damage;


		for( int i=0; i<count-1;i++)
		{
			GameObject projectile = Instantiate (Resources.Load ("Tests/FireballProjectile1")) as GameObject;
			projectile.transform.position = new Vector2 (5, 0);
			ProjectileController pc = projectile.GetComponent<ProjectileController> ();
			pc.setVelocityAndRotation (new Vector2 (-4, 0), 0);
			//projectile [i] = projectile;
		}
		yield return new WaitForSeconds(5);
		Assert.AreEqual ((GameObject.FindGameObjectsWithTag("Enemy")).Length,1);
	}






	/*	@Description: Test if enemy bat moves to the right position
	 * 	@Parameters: distance between new and current position:[1.0, 10.0]
	 */ 
	[UnityTest]
	public IEnumerator Enemy_BatWayPoint1() {
		NetworkServer.Listen(7777);
		Instantiate (Resources.Load ("Tests/maincanvas"));
		Instantiate (Resources.Load ("Tests/maincamera"));
		Instantiate (Resources.Load ("Tests/gamemanager"));
		GameObject enemyBat = Instantiate (Resources.Load ("Tests/Enemy_Bat 1")) as GameObject;
		enemyBat.transform.position = new Vector2 (0, 0);
		foreach (var gameObject in GameObject.FindGameObjectsWithTag("WayPoint")) {
			gameObject.transform.position = new Vector2 (11, 11);
		}

		yield return new WaitForSeconds(10);

		Vector3 vec = GameObject.FindGameObjectWithTag ("Enemy").transform.position;
		Vector3 vec_round = new Vector3 (Mathf.Round (vec.x), Mathf.Round (vec.y),Mathf.Round (vec.z)); //rounds postion to nearest integer

		Assert.AreEqual (new Vector3(2f,2f,0),vec_round );
	}
	[UnityTest]
	public IEnumerator Enemy_BatWayPoint2() {
		NetworkServer.Listen(7777);
		Instantiate (Resources.Load ("Tests/maincanvas"));
		Instantiate (Resources.Load ("Tests/maincamera"));
		Instantiate (Resources.Load ("Tests/gamemanager"));
		GameObject enemyBat = Instantiate (Resources.Load ("Tests/Enemy_Bat 1")) as GameObject;
		enemyBat.transform.position = new Vector2 (0, 0);
		Enemy bat=GameObject.FindGameObjectWithTag ("Enemy").GetComponent<Enemy>();
		bat.waitAtWaypointTime = 0f;
		bat.moveSpeed = 3f;
		bat.loopWaypoints = false;
		int i = 1;
		foreach (var gameObject in GameObject.FindGameObjectsWithTag("WayPoint")) {

			gameObject.transform.position = new Vector2 (-2*i, -2*i);
			i = i +1;
		}

		yield return new WaitForSeconds(10);
		Vector3 vec = GameObject.FindGameObjectWithTag ("Enemy").transform.position;
		Vector3 vec_round = new Vector3 (Mathf.Round (vec.x), Mathf.Round (vec.y),Mathf.Round (vec.z)); //rounds postion to nearest integer
		Assert.AreEqual (new Vector3(-4f,-4f,0),vec_round);
	}
	[TearDown]
	public void afterEveryTest(){
		foreach(GameObject i in Object.FindObjectsOfType<GameObject>()) {
			Destroy (i);
		}
		NetworkServer.dontListen = true;
	}


}





