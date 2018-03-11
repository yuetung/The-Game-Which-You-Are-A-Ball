using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyClamp : MonoBehaviour {

	public Transform RectBox;

	// Update is called once per frame
	void Update () {
		Vector3 RectBoxPos = Camera.main.WorldToScreenPoint (this.transform.position);
		RectBox.transform.position = RectBoxPos;
	}
}
