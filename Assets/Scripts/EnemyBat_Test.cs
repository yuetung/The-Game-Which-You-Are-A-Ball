

using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using UnityEngine.Networking;

public class EnemyBat_Test: MonoBehaviour {
	// A UnityTest behaves like a coroutine in PlayMode
	// and allows you to yield null to skip a frame in EditMode
	[UnityTest] /*1 Projectile to enemeyBat: enemyBat Hp=7? true;*/
	public IEnumerator Enemy_BatHealthOnImpact() {
		NetworkServer.Listen(7777);
		GameObject maincanvas = Instantiate (Resources.Load ("Tests/maincanvas")) as GameObject;
		GameObject maincamera = Instantiate (Resources.Load ("Tests/maincamera")) as GameObject;
		GameObject gm= Instantiate (Resources.Load ("Tests/gamemanager")) as GameObject;
		GameObject enemyBat = Instantiate (Resources.Load ("Tests/Enemy_Bat 1")) as GameObject;
		GameObject projectile = Instantiate (Resources.Load ("Tests/FireballProjectile1")) as GameObject;
		enemyBat.transform.position = new Vector2 (0, 0);
		projectile.transform.position = new Vector2 (5, 0);
		ProjectileController pc = projectile.GetComponent<ProjectileController> ();
		pc.setVelocityAndRotation (new Vector2 (-4, 0), 0);

		yield return new WaitForSeconds(5);

		GameObject[] spawnedEnemyBatList = GameObject.FindGameObjectsWithTag("Enemy");
		GameObject  spawnedEnemyBat = spawnedEnemyBatList[0];
		Enemy enemyScript=spawnedEnemyBat.GetComponent<Enemy>();
		Assert.AreEqual (enemyScript.hp, 7);
	}
	[UnityTest] /*5 Projectile to enemeyBat: enemyBat Destroyed? true;*/
	public IEnumerator Enemy_BatDestroyOnImpact() {
		//GameObject[] projectileList = new GameObject[5];
		NetworkServer.Listen(7777);
		GameObject maincanvas = Instantiate (Resources.Load ("Tests/maincanvas")) as GameObject;
		GameObject maincamera = Instantiate (Resources.Load ("Tests/maincamera")) as GameObject;
		GameObject gm= Instantiate (Resources.Load ("Tests/gamemanager")) as GameObject;
		GameObject enemyBat = Instantiate (Resources.Load ("Tests/Enemy_Bat 1")) as GameObject;
		enemyBat.transform.position = new Vector2 (0, 0);
		for( int i=0; i<5;i++)
		{
			GameObject projectile = Instantiate (Resources.Load ("Tests/FireballProjectile1")) as GameObject;
			projectile.transform.position = new Vector2 (5, 0);
			ProjectileController pc = projectile.GetComponent<ProjectileController> ();
			pc.setVelocityAndRotation (new Vector2 (-4, 0), 0);
			//projectile [i] = projectile;
		}


		yield return new WaitForSeconds(5);

		//GameObject[] spawnedEnemyBatList = GameObject.FindGameObjectsWithTag("Enemy");
		//GameObject  spawnedEnemyBat = spawnedEnemyBatList[0];
		//Enemy enemyScript=spawnedEnemyBat.GetComponent<Enemy>();
		Assert.AreEqual (GameObject.FindGameObjectWithTag("Enemy"), null);
	}
	[UnityTest] /*WayPoint: enemyBat position? waypoint;*/
	public IEnumerator Enemy_BatWayPoint() {
		//GameObject[] projectileList = new GameObject[5];
		NetworkServer.Listen(7777);
		GameObject maincanvas = Instantiate (Resources.Load ("Tests/maincanvas")) as GameObject;
		GameObject maincamera = Instantiate (Resources.Load ("Tests/maincamera")) as GameObject;
		GameObject gm= Instantiate (Resources.Load ("Tests/gamemanager")) as GameObject;
		GameObject enemyBat = Instantiate (Resources.Load ("Tests/Enemy_Bat 1")) as GameObject;
		enemyBat.transform.position = new Vector2 (0, 0);
		foreach (var gameObject in GameObject.FindGameObjectsWithTag("WayPoint")) {
			gameObject.transform.position = new Vector2 (11, 11);
		}

		yield return new WaitForSeconds(10);

		Vector3 vec = GameObject.FindGameObjectWithTag ("Enemy").transform.position;
		Vector3 vec_round = new Vector3 (Mathf.Round (vec.x), Mathf.Round (vec.y),Mathf.Round (vec.z)); //rounds postion to nearest integer

		Assert.AreEqual (new Vector3(10f,10f,0),vec_round );
	}
	[UnityTest] /*WayPoint: enemyBat position? waypoint;*/
	public IEnumerator Enemy_BatWayPoint1() {
		//GameObject[] projectileList = new GameObject[5];
		NetworkServer.Listen(7777);
		GameObject maincanvas = Instantiate (Resources.Load ("Tests/maincanvas")) as GameObject;
		GameObject maincamera = Instantiate (Resources.Load ("Tests/maincamera")) as GameObject;
//		var playerPrefab = Resources.Load ("Tests/player");
//		GameObject player = (GameObject)Instantiate(playerPrefab);

		GameObject gm= Instantiate (Resources.Load ("Tests/gamemanager")) as GameObject;
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





