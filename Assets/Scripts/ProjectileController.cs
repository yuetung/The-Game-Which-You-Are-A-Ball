using UnityEngine;

public class ProjectileController : MonoBehaviour {
	
	[Tooltip("true projectile speed = player base speed * projectile speed")]
	public float projectileSpeed = 1.0f;

	[Tooltip("true damage = player base power * projectile damage")]
	public float projectileDamage = 1.0f;

	[Tooltip("how many seconds before destroying object when hit, leave just enought to play animation")]
	public float explodeAnimationSeconds = 1.0f;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	// Collision with wall
	void OnTriggerEnter2D(Collider2D other) {
		if (other.tag == "Wall") {
			gameObject.GetComponent<Rigidbody2D> ().velocity = Vector2.zero;
			gameObject.GetComponent<Animator> ().SetTrigger ("Explode");
			Invoke ("DestroyNow", explodeAnimationSeconds);
		}
	}

	void DestroyNow() {
		DestroyObject (this.gameObject);
	}
}
