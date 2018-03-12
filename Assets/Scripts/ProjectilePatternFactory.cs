using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ProjectilePatternFactory : NetworkBehaviour {

	private ProjectileFactory projectileFactory;
	// Use this for initialization
	void Start () {
		projectileFactory = GameManager.gm.GetComponent<ProjectileFactory> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void createProjectilePattern(string pattern, Vector3 startPosition, Vector2 shootDirection, bool belongToPlayer) {
		if (pattern == "shoot5FireballSpread30Degree") {
			for (int i = 0; i < 5; i++) {
				Rigidbody2D projectile = projectileFactory.getProjectileFromType (PlayerController.ElementType.Fire, 1);
				Rigidbody2D clone;
				clone = Instantiate (projectile, startPosition, transform.rotation) as Rigidbody2D;
				GameObject cloneGameObject = clone.gameObject;
				if (belongToPlayer) {
					cloneGameObject.GetComponent<ProjectileController> ().belongsToPlayer ();
				}
				float rotation = Mathf.Atan2 (shootDirection.y, shootDirection.x) * Mathf.Rad2Deg-15+30*i/5;
				cloneGameObject.GetComponent<ProjectileController> ().setRotation (rotation);
				//float vx = cloneGameObject.transform.right*projectile.GetComponent<ProjectileController> ().projectileSpeed;
				//float vy = cloneGameObject.transform.up*projectile.GetComponent<ProjectileController> ().projectileSpeed;
				Vector2 velocity = new Vector2(Mathf.Cos(rotation*Mathf.Deg2Rad),Mathf.Sin(rotation*Mathf.Deg2Rad)).normalized*cloneGameObject.GetComponent<ProjectileController> ().projectileSpeed;
				cloneGameObject.GetComponent<ProjectileController> ().setVelocity (velocity);
				NetworkServer.Spawn (cloneGameObject);
			}
		}
		if (pattern == "shoot8Fireball360DegreeSpread") {
			for (int i = 0; i < 8; i++) {
				Rigidbody2D projectile = projectileFactory.getProjectileFromType (PlayerController.ElementType.Fire, 1);
				Rigidbody2D clone;
				clone = Instantiate (projectile, startPosition, transform.rotation) as Rigidbody2D;
				GameObject cloneGameObject = clone.gameObject;
				if (belongToPlayer) {
					cloneGameObject.GetComponent<ProjectileController> ().belongsToPlayer ();
				}
				float rotation = Mathf.Atan2 (shootDirection.y, shootDirection.x) * Mathf.Rad2Deg+45*i;
				cloneGameObject.GetComponent<ProjectileController> ().setRotation (rotation);
				//float vx = cloneGameObject.transform.right*projectile.GetComponent<ProjectileController> ().projectileSpeed;
				//float vy = cloneGameObject.transform.up*projectile.GetComponent<ProjectileController> ().projectileSpeed;
				Vector2 velocity = new Vector2(Mathf.Cos(rotation*Mathf.Deg2Rad),Mathf.Sin(rotation*Mathf.Deg2Rad)).normalized*cloneGameObject.GetComponent<ProjectileController> ().projectileSpeed;
				cloneGameObject.GetComponent<ProjectileController> ().setVelocity (velocity);
				NetworkServer.Spawn (cloneGameObject);
			}
		}
		if (pattern == "basicFireball") {
			Rigidbody2D projectile = projectileFactory.getProjectileFromType (PlayerController.ElementType.Fire, 1);
			Rigidbody2D clone;
			clone = Instantiate (projectile, startPosition, transform.rotation) as Rigidbody2D;
			GameObject cloneGameObject = clone.gameObject;
			if (belongToPlayer) {
				cloneGameObject.GetComponent<ProjectileController> ().belongsToPlayer ();
			}
			float rotation = Mathf.Atan2 (shootDirection.y, shootDirection.x) * Mathf.Rad2Deg;
			Vector2 velocity = shootDirection.normalized * cloneGameObject.GetComponent<ProjectileController>().projectileSpeed;
			cloneGameObject.GetComponent<ProjectileController> ().setVelocityAndRotation (velocity,rotation);
			NetworkServer.Spawn (cloneGameObject);
		}




	}
}
