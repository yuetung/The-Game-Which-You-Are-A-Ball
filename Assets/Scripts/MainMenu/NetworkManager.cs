using System.Collections;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine;

public class NetworkManager_Custom : NetworkManager {

	public void StartupHost() {
		SetPort();
		NetworkManager.singleton.StartHost ();

	}
	public void joinGame(){
		SetIPAddress ();
		SetPort ();
		NetworkManager.singleton.StartClient ();
	}
	void SetIPAddress(){
		string ipAddress=GameObject.Find("InputFieldIPAddress").transform.Find("Text").GetComponent<Text>().text;
		NetworkManager.singleton.networkAddress = ipAddress;
	}
	void SetPort(){
		NetworkManager.singleton.networkPort = 7777;
	}

}

