using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyNotice : MonoBehaviour {

	private GameObject parent;
	private Vector3 offset;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (parent) {
			transform.position = parent.transform.position + offset;
		}
	}

	public void setParentOffset(GameObject parentObject, Vector3 parentOffset){
		this.parent = parentObject;
		this.offset = parentOffset;
	}
}
