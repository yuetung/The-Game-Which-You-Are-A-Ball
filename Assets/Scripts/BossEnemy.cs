using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossEnemy : MonoBehaviour {

	public enum State {
		Init,
		OpenEye,
		Moving,
		Charging,
		Releasing,
	};

	public State state = State.Init;
	public string[] patterns;
	public string[] chargedPatterns;
	private Animator _animator;
	private Enemy enemyScript;
	private GameObject player;
	ProjectilePatternFactory projectilePatternFactory;
	public float initIdleTime = 5f;
	public float noChargeTime = 10f;
	private float nextChargeTime = 10f;
	public float chargingTime = 5f;
	public float releaseTime = 1f;
	public float cooldownTime = 1f;
	public float moveSpeed = 3f;
	private float spawnTime;

	public void Start(){
		enemyScript = gameObject.GetComponent<Enemy>();
		_animator = gameObject.GetComponent<Animator>();
		projectilePatternFactory = GameManager.gm.gameObject.GetComponent<ProjectilePatternFactory>();
		player = GameObject.FindGameObjectWithTag ("Player");
		spawnTime = 0f;
		enemyScript.moveSpeed = 0f;
	}

	public void Update() {
		if (state == State.Moving) {
			if (Time.time >= nextChargeTime) {
				nextChargeTime = Time.time + noChargeTime + chargingTime+releaseTime;
				charge ();
			}
			else if (Time.time >= spawnTime) {
				spawnProjectile ();
			}
		}
	}

	public void openEye(){
		if (state == State.Init) {
			_animator.SetTrigger ("OpenEye");
			state = State.OpenEye;
			nextChargeTime = Time.time + noChargeTime;
			Invoke ("startMoving", initIdleTime);
		}
	}

	private void startMoving (){
		enemyScript.moveSpeed = moveSpeed;
		state = State.Moving;
	}

	private void charge(){
		if (state == State.Moving) {
			_animator.SetTrigger ("CloseEye");
			enemyScript.moveSpeed = 0;
			state = State.Charging;
			Invoke ("release", chargingTime);
		}
	}

	private void release(){
		if (state == State.Charging) {
			_animator.SetTrigger ("OpenEyeFast");
			enemyScript.moveSpeed = moveSpeed;
			state = State.Releasing;
			spawnChargedProjectile ();
			Invoke ("startMoving", releaseTime);
		}
	}

	void spawnProjectile() {
		if (player==null) {
			player = GameObject.FindGameObjectWithTag ("Player");
		}
		if (projectilePatternFactory == null) {
			projectilePatternFactory = GameManager.gm.gameObject.GetComponent<ProjectilePatternFactory>();
		}
		string pattern;
		if (Time.time >= spawnTime && patterns.Length>0 && player!=null) {
			int rand = Random.Range (0, patterns.Length);
			pattern= patterns [rand];
			Vector2 shootDirection = player.transform.position - transform.position;
			projectilePatternFactory.createProjectilePattern(pattern,transform.position,shootDirection, false,this.gameObject);
		}
		spawnTime = Time.time + cooldownTime;
	}

	void spawnChargedProjectile() {
		if (player==null) {
			player = GameObject.FindGameObjectWithTag ("Player");
		}
		if (projectilePatternFactory == null) {
			projectilePatternFactory = GameManager.gm.gameObject.GetComponent<ProjectilePatternFactory>();
		}
		string pattern;
		if (Time.time >= spawnTime && patterns.Length>0) {
			int rand = Random.Range (0, chargedPatterns.Length);
			pattern= chargedPatterns [rand];
			Vector2 shootDirection = player.transform.position - transform.position;
			projectilePatternFactory.createProjectilePattern(pattern,transform.position,shootDirection, false,this.gameObject);
		}
		spawnTime = Time.time + cooldownTime;
	}

}
