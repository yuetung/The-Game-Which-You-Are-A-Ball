using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public static GameManager gm;

	// Use this for initialization
	void Start () {
		if (gm == null)
			gm = this.gameObject.GetComponent<GameManager> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
