using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour {

	[Range(1,200)]
	public int maxHp = 10;

	public int hp;

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

	public bool chasePlayer = false;
	public float maxChaseDistance = 0f;

	public List<PlayerController.ElementType> immuneTo;

	[Tooltip("Set to true if the enemy can rotate when move")]
	public bool randomProjectile=false;
	public bool canRotate = false;
	public bool canFlip = false;

	public GameObject crystalPrefab;
	public int maxValue = 0;
	public int minValue = 0;

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
	public GameObject noticeBubble;
	private GameObject EnemyHealthBar;
	private GameObject gameManager;
	private Vector3 startPosition;
	CircleCollider2D _collider;
	private bool chasingPlayer = false;
	private bool sensingPlayer = false; // to display alert bubble for non-chaser enemies
	public float dontChaseForSeconds = 1f;
	private float chaseCooldownCount;

	void Awake() {
		_rigidbody = GetComponent<Rigidbody2D> ();
		_animator = GetComponent<Animator> ();
		_collider = GetComponent<CircleCollider2D> ();
		moveTime = 0f;
		spawnTime = 0f;
		moving = true;

	}
    IEnumerator WaitForSecondsWrapper(float secs)
    {
        yield return new UnityEngine.WaitForSeconds(secs);
    }
    void Start() {
		hp = maxHp;
		gameManager = GameManager.gm.gameObject;
        //var test1 = GameManager.gm;
		projectilePatternFactory = gameManager.GetComponent<ProjectilePatternFactory>();
        //projectilePatternFactory = GameManager.gm.GetComponent<ProjectilePatternFactory>();
        CreateHealthBar ();
		startPosition = this.transform.position;
		chaseCooldownCount = 0f;
    }

	void CreateHealthBar(){
        // instantiate a new enemyhealthbar gameobject from prefab
        //		GameObject UIHealthBar = Instantiate(EnemyHealthBarPrefab,new Vector2 (0,0),Quaternion.identity) as GameObject;
		Debug.Log("enemyhealthbarprefab: "+EnemyHealthBarPrefab);
		EnemyHealthBar = Instantiate(EnemyHealthBarPrefab,new Vector2 (0,0),Quaternion.identity) as GameObject;
        // place this gameobject inside the canvas (only way to display a UI)
		Debug.Log("enemyhealthbar: "+EnemyHealthBar);
		Debug.Log ("maincanvas: " + GameObject.FindGameObjectWithTag ("MainCanvas"));
		EnemyHealthBar.transform.SetParent(GameObject.FindGameObjectWithTag("MainCanvas").transform,false);
		Debug.Log ("created enemy health bar");
		// fill the enemyhealthbar with red color
		Transform barFill = EnemyHealthBar.transform.Find ("Fill Area").transform.Find ("Fill");
		barFill.GetComponent<Image>().color = Color.red;
	}
	int count=0;
	// Update is called once per frame
	void Update () {
		//Initiate gm if it has not been initiated
		if (projectilePatternFactory == null) {
			gameManager = GameManager.gm.gameObject;
			projectilePatternFactory = gameManager.GetComponent<ProjectilePatternFactory> ();
			CreateHealthBar ();
		} 
		float distanceFromStartPosition = Vector3.Magnitude (this.transform.position - startPosition);
		if (chasePlayer && isNearPlayer () && distanceFromStartPosition <= maxChaseDistance && chaseCooldownCount<=0) {
			ChasePlayer ();
		} 
		else {
			if (distanceFromStartPosition > maxChaseDistance) {
				chaseCooldownCount = dontChaseForSeconds;
			}
			if (chaseCooldownCount>0)
				chaseCooldownCount -= Time.deltaTime;
			if (chasingPlayer) {
				chasingPlayer = false;
			}
			if (Time.time >= moveTime) {
				EnemyMovement ();
			} else {
				_animator.SetBool ("Moving", false);
			}
		}
		if (Time.time >= spawnTime) {
			if (randomProjectile) {
				spawnProjectile ();
 			} else {
				spawnProjectile(count);
				count++;
			}
		}
        // update position of the health bar to follow the enemy
        Vector3 TransformedPos = new Vector3((float)0, (float)0.7, (float)0);
        Vector3 RectBoxPos = Camera.main.WorldToScreenPoint(transform.position + TransformedPos);
        EnemyHealthBar.transform.position = RectBoxPos;

        // update the health bar to show current health of enemy
        float enemyhealth = hp * 1.0f / maxHp;
        EnemyHealthBar.GetComponent<Slider>().value = enemyhealth;
    }

	void spawnProjectile() {
		string pattern;
		if (Time.time >= spawnTime && isNearPlayer () && patterns.Length>0) {
			int rand = Random.Range (0, patterns.Length);
			pattern= patterns [rand];
			Vector2 shootDirection = player.transform.position - transform.position;
			Debug.Log (projectilePatternFactory);
			projectilePatternFactory.createProjectilePattern(pattern,transform.position,shootDirection, false,this.gameObject);
		}
		spawnTime = Time.time + cooldownTime;
	}
	void spawnProjectile(int count) {
		string pattern;
		if (Time.time >= spawnTime && isNearPlayer () && patterns.Length>0) {
			pattern = patterns [count % patterns.Length];
			Vector2 shootDirection = player.transform.position - transform.position;
			Debug.Log (projectilePatternFactory);
			projectilePatternFactory.createProjectilePattern(pattern,transform.position,shootDirection, false, this.gameObject);
		}
		spawnTime = Time.time + cooldownTime;
	}
	void ChasePlayer() {
		_animator.SetBool ("Moving", true);
		_rigidbody.velocity = Vector3.Normalize (player.transform.position - gameObject.transform.position) * moveSpeed;
		if (!chasingPlayer) {
			chasingPlayer = true;
			generateAlertBubble ();
		}
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
				_rigidbody.velocity = Vector3.Normalize(new Vector3 (vx, vy,0)) * moveSpeed;

				//for enemy that can rotate
				if (canRotate) {
					_rigidbody.rotation = Mathf.Atan2 (vy, vx) * Mathf.Rad2Deg + 90; 
				}
				if (canFlip) {
					if ((transform.localScale.x < 0 && vx > 0) || (transform.localScale.x > 0 && vx < 0)) {
						transform.localScale = new Vector2 (-1 * transform.localScale.x, transform.localScale.y);
					}
				}
			}
		}
	}

	public void depleteHealth(int damage, PlayerController.ElementType elementType){
		if (immuneTo.Contains (elementType))
			return;
		if (hp - damage <= 0) {
			// add Spawn Gold coin , Kenny
			int value = Random.Range (minValue, maxValue);
			Debug.Log(value+ "Crystal");
			if (value > 0) { 
				GameObject crystal = Instantiate (crystalPrefab, transform.position, Quaternion.identity);
				crystal.GetComponent<Collectible> ().setValue (value);
				float scalingFactor = Mathf.Log (value);
				float scaled = scalingFactor * crystal.transform.localScale.x;
				crystal.transform.localScale = new Vector3 (scaled, scaled, crystal.transform.localScale.z);
				//crystal.transform.localScale = new Vector3 (value / 50, value / 50, 1);
				Debug.Log("Crystal"+ crystal);
			}
			//

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
			if (!chasePlayer && !sensingPlayer) {
				sensingPlayer = true;
				generateAlertBubble();
			}
			return true;
		}
		sensingPlayer = false;
		return false;
	}
	void generateAlertBubble(){
		Vector3 bubbleOffset = new Vector3 (_collider.radius*1.1f, _collider.radius*1.1f, 0);
		Vector3 bubbleScale = new Vector3 (_collider.radius, _collider.radius, 0);
		GameObject bubble = Instantiate (noticeBubble, transform.position+bubbleOffset, Quaternion.identity);
		bubble.transform.localScale = bubbleScale;
		bubble.GetComponent<EnemyNotice> ().setParentOffset (this.gameObject, bubbleOffset);
	}
}
