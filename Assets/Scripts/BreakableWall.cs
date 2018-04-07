using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableWall : MonoBehaviour {

	public GameObject explosionPrefab;
	public int maxHealth = 10;
	public PlayerController.ElementType elementType;
	private int health;
	Animator _animator;
	public GameObject crystalPrefab;
	public int maxValue = 0;
	public int minValue = 0;
	public float dropRate = 1f;
	// Use this for initialization
	void Start () {
		health = maxHealth;
		_animator = gameObject.GetComponent<Animator> ();
		if (_animator)
			_animator.SetInteger ("HealthLevel", 3);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
		
	public void depleteHealth(int damage, PlayerController.ElementType element){
		if (elementType!=PlayerController.ElementType.Default && element != elementType)
			return;
		if (health - damage <= 0) {
			int value = Random.Range (minValue, maxValue);
			bool drop = Random.value <= dropRate;
			Debug.Log(value+ "Crystal");
			if (value > 0 && drop) { 
				GameObject crystal = Instantiate (crystalPrefab, transform.position, Quaternion.identity);
				crystal.GetComponent<Collectible> ().setValue (value);
				float scalingFactor = Mathf.Log (value)/6f;
				float scaled = scalingFactor;
				crystal.transform.localScale = new Vector3 (scaled, scaled, 0);
				//crystal.transform.localScale = new Vector3 (value / 50, value / 50, 1);
				Debug.Log("Crystal"+ crystal);
			}
			destroyNow ();
		} else {
			health -= damage;
			if (_animator)
				_animator.SetInteger ("HealthLevel", health*3/maxHealth);
		}
	}

	public void destroyNow(){
		Instantiate (explosionPrefab, transform.position, transform.rotation);
		DestroyObject (this.gameObject);
	}

}
