using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


// not used anymore, merged this code into NetworkManager_Custom.cs
public class SinglePlayerNetwork : NetworkManager
{
    public GameObject player1;

    void StartSinglePlayer()
    {
        Debug.Log("Start");
        StartHost();
    }

    public override void OnClientConnect(NetworkConnection conn)
    {
        Debug.Log("OnClientConnect");
        ClientScene.Ready(conn);
        ClientScene.AddPlayer(0);
    }

    public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId)
    {
        Debug.Log("OnServerAddPlayer");
        GameObject player = Instantiate(player1);
        player.transform.position = new Vector3();
        NetworkServer.AddPlayerForConnection(conn, player, playerControllerId);
		GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<Camera2DFollow> ().followPlayer (player);
    }
}
