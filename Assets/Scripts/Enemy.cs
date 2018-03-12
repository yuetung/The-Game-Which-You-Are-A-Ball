using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

	[Range(1,200)]
	public int hp = 10;

	[Range(0f,20f)]
	public float moveSpeed = 3f;

	[Tooltip("Empty game objects which this enemy will move towards and backwards")]
	public GameObject[] wayPoints;

	[Tooltip("How long will the enemy wait at each wayPoints")]
	public float waitAtWaypointTime = 1f;

	public bool loopWaypoints = true;

	public GameObject explosionPrefab;

	[Tooltip("check list of available patterns at ProjectilePatternFactory class")]
	public string[] patterns;

	[Tooltip("How long before each attack")]
	public float cooldownTime = 3.0f;

	[Tooltip("How far away to sense player")]
	public float sensePlayer = 5.0f;

	[Tooltip("Set to true if the enemy can rotate when move")]
	public bool canRotate = false;
	Rigidbody2D _rigidbody;
	Animator _animator;

	[SerializeField]
	int waypointIndex = 0;
	GameObject player;
	float vx = 0f;  //Horizotal velocity
	float vy = 0f;  //Vertical velocity
	float moveTime; //Tracks time to determine whether enemy should move already
	float spawnTime;
	bool moving = true;
	ProjectilePatternFactory projectilePatternFactory;

	void Awake() {
		_rigidbody = GetComponent<Rigidbody2D> ();
		_animator = GetComponent<Animator> ();
		moveTime = 0f;
		spawnTime = 0f;
		moving = true;
	}

	void Start() {
		projectilePatternFactory = GameManager.gm.GetComponent<ProjectilePatternFactory> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.time >= moveTime) {
			EnemyMovement ();
		} else {
			_animator.SetBool ("Moving", false);
		}
		if (Time.time >= spawnTime) {
			spawnProjectile ();
		} 
	}

	void spawnProjectile() {
		if (Time.time >= spawnTime && isNearPlayer () && patterns.Length>0) {
			int rand = Random.Range (0, patterns.Length);
			string pattern = patterns [rand];
			Vector2 shootDirection = player.transform.position - transform.position;
			Debug.Log (projectilePatternFactory);
			projectilePatternFactory.createProjectilePattern(pattern,transform.position,shootDirection, false);
		}
		spawnTime = Time.time + cooldownTime;
	}
	void EnemyMovement() {
		if (wayPoints.Length != 0 && moving) {
			vx = wayPoints [waypointIndex].transform.position.x - transform.position.x;
			vy = wayPoints [waypointIndex].transform.position.y - transform.position.y;

			if (Mathf.Sqrt (vx * vx + vy * vy) <= 0.05f) {
				//Near wayPoint, stop moving
				_rigidbody.velocity = new Vector2 (0, 0);

				//go to next wayPoint
				waypointIndex++;

				//reset wayPoint if at final wayPoint
				if (waypointIndex >= wayPoints.Length) {
					if (loopWaypoints) {
						waypointIndex = 0;
					} else {
						moving = false;
					}
				}

				//setup wait time at current wayPoint
				moveTime = Time.time + waitAtWaypointTime;
			} else {
				//enemy is movng
				_animator.SetBool ("Moving", true);

				//set enemy's velocity to moveSpeed towards new target position
				_rigidbody.velocity = new Vector2 (vx * moveSpeed, vy * moveSpeed);

				//for enemy that can rotate
				if (canRotate) {
					_rigidbody.rotation = Mathf.Atan2 (vy, vx) * Mathf.Rad2Deg + 90; 
				}
			}
		}
	}

	void OnTriggerEnter2D(Collider2D collision) {
		if (collision.tag == "Projectile" && collision.GetComponent<ProjectileController>().getBelongsToPlayer()) {
			int damage = collision.GetComponent<ProjectileController> ().getProjectileDamage();
			if (hp - damage <= 0) {
				hp = 0;
				Instantiate (explosionPrefab, transform.position, transform.rotation);
				for (int i = 0; i < wayPoints.Length; i++) {
					DestroyObject (wayPoints [i]);
				}
				DestroyObject (transform.parent.gameObject);
				DestroyObject (this.gameObject);
			} else {
				hp -= damage;
			}
		}
	}

	bool isNearPlayer() {
		if (!player) {
			player = GameObject.FindGameObjectWithTag ("Player");
		}
		if (!player) return false;
		Vector3 playerLocation = player.transform.position;
		Vector3 difference = transform.position - playerLocation;
		if(difference.magnitude < sensePlayer)
		{
			return true;
		}
		return false;
	}
}
