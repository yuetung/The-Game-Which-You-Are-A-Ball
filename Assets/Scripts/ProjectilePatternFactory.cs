using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ProjectilePatternFactory : NetworkBehaviour {
	public GameObject lightningEffect;

	[HideInInspector]
	public ProjectileFactory projectileFactory;
	// Use this for initialization
	void Start () {
		projectileFactory = GameManager.gm.GetComponent<ProjectileFactory> ();
	}
	
	// Update is called once per frame

	void Update () {
	}

	public void createProjectilePattern(string pattern, Vector3 startPosition, Vector2 shootDirection, bool belongToPlayer, GameObject shooter) {
		// Fire, Water
		Vector2 noDirection = new Vector2(1.0f, 0.0f);
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
		if (pattern == "weakFireball") {

			Rigidbody2D projectile = projectileFactory.getProjectileFromType (PlayerController.ElementType.Fire, 0);
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
		if (pattern == "shoot3FireballSpread30Degree2") {
			for (int i = 0; i < 3; i++) {
				Rigidbody2D projectile = projectileFactory.getProjectileFromType (PlayerController.ElementType.Fire, 2);
				Rigidbody2D clone;
				clone = Instantiate (projectile, startPosition, transform.rotation) as Rigidbody2D;
				GameObject cloneGameObject = clone.gameObject;
				if (belongToPlayer) {
					cloneGameObject.GetComponent<ProjectileController> ().belongsToPlayer ();
				}
				float rotation = Mathf.Atan2 (shootDirection.y, shootDirection.x) * Mathf.Rad2Deg-15+30*i/3;
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

		if (pattern == "shoot18lv2Fireball360DegreeSpread") {
			for (int i = 0; i < 18; i++) {
				Rigidbody2D projectile = projectileFactory.getProjectileFromType (PlayerController.ElementType.Fire, 2);
				Rigidbody2D clone;
				clone = Instantiate (projectile, startPosition, transform.rotation) as Rigidbody2D;
				GameObject cloneGameObject = clone.gameObject;
				if (belongToPlayer) {
					cloneGameObject.GetComponent<ProjectileController> ().belongsToPlayer ();
				}
				float rotation = Mathf.Atan2 (shootDirection.y, shootDirection.x) * Mathf.Rad2Deg+20*i;
				cloneGameObject.GetComponent<ProjectileController> ().setRotation (rotation);
				//float vx = cloneGameObject.transform.right*projectile.GetComponent<ProjectileController> ().projectileSpeed;
				//float vy = cloneGameObject.transform.up*projectile.GetComponent<ProjectileController> ().projectileSpeed;
				Vector2 velocity = new Vector2(Mathf.Cos(rotation*Mathf.Deg2Rad),Mathf.Sin(rotation*Mathf.Deg2Rad)).normalized*cloneGameObject.GetComponent<ProjectileController> ().projectileSpeed;
				cloneGameObject.GetComponent<ProjectileController> ().setVelocity (velocity);
				NetworkServer.Spawn (cloneGameObject);
			}
		}

		if (pattern == "basicWaterball") {
			Rigidbody2D projectile = projectileFactory.getProjectileFromType (PlayerController.ElementType.Water, 1);
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

		if (pattern == "weakWaterball") {
			Rigidbody2D projectile = projectileFactory.getProjectileFromType (PlayerController.ElementType.Water, 0);
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
		if (pattern == "shoot5lv3Waterball360DegreeSpread") {
			for (int i = 0; i < 5; i++) {
				Rigidbody2D projectile = projectileFactory.getProjectileFromType (PlayerController.ElementType.Water, 3);
				Rigidbody2D clone;
				clone = Instantiate (projectile, startPosition, transform.rotation) as Rigidbody2D;
				GameObject cloneGameObject = clone.gameObject;
				if (belongToPlayer) {
					cloneGameObject.GetComponent<ProjectileController> ().belongsToPlayer ();
				}
				float rotation = Mathf.Atan2 (shootDirection.y, shootDirection.x) * Mathf.Rad2Deg+72*i;
				cloneGameObject.GetComponent<ProjectileController> ().setRotation (rotation);
				//float vx = cloneGameObject.transform.right*projectile.GetComponent<ProjectileController> ().projectileSpeed;
				//float vy = cloneGameObject.transform.up*projectile.GetComponent<ProjectileController> ().projectileSpeed;
				Vector2 velocity = new Vector2(Mathf.Cos(rotation*Mathf.Deg2Rad),Mathf.Sin(rotation*Mathf.Deg2Rad)).normalized*cloneGameObject.GetComponent<ProjectileController> ().projectileSpeed;
				cloneGameObject.GetComponent<ProjectileController> ().setVelocity (velocity);
				NetworkServer.Spawn (cloneGameObject);
			}
		}
		if (pattern == "shoot4Waterball360DegreeSpread0") {
			for (int i = 0; i < 4; i++) {
				Rigidbody2D projectile = projectileFactory.getProjectileFromType (PlayerController.ElementType.Water, 1);
				Rigidbody2D clone;
				clone = Instantiate (projectile, startPosition, transform.rotation) as Rigidbody2D;
				GameObject cloneGameObject = clone.gameObject;
				if (belongToPlayer) {
					cloneGameObject.GetComponent<ProjectileController> ().belongsToPlayer ();
				}
				float rotation = Mathf.Atan2 (noDirection.y, noDirection.x) * Mathf.Rad2Deg +360/4*i+ 90/8*0;
				cloneGameObject.GetComponent<ProjectileController> ().setRotation (rotation);
				//float vx = cloneGameObject.transform.right*projectile.GetComponent<ProjectileController> ().projectileSpeed;
				//float vy = cloneGameObject.transform.up*projectile.GetComponent<ProjectileController> ().projectileSpeed;
				Vector2 velocity = new Vector2 (Mathf.Cos (rotation * Mathf.Deg2Rad), Mathf.Sin (rotation * Mathf.Deg2Rad)).normalized * cloneGameObject.GetComponent<ProjectileController> ().projectileSpeed;
				cloneGameObject.GetComponent<ProjectileController> ().setVelocity (velocity);
				NetworkServer.Spawn (cloneGameObject);
			}
		}		
		if (pattern == "shoot4Waterball360DegreeSpread1") {
			for (int i = 0; i < 4; i++) {
				Rigidbody2D projectile = projectileFactory.getProjectileFromType (PlayerController.ElementType.Water, 1);
				Rigidbody2D clone;
				clone = Instantiate (projectile, startPosition, transform.rotation) as Rigidbody2D;
				GameObject cloneGameObject = clone.gameObject;
				if (belongToPlayer) {
					cloneGameObject.GetComponent<ProjectileController> ().belongsToPlayer ();
				}
				float rotation = Mathf.Atan2 (noDirection.y, noDirection.x) * Mathf.Rad2Deg +360/4*i+ 90 / 8 * 1;
				cloneGameObject.GetComponent<ProjectileController> ().setRotation (rotation);
				//float vx = cloneGameObject.transform.right*projectile.GetComponent<ProjectileController> ().projectileSpeed;
				//float vy = cloneGameObject.transform.up*projectile.GetComponent<ProjectileController> ().projectileSpeed;
				Vector2 velocity = new Vector2 (Mathf.Cos (rotation * Mathf.Deg2Rad), Mathf.Sin (rotation * Mathf.Deg2Rad)).normalized * cloneGameObject.GetComponent<ProjectileController> ().projectileSpeed;
				cloneGameObject.GetComponent<ProjectileController> ().setVelocity (velocity);
				NetworkServer.Spawn (cloneGameObject);
			}
		}
		if (pattern == "shoot4Waterball360DegreeSpread2") {
			for (int i = 0; i < 4; i++) {
				Rigidbody2D projectile = projectileFactory.getProjectileFromType (PlayerController.ElementType.Water, 1);
				Rigidbody2D clone;
				clone = Instantiate (projectile, startPosition, transform.rotation) as Rigidbody2D;
				GameObject cloneGameObject = clone.gameObject;
				if (belongToPlayer) {
					cloneGameObject.GetComponent<ProjectileController> ().belongsToPlayer ();
				}
				float rotation = Mathf.Atan2 (noDirection.y, noDirection.x) * Mathf.Rad2Deg +360/4*i+ 90 / 8 * 2;
				cloneGameObject.GetComponent<ProjectileController> ().setRotation (rotation);
				//float vx = cloneGameObject.transform.right*projectile.GetComponent<ProjectileController> ().projectileSpeed;
				//float vy = cloneGameObject.transform.up*projectile.GetComponent<ProjectileController> ().projectileSpeed;
				Vector2 velocity = new Vector2 (Mathf.Cos (rotation * Mathf.Deg2Rad), Mathf.Sin (rotation * Mathf.Deg2Rad)).normalized * cloneGameObject.GetComponent<ProjectileController> ().projectileSpeed;
				cloneGameObject.GetComponent<ProjectileController> ().setVelocity (velocity);
				NetworkServer.Spawn (cloneGameObject);
			}
		}
		if (pattern == "shoot4Waterball360DegreeSpread3") {
			for (int i = 0; i < 4; i++) {
				Rigidbody2D projectile = projectileFactory.getProjectileFromType (PlayerController.ElementType.Water, 1);
				Rigidbody2D clone;
				clone = Instantiate (projectile, startPosition, transform.rotation) as Rigidbody2D;
				GameObject cloneGameObject = clone.gameObject;
				if (belongToPlayer) {
					cloneGameObject.GetComponent<ProjectileController> ().belongsToPlayer ();
				}
				float rotation = Mathf.Atan2 (noDirection.y, noDirection.x) * Mathf.Rad2Deg + 360/4*i+ 90/8*3;
				cloneGameObject.GetComponent<ProjectileController> ().setRotation (rotation);
				//float vx = cloneGameObject.transform.right*projectile.GetComponent<ProjectileController> ().projectileSpeed;
				//float vy = cloneGameObject.transform.up*projectile.GetComponent<ProjectileController> ().projectileSpeed;
				Vector2 velocity = new Vector2 (Mathf.Cos (rotation * Mathf.Deg2Rad), Mathf.Sin (rotation * Mathf.Deg2Rad)).normalized * cloneGameObject.GetComponent<ProjectileController> ().projectileSpeed;
				cloneGameObject.GetComponent<ProjectileController> ().setVelocity (velocity);
				NetworkServer.Spawn (cloneGameObject);
			}
		}
		if (pattern == "shoot4Waterball360DegreeSpread4") {
			for (int i = 0; i < 4; i++) {
				Rigidbody2D projectile = projectileFactory.getProjectileFromType (PlayerController.ElementType.Water, 1);
				Rigidbody2D clone;
				clone = Instantiate (projectile, startPosition, transform.rotation) as Rigidbody2D;
				GameObject cloneGameObject = clone.gameObject;
				if (belongToPlayer) {
					cloneGameObject.GetComponent<ProjectileController> ().belongsToPlayer ();
				}
				float rotation = Mathf.Atan2 (noDirection.y, noDirection.x) * Mathf.Rad2Deg + 360/4*i+ 90/8*4;
				cloneGameObject.GetComponent<ProjectileController> ().setRotation (rotation);
				//float vx = cloneGameObject.transform.right*projectile.GetComponent<ProjectileController> ().projectileSpeed;
				//float vy = cloneGameObject.transform.up*projectile.GetComponent<ProjectileController> ().projectileSpeed;
				Vector2 velocity = new Vector2 (Mathf.Cos (rotation * Mathf.Deg2Rad), Mathf.Sin (rotation * Mathf.Deg2Rad)).normalized * cloneGameObject.GetComponent<ProjectileController> ().projectileSpeed;
				cloneGameObject.GetComponent<ProjectileController> ().setVelocity (velocity);
				NetworkServer.Spawn (cloneGameObject);
			}
		}
		if (pattern == "shoot4Waterball360DegreeSpread5") {
			for (int i = 0; i < 4; i++) {
				Rigidbody2D projectile = projectileFactory.getProjectileFromType (PlayerController.ElementType.Water, 1);
				Rigidbody2D clone;
				clone = Instantiate (projectile, startPosition, transform.rotation) as Rigidbody2D;
				GameObject cloneGameObject = clone.gameObject;
				if (belongToPlayer) {
					cloneGameObject.GetComponent<ProjectileController> ().belongsToPlayer ();
				}
				float rotation = Mathf.Atan2 (noDirection.y, noDirection.x) * Mathf.Rad2Deg + 360/4*i+ 90/8*5;
				cloneGameObject.GetComponent<ProjectileController> ().setRotation (rotation);
				//float vx = cloneGameObject.transform.right*projectile.GetComponent<ProjectileController> ().projectileSpeed;
				//float vy = cloneGameObject.transform.up*projectile.GetComponent<ProjectileController> ().projectileSpeed;
				Vector2 velocity = new Vector2 (Mathf.Cos (rotation * Mathf.Deg2Rad), Mathf.Sin (rotation * Mathf.Deg2Rad)).normalized * cloneGameObject.GetComponent<ProjectileController> ().projectileSpeed;
				cloneGameObject.GetComponent<ProjectileController> ().setVelocity (velocity);
				NetworkServer.Spawn (cloneGameObject);
			}
		}
		if (pattern == "shoot4Waterball360DegreeSpread6") {
			for (int i = 0; i < 4; i++) {
				Rigidbody2D projectile = projectileFactory.getProjectileFromType (PlayerController.ElementType.Water, 1);
				Rigidbody2D clone;
				clone = Instantiate (projectile, startPosition, transform.rotation) as Rigidbody2D;
				GameObject cloneGameObject = clone.gameObject;
				if (belongToPlayer) {
					cloneGameObject.GetComponent<ProjectileController> ().belongsToPlayer ();
				}
				float rotation = Mathf.Atan2 (noDirection.y, noDirection.x) * Mathf.Rad2Deg + 360/4*i+ 90/8*6;
				cloneGameObject.GetComponent<ProjectileController> ().setRotation (rotation);
				//float vx = cloneGameObject.transform.right*projectile.GetComponent<ProjectileController> ().projectileSpeed;
				//float vy = cloneGameObject.transform.up*projectile.GetComponent<ProjectileController> ().projectileSpeed;
				Vector2 velocity = new Vector2 (Mathf.Cos (rotation * Mathf.Deg2Rad), Mathf.Sin (rotation * Mathf.Deg2Rad)).normalized * cloneGameObject.GetComponent<ProjectileController> ().projectileSpeed;
				cloneGameObject.GetComponent<ProjectileController> ().setVelocity (velocity);
				NetworkServer.Spawn (cloneGameObject);
			}
		}
		if (pattern == "shoot4Waterball360DegreeSpread7") {
			for (int i = 0; i < 4; i++) {
				Rigidbody2D projectile = projectileFactory.getProjectileFromType (PlayerController.ElementType.Water, 1);
				Rigidbody2D clone;
				clone = Instantiate (projectile, startPosition, transform.rotation) as Rigidbody2D;
				GameObject cloneGameObject = clone.gameObject;
				if (belongToPlayer) {
					cloneGameObject.GetComponent<ProjectileController> ().belongsToPlayer ();
				}
				float rotation = Mathf.Atan2 (noDirection.y, noDirection.x) * Mathf.Rad2Deg + 360/4*i+ 90/8*7;
				cloneGameObject.GetComponent<ProjectileController> ().setRotation (rotation);
				//float vx = cloneGameObject.transform.right*projectile.GetComponent<ProjectileController> ().projectileSpeed;
				//float vy = cloneGameObject.transform.up*projectile.GetComponent<ProjectileController> ().projectileSpeed;
				Vector2 velocity = new Vector2 (Mathf.Cos (rotation * Mathf.Deg2Rad), Mathf.Sin (rotation * Mathf.Deg2Rad)).normalized * cloneGameObject.GetComponent<ProjectileController> ().projectileSpeed;
				cloneGameObject.GetComponent<ProjectileController> ().setVelocity (velocity);
				NetworkServer.Spawn (cloneGameObject);
			}
		}if (pattern == "basicLightningball") {
			Rigidbody2D projectile = projectileFactory.getProjectileFromType (PlayerController.ElementType.Lightning, 0);
			Rigidbody2D clone;
			float maxDistance = projectile.GetComponent<ProjectileController> ().maxDistance; 
			int layerMask = LayerMask.GetMask ("Player", "Wall"); //changed Player and wall
			RaycastHit2D hit = Physics2D.Raycast (startPosition, shootDirection, Mathf.Infinity, layerMask);// change transform to startPostion
			Vector2 finalPosition;
			if (hit.distance <= maxDistance) {
				finalPosition = hit.transform.position;
			} else {
				finalPosition = new Vector2 (startPosition.x, startPosition.y) + shootDirection.normalized * maxDistance;
			}
			clone = Instantiate (projectile, finalPosition, transform.rotation) as Rigidbody2D;
			GameObject lightning = Instantiate (lightningEffect, transform.position, transform.rotation);
			lightning.GetComponent<LightningBoltScript> ().StartPosition = startPosition;
			lightning.GetComponent<LightningBoltScript> ().EndPosition = finalPosition;
			NetworkServer.Spawn (lightning);
			GameObject cloneGameObject = clone.gameObject;
			NetworkServer.Spawn(cloneGameObject);
			if (belongToPlayer) {
				cloneGameObject.GetComponent<ProjectileController> ().belongsToPlayer ();
			}
		}
		if (pattern == "basicRockball") {

			Rigidbody2D projectile = projectileFactory.getProjectileFromType (PlayerController.ElementType.Earth, 1);
			Rigidbody2D clone;
			clone = Instantiate (projectile, startPosition, transform.rotation) as Rigidbody2D;
			GameObject cloneGameObject = clone.gameObject;
			if (belongToPlayer) {
				cloneGameObject.GetComponent<ProjectileController> ().belongsToPlayer ();
			}
			cloneGameObject.GetComponent<EarthProjectileSpawner> ().shooter = shooter;
			NetworkServer.Spawn (cloneGameObject);
		}
	}
}
