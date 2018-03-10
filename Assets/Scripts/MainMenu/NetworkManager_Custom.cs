using System.Collections;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine;

public class NetworkManager_Custom : NetworkManager {

	public void StartupHost() {
        Debug.Log("startupHost");
        SetPort();
		NetworkManager.singleton.StartHost();
	}
	public void joinGame(){
        Debug.Log("joinGame");
		SetIPAddress ();
		SetPort ();
		NetworkManager.singleton.StartClient();
    }
	void SetIPAddress(){
		string ipAddress=GameObject.Find("InputFieldIPAddress").transform.Find("Text").GetComponent<Text>().text;
		NetworkManager.singleton.networkAddress = ipAddress;
        Debug.Log("IP Address is " + ipAddress + ", length: " + ipAddress.Length);
	}
	void SetPort(){
		NetworkManager.singleton.networkPort = 7777;
	}



}

