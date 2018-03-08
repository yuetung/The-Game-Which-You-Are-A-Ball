using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SinglePlayerNetwork : NetworkManager
{
    public GameObject player1;

    void Start()
    {
        StartHost();
    }

    public override void OnClientConnect(NetworkConnection conn)
    {
        ClientScene.Ready(conn);
        ClientScene.AddPlayer(0);
    }

    public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId)
    {
        GameObject player = Instantiate(player1);
        player.transform.position = new Vector3();

        NetworkServer.AddPlayerForConnection(conn, player, playerControllerId);
    }
}
