using System.Collections;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine;
using UnityEngine.Networking.Match;
using System.Collections.Generic;

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

    //==========NetworkMatch==========
    //this method is called when your request for creating a match is returned

    public void CreateInternetMatch()
    {
        string matchName = GameObject.Find("InputFieldIPAddress").transform.Find("Text").GetComponent<Text>().text;
        var mm = NetworkManager.singleton.matchMaker;
        mm.ToString();
        mm.CreateMatch(matchName, 4, true, "", "", "", 0, 0, OnInternetMatchCreate);
    }

    private void OnInternetMatchCreate(bool success, string extendedInfo, MatchInfo matchInfo)
    {
        if (success)
        {
            //Debug.Log("Create match succeeded");

            MatchInfo hostInfo = matchInfo;
            NetworkServer.Listen(hostInfo, 9000);

            NetworkManager.singleton.StartHost(hostInfo);
        }
        else
        {
            Debug.LogError("Create match failed");
        }
    }

    //call this method to find a match through the matchmaker
    public void FindInternetMatch()
    {
        string matchName = GameObject.Find("InputFieldIPAddress").transform.Find("Text").GetComponent<Text>().text;
        NetworkManager.singleton.matchMaker.ListMatches(0, 10, matchName, true, 0, 0, OnInternetMatchList);
    }

    //this method is called when a list of matches is returned
    private void OnInternetMatchList(bool success, string extendedInfo, List<MatchInfoSnapshot> matches)
    {
        if (success)
        {
            if (matches.Count != 0)
            {
                //Debug.Log("A list of matches was returned");

                //join the last server (just in case there are two...)
                NetworkManager.singleton.matchMaker.JoinMatch(matches[matches.Count - 1].networkId, "", "", "", 0, 0, OnJoinInternetMatch);
            }
            else
            {
                Debug.Log("No matches in requested room!");
            }
        }
        else
        {
            Debug.LogError("Couldn't connect to match maker");
        }
    }

    //this method is called when your request to join a match is returned
    private void OnJoinInternetMatch(bool success, string extendedInfo, MatchInfo matchInfo)
    {
        if (success)
        {
            //Debug.Log("Able to join a match");

            MatchInfo hostInfo = matchInfo;
            NetworkManager.singleton.StartClient(hostInfo);
        }
        else
        {
            Debug.LogError("Join match failed");
        }
    }


}

