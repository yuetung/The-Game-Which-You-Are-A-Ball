﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class BreakableWall : NetworkBehaviour {

	public GameObject explosionPrefab;
	public int maxHealth = 10;
	public PlayerController.ElementType elementType;
	[Tooltip("put 0 if initHealth = maxhealth for convenience")]
	public int initHealth;
	private int health;
	Animator _animator;
	public GameObject crystalPrefab;
	public int maxValue = 0;
	public int minValue = 0;
	public float dropRate = 1f;
	// Use this for initialization
	void Start () {
		if (initHealth != 0)
			health = initHealth;
		else health = maxHealth;
		_animator = gameObject.GetComponent<Animator> ();
		if (_animator)
			_animator.SetInteger ("HealthLevel", health*3/maxHealth);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
		
	public void depleteHealth(int damage, PlayerController.ElementType element){
		if (element != elementType && elementType!=PlayerController.ElementType.Default)
			return;
		if (health - damage <= 0) {
			int value = Random.Range (minValue, maxValue+1);
			bool drop = Random.value <= dropRate;
			Debug.Log(value+ "Crystal");
			if (value > 0 && drop) { 
				GameObject crystal = Instantiate (crystalPrefab, transform.position, Quaternion.identity);
				crystal.GetComponent<Collectible> ().setValue (value);
				//crystal.transform.localScale = new Vector3 (value / 50, value / 50, 1);
				Debug.Log("Crystal"+ crystal);
			}
			Invoke ("destroyNow", 0.3f);
		} else {
			health -= damage;
			if (_animator)
				_animator.SetInteger ("HealthLevel", health*3/maxHealth);
		}
	}

	public void destroyNow(){
		Instantiate (explosionPrefab, transform.position, transform.rotation);
		DestroyObject (this.gameObject);
        if (transform.GetComponent<NetworkIdentity>())
        {
<<<<<<< HEAD
			CmdDestroyNow ();
=======
			Invoke("CmdDestroyNow",0.3f);
>>>>>>> 6653df8eeb1a28ebeaea4020f52bfc5b308eb93a
        }
	}

    [Command]
    void CmdDestroyNow()
    {
        NetworkServer.Destroy(this.gameObject);
    }

}
