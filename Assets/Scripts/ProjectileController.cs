﻿using UnityEngine;
using UnityEngine.Networking;

public class ProjectileController : NetworkBehaviour {

	public PlayerController.ElementType elementType = PlayerController.ElementType.Default;
	[Tooltip("true projectile speed = player base speed * projectile speed")]
	public float projectileSpeed = 1.0f;

	[Tooltip("true damage = player base power * projectile damage")]
	public int projectileDamage = 1;

	[Tooltip("how many seconds before destroying object when hit, leave just enought to play animation")]
	public float explodeAnimationSeconds = 1.0f;

	[Tooltip("only change this if sprite is originally not facing right" +
		"if facing top, type 270; facing left, type 180; facing down type 90")]
	public float angleAdjustment = 0.0f;

	public bool belongToPlayer= false;

	private bool alreadyHit = false;

    [SyncVar]
    public GameObject shooter;

	// Store references to gamebject Components
	private Rigidbody2D _rigidbody;

	void Awake () {
		_rigidbody = gameObject.GetComponent<Rigidbody2D>();
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	// Initializer called by Player during instantiation
	public void setVelocityAndRotation(Vector2 velocity, float rotation) {
		_rigidbody.velocity = velocity;
		_rigidbody.rotation = rotation+angleAdjustment;
	}

	public void setVelocity(Vector2 velocity) {
		_rigidbody.velocity = velocity;
	}

	public void setRotation(float rotation) {
		_rigidbody.rotation = rotation+angleAdjustment;
	}

	// Collision with wall
	void OnTriggerEnter2D(Collider2D other) {
		if (alreadyHit)
			return;
        
		if (other.tag == "Wall") {
			gameObject.GetComponent<Rigidbody2D> ().velocity = Vector2.zero;
			gameObject.GetComponent<Animator> ().SetTrigger ("Explode");
			Invoke ("DestroyNow", explodeAnimationSeconds);
			alreadyHit = true;
		}
		if (other.tag == "BreakableWall") {
			gameObject.GetComponent<Rigidbody2D> ().velocity = Vector2.zero;
			gameObject.GetComponent<Animator> ().SetTrigger ("Explode");
			other.GetComponent<BreakableWall> ().depleteHealth (projectileDamage,elementType);
			Invoke ("DestroyNow", explodeAnimationSeconds);
			alreadyHit = true;
		}
		else if (other.tag == "Enemy" && belongToPlayer) {
			gameObject.GetComponent<Rigidbody2D> ().velocity = Vector2.zero;
			gameObject.GetComponent<Animator> ().SetTrigger ("Explode");
			other.GetComponent<Enemy> ().depleteHealth (projectileDamage);
			Invoke ("DestroyNow", explodeAnimationSeconds);
			alreadyHit = true;
		}

        else if (shooter != null && other.gameObject == shooter.gameObject)
        {
            Debug.Log("Hit self");
            //do nothing
        }

        else if (other.tag == "Player")
        {
            gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            gameObject.GetComponent<Animator>().SetTrigger("Explode");
			other.GetComponent<PlayerController> ().depleteHealth (projectileDamage);
            Invoke("DestroyNow", explodeAnimationSeconds);
			alreadyHit = true;
        }
	}

	void DestroyNow() {
		DestroyObject (this.gameObject);
	}

	public int getProjectileDamage() {
		return projectileDamage;
	}

	public void belongsToPlayer() {
		belongToPlayer = true;
	}

	public bool getBelongsToPlayer() {
		return belongToPlayer;
	}
}
