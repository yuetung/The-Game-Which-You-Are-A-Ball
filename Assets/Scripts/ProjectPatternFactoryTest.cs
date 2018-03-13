using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using UnityEngine.Networking;

public class ProjectilePatternFactoryTest: MonoBehaviour {
	// A UnityTest behaves like a coroutine in PlayMode
	// and allows you to yield null to skip a frame in EditMode
	[UnityTest] /*1 Projectile to enemeyBat: enemyBat Hp=7? true;*/
	public IEnumerator Test1() {
		NetworkServer.Listen(7777);
		GameObject gm= Instantiate (Resources.Load ("Tests/GameManager")) as GameObject;
		ProjectilePatternFactory factory = gm.GetComponent<ProjectilePatternFactory> ();
		string pattern = "shoot5FireballSpread30Degree";
		Vector3 startPosition = new Vector3(0,0,0);
		Vector2 shootDirection = new Vector2 (0,0);
		bool belongToPlayer = false;
		factory.projectileFactory = gm.GetComponent<ProjectileFactory> ();
		factory.createProjectilePattern (pattern, startPosition, shootDirection, belongToPlayer);
		yield return new WaitForSeconds(1);
		GameObject[] projectilesSpawned = GameObject.FindGameObjectsWithTag ("Projectile");
		Assert.AreEqual (5, projectilesSpawned.Length);
	}
	[UnityTest] /*1 Projectile to enemeyBat: enemyBat Hp=7? true;*/
	public IEnumerator Test2() {
		NetworkServer.Listen(7777);
		GameObject gm= Instantiate (Resources.Load ("Tests/GameManager")) as GameObject;
		ProjectileFactory projectileFactory = gm.GetComponent<ProjectileFactory> ();
		PlayerController.ElementType element = PlayerController.ElementType.Fire;
		int level = 1;
		Rigidbody2D projectile = projectileFactory.getProjectileFromType (element, level);
		yield return new WaitForSeconds(1);
		GameObject projectile2 = Instantiate (Resources.Load ("Tests/FireballProjectile1")) as GameObject;
		Assert.AreEqual (projectile2.GetComponent<ProjectileController>().projectileDamage, projectile.gameObject.GetComponent<ProjectileController>().projectileDamage);
	}


	[TearDown]
	public void afterEveryTest(){
		foreach (var gameObject in GameObject.FindGameObjectsWithTag("Enemy")) {
			Object.Destroy (gameObject);
		}
		NetworkServer.dontListen=true;
	}


}

