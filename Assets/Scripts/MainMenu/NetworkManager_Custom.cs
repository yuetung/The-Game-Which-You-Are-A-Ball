using System.Collections;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine;

public class NetworkManager_Custom : NetworkManager {

    public GameObject player1;
    private static bool singlePlayer = false;

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

    public static void StartSinglePlayer()
    {
        Debug.Log("Start");
        NetworkManager.singleton.StartHost();
        singlePlayer = true;
    }

    public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId)
    {
        if (singlePlayer)
        {
            Debug.Log("OnServerAddPlayer");
            GameObject player = Instantiate(player1);
            player.transform.position = new Vector3();
            NetworkServer.AddPlayerForConnection(conn, player, playerControllerId);
            //GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera2DFollow>().followPlayer(player);
        }
        else
        {
            base.OnServerAddPlayer(conn, playerControllerId);
        }
    }


}

