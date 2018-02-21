using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	// static reference to game manager so can be called from other scripts directly (not just through gameobject component)
	public static GameManager gm;

	// set things up here
	void Awake () {
		// setup reference to game manager
		if (gm == null)
			gm = this.GetComponent<GameManager>();
	}

	// game loop
	void Update() {
		
	}
}
