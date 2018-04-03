using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Collectible : MonoBehaviour {
	[Tooltip("explosion particle effect")]
	public GameObject explosion;
	private int value;
	public GameObject notificationPrefab;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	// Collision with player
	void OnTriggerEnter2D(Collider2D other) {
		if (other.tag == "Player") {
			int currentGold = GameManager.getGold ();
			GameManager.setGold (currentGold + value);
			GameObject notification = Instantiate (notificationPrefab, transform.position, transform.rotation);
			notification.transform.SetParent (GameObject.FindGameObjectWithTag ("MainCanvas").transform, false);

			//notification.transform.position = new Vector3 (transform.position.x, transform.position.y + 0.5f, transform.position.z);

			Vector3 TransformedPos = new Vector3(0.65f, transform.localScale.y+0.5f, 0f);
			Vector3 RectBoxPos = Camera.main.WorldToScreenPoint(transform.position + TransformedPos);
			notification.transform.position = RectBoxPos;

			notification.GetComponentInChildren<Text> ().text = "+" + value;

			if (explosion) {
				Instantiate (explosion, transform.position, transform.rotation);
			}
			DestroyObject (this.gameObject);
		}

	}
	public void setValue(int value){
		this.value = value;
	}
}
	