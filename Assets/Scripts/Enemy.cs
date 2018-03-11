using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour {

	[Range(1,200)]
	public int hp = 10;

	public GameObject EnemyHealthBar;

	public GameObject EnemyHealthBarPrefab;

	//public GameObject canvas_parent;

	[Range(0f,20f)]
	public float moveSpeed = 3f;

	[Tooltip("Empty game objects which this enemy will move towards and backwards")]
	public GameObject[] wayPoints;

	[Tooltip("How long wil the enemy wait at each wayPoints")]
	public float waitAtWaypointTime = 1f;

	public bool loopWaypoints = true;

	public GameObject explosionPrefab;

	//public Transform RectBox;

	[Tooltip("Set to true if the enemy can rotate when move")]
	public bool canRotate = false;
	Rigidbody2D _rigidbody;
	Animator _animator;

	[SerializeField]
	int waypointIndex = 0;
	float vx = 0f;  //Horizotal velocity
	float vy = 0f;  //Vertical velocity
	float moveTime; //Tracks time to determine whether enemy should move already
	bool moving = true;

	void Awake() {
		_rigidbody = GetComponent<Rigidbody2D> ();
		_animator = GetComponent<Animator> ();
		moveTime = 0f;
		moving = true;
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

	// Use this for initialization
	void Start () {
		Debug.Log ("created");
		CreateHealthBar ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.time >= moveTime) {
			EnemyMovement ();
		} else {
			_animator.SetBool ("Moving", false);
		}

		// update position of the health bar to follow the enemy
		Vector3 TransformedPos = new Vector3 ((float)0, (float)0.7, (float)0);
		Vector3 RectBoxPos = Camera.main.WorldToScreenPoint (this.transform.position + TransformedPos);
		Debug.Log (RectBoxPos);
		EnemyHealthBar.transform.position = RectBoxPos;

		// update the health bar to show current health of enemy
		float enemyhealth = hp/10.0f;
		EnemyHealthBar.GetComponent<Slider> ().value = enemyhealth;
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
		if (collision.tag == "Projectile") {
			int damage = collision.GetComponent<ProjectileController> ().getProjectileDamage();
			if (hp - damage <= 0) {
				hp = 0;
				Instantiate (explosionPrefab, transform.position, transform.rotation);
				for (int i = 0; i < wayPoints.Length; i++) {
					DestroyObject (wayPoints [i]);
				}
				DestroyObject (transform.parent.gameObject);
				DestroyObject (this.gameObject);
				DestroyObject (EnemyHealthBar);
			} else {
				hp -= damage;
			}
		}
	}
}
