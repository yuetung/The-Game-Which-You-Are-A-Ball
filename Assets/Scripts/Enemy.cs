using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour {

	[Range(1,200)]
	public int maxHp = 10;

	private int hp;

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
	public GameObject EnemyHealthBarPrefab;
	private GameObject EnemyHealthBar;

	void Awake() {
		_rigidbody = GetComponent<Rigidbody2D> ();
		_animator = GetComponent<Animator> ();
		moveTime = 0f;
		spawnTime = 0f;
		moving = true;
	}

	void Start() {
		hp = maxHp;
		projectilePatternFactory = GameManager.gm.GetComponent<ProjectilePatternFactory> ();
		CreateHealthBar ();
	}

	void CreateHealthBar(){
		// instantiate a new enemyhealthbar gameobject from prefab
		//		GameObject UIHealthBar = Instantiate(EnemyHealthBarPrefab,new Vector2 (0,0),Quaternion.identity) as GameObject;
		EnemyHealthBar = Instantiate(EnemyHealthBarPrefab,new Vector2 (0,0),Quaternion.identity) as GameObject;
		// place this gameobject inside the canvas (only way to display a UI)
		EnemyHealthBar.transform.SetParent(GameObject.FindGameObjectWithTag("MainCanvas").transform,false);
		Debug.Log ("created enemy health bar");
		// fill the enemyhealthbar with red color
		Transform barFill = EnemyHealthBar.transform.Find ("Fill Area").transform.Find ("Fill");
		barFill.GetComponent<Image>().color = Color.red;
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
		// update position of the health bar to follow the enemy
		Vector3 TransformedPos = new Vector3 ((float)0, (float)0.7, (float)0);
		Vector3 RectBoxPos = Camera.main.WorldToScreenPoint (transform.position + TransformedPos);
		EnemyHealthBar.transform.position = RectBoxPos;

		// update the health bar to show current health of enemy
		float enemyhealth = hp*1.0f/maxHp;
		EnemyHealthBar.GetComponent<Slider> ().value = enemyhealth;
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

	public void depleteHealth(int damage){
		if (hp - damage <= 0) {
			hp = 0;
			Instantiate (explosionPrefab, transform.position, transform.rotation);
			for (int i = 0; i < wayPoints.Length; i++) {
				DestroyObject (wayPoints [i]);
			}
			DestroyObject (transform.parent.gameObject);
			DestroyObject (EnemyHealthBar);
			DestroyObject (this.gameObject);
		} else {
			hp -= damage;
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
