using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableWall : MonoBehaviour {

	public GameObject explosionPrefab;
	[Range(1,20)]
	public int maxHealth = 10;
	public PlayerController.ElementType elementType;
	private int health;
	Animator _animator;
	// Use this for initialization
	void Start () {
		health = maxHealth;
		_animator = gameObject.GetComponent<Animator> ();
		_animator.SetInteger ("HealthLevel", 3);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
		
	public void depleteHealth(int damage, PlayerController.ElementType element){
		if (elementType!=PlayerController.ElementType.Default && element != elementType)
			return;
		if (health - damage <= 0) {
			Instantiate (explosionPrefab, transform.position, transform.rotation);
			DestroyObject (this.gameObject);
		} else {
			health -= damage;
			_animator.SetInteger ("HealthLevel", health*3/maxHealth);
		}
	}
}
