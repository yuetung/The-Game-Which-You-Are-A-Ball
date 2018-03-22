using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using UnityEngine.Networking;

public class PlayerTest : MonoBehaviour {

	// A UnityTest behaves like a coroutine in PlayMode
	// and allows you to yield null to skip a frame in EditMode
	[UnityTest]
	public IEnumerator TestMoveTarget() {
		// Use the Assert class to test conditions.
		// yield to skip a frame

		GameObject gm = (GameObject)Instantiate(Resources.Load("Tests/gamemanager"));
		var playerPrefab = Resources.Load ("Tests/player");
		GameObject player = (GameObject)Instantiate(playerPrefab);

		Vector2 newPos = new Vector2 (2.0f, 2.0f);
//		player.GetComponent<PlayerController> ().moveTarget = newPos;
//		player.rigidbody = player.GetComponent<Rigidbody2D> ();

		NetworkServer.Listen (7777);

		yield return new WaitForSeconds(5);
		player.GetComponent<PlayerController> ().moveTarget = newPos;
		player.GetComponent<PlayerController> ().setMovementTarget(newPos);
		float endTime = Time.time + 2.0f;
		while (Time.time < endTime) {
			player.GetComponent<PlayerController> ().move ();
			yield return new WaitForEndOfFrame();
		}
		GameObject newplayer = GameObject.FindGameObjectWithTag("Player");
		float diff = (newPos - new Vector2(newplayer.transform.position.x, newplayer.transform.position.y)).magnitude;
		Assert.Less(diff, 0.1);

	}
		
}
