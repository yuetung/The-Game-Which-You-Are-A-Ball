using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class WaitFor2P : NetworkBehaviour {
    NetworkManager_Custom nw;
    public int playerCount = 0;
    private bool start = false;

	void Start () {
        nw = GameObject.FindGameObjectWithTag("NetworkManager").GetComponent<NetworkManager_Custom>();
    }
	
	void Update () {
        //Debug.Log(NetworkServer.connections.Count);
        if(NetworkServer.connections.Count >= 2 && !start)
        {
            Debug.Log("Starting in 3");
            start = true;
            var WaitingTextBox = GameObject.FindGameObjectWithTag("MainCanvas").transform.Find("WaitingText");
            if (WaitingTextBox != null)
            {
                WaitingTextBox.GetComponent<UnityEngine.UI.Text>().text = "Starting game...";
            }
            else
            {
                Debug.Log("No IP Text");
            }
            StartCoroutine(StartGame(3));
        }
    }

    void OnPlayerConnected(NetworkPlayer player)
    {
        playerCount++;
        Debug.Log("Player " + playerCount + " connected");
        if(playerCount >= 2)
        {
            nw.ServerChangeScene("Multiplayer_Scene");
        }
    }
    
    [Server]
    IEnumerator StartGame(float waitTime)
    {
        Debug.Log("coroutine started");
        yield return new WaitForSeconds(waitTime);
        nw.ServerChangeScene("Multiplayer_Scene");
    }

}
