using UnityEngine;
using UnityEngine.Networking;
using System.Collections.Generic;

public class AOEExplosion : MonoBehaviour {

	public PlayerController.ElementType elementType = PlayerController.ElementType.Default;

	[Tooltip("Deal this damage once to all enemies within the area")]
	public int AOEDamage = 1;

	[Tooltip("how many seconds before destroying object when hit, leave just enought to play animation")]
	public float explodeAnimationSeconds = 1.0f;

	public bool belongToPlayer= false;

	private List<GameObject> alreadyHit = new List<GameObject>();

	public GameObject shooter;

	// Collision with wall
	void OnTriggerEnter2D(Collider2D other) {
		if (other.tag == "BreakableWall" && !alreadyHit.Contains(other.gameObject)) {
			Debug.Log("Hit box");
			other.GetComponent<BreakableWall> ().depleteHealth (AOEDamage,elementType);
			alreadyHit.Add (other.gameObject);
		}
		else if (other.tag == "Enemy" && belongToPlayer && !alreadyHit.Contains(other.gameObject)) {
			other.GetComponent<Enemy> ().depleteHealth (AOEDamage, elementType);
			alreadyHit.Add (other.gameObject);
		}

		else if (shooter != null && other.gameObject == shooter.gameObject)
		{
			Debug.Log("Hit self");
			//do nothing
		}

		else if (other.tag == "Player" && !alreadyHit.Contains(other.gameObject))
		{
			other.GetComponent<PlayerController> ().depleteHealth (AOEDamage);
			alreadyHit.Add (other.gameObject);
		}
	}

	public int getProjectileDamage() {
		return AOEDamage;
	}

	public void belongsToPlayer() {
		belongToPlayer = true;
	}

	public bool getBelongsToPlayer() {
		return belongToPlayer;
	}

	public void setShooter(GameObject shooter){
		this.shooter = shooter;
	}
}
