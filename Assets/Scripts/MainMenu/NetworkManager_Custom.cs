using System.Collections;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine;
using UnityEngine.Networking.Match;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class NetworkManager_Custom : NetworkManager
{

    public GameObject player1;
    private static bool singlePlayer = false;

    public void Start()
    {
        //StartCoroutine(UpdateMatches());
        StartCoroutine(
        UpdateSpawnPoint());
    }

    IEnumerator UpdateSpawnPoint()
    {
        while (true)
        {
            yield return new WaitForSeconds(3);
            GameObject[] spawnPoints = GameObject.FindGameObjectsWithTag("SpawnPoint");
            foreach (GameObject spawnPoint in spawnPoints)
            {
                //NetworkManager.RegisterStartPosition(spawnPoint.transform);
                Debug.Log(spawnPoint.GetComponent<NetworkStartPosition>().isActiveAndEnabled);
            }
            //Debug.Log(singleton.startPositions.Count);
            Debug.Log(singleton.playerSpawnMethod);
            
        }
    }
    public void StartupHost()
    {
        Debug.Log("startupHost");
        SetPort();
        NetworkManager.singleton.StartHost();
    }
    public void joinGame()
    {
        Debug.Log("joinGame");
        SetIPAddress();
        SetPort();
        NetworkManager.singleton.StartClient();
    }
    void SetIPAddress()
    {
        string ipAddress = GameObject.Find("InputFieldIPAddress").transform.Find("Text").GetComponent<Text>().text;
        NetworkManager.singleton.networkAddress = ipAddress;
        Debug.Log("IP Address is " + ipAddress + ", length: " + ipAddress.Length);
    }
    void SetPort()
    {
        NetworkManager.singleton.networkPort = 7777;
    }

    public static void StartSinglePlayer()
    {
        Debug.Log("Start");
        singleton.StartHost();
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

    //public void StartMM()
    //{
    //    StartMatchMaker();
    //}

    public void FindOrStartInternetMatch()
    {
        FindInternetMatch2();
    }
    public void FindInternetMatch2()
    {
        string matchName = "";
        NetworkManager.singleton.matchMaker.ListMatches(0, 10, matchName, true, 0, 0, OnInternetMatchList2);
    }
    private void OnInternetMatchList2(bool success, string extendedInfo, List<MatchInfoSnapshot> matches)
    {
        if (success)
        {
            if (matches.Count != 0)
            {
                //Debug.Log("A list of matches was returned");
                GameObject.FindGameObjectWithTag("MainCanvas").transform.Find("Background").Find("Network Menu").Find("JoiningGame").gameObject.SetActive(true);
                GameObject.FindGameObjectWithTag("MainCanvas").transform.Find("Background").Find("Network Menu").Find("StartHost").gameObject.SetActive(false);
                //join the last server (just in case there are two...)
                NetworkManager.singleton.matchMaker.JoinMatch(matches[matches.Count - 1].networkId, "", "", "", 0, 0, OnJoinInternetMatch);
            }
            else
            {
                CreateInternetMatch();
            }
        }
        else
        {
            Debug.LogError("Couldn't connect to match maker");
        }
    }

    public void CreateInternetMatch()
    {
        //string matchName = GameObject.Find("InputFieldIPAddress").transform.Find("Text").GetComponent<Text>().text;
        GameObject.FindGameObjectWithTag("MainCanvas").transform.Find("Background").Find("Network Menu").Find("CreatingGame").gameObject.SetActive(true);
        GameObject.FindGameObjectWithTag("MainCanvas").transform.Find("Background").Find("Network Menu").Find("StartHost").gameObject.SetActive(false);
        string matchName = "";
        var mm = NetworkManager.singleton.matchMaker;
        UpdateSpawnPoint();
        mm.ToString();
        mm.CreateMatch(matchName, 2, true, "", "", "", 0, 0, OnInternetMatchCreate);
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


    //this method is called when your request to join a match is returned
    private void OnJoinInternetMatch(bool success, string extendedInfo, MatchInfo matchInfo)
    {
        if (success)
        {
            //Debug.Log("Able to join a match");
            
            MatchInfo hostInfo = matchInfo;
            NetworkManager.singleton.StartClient(hostInfo);
            StartCoroutine(JoinGameChecker());
        }
        else
        {
            Debug.LogError("Join match failed");
        }
    }

    public void OnFailedToConnect(NetworkConnectionError error)
    {
        Debug.Log("Failed to connect");
    }

    public override void OnServerDisconnect(NetworkConnection conn)
    {
        base.OnServerDisconnect(conn);
        PauseMenuList temp = new PauseMenuList();
        temp.MainMenu();
    }

    private IEnumerator JoinGameChecker()
    {
        yield return new WaitForSeconds(5);
        if (!clientLoadedScene)
        {
            StopClient();
            StopServer();

            CreateInternetMatch();

            //ServerChangeScene("Menu_Scene");
            //SceneManager.LoadScene(0);
            Debug.LogError("Restarting client");
        }
        else
        {
            Debug.Log("Client loaded scene!");
        }
    }

    IEnumerator UpdateMatches()
    {
        while (networkSceneName == "Menu_Scene")
        {
            yield return new WaitForSeconds(3);
            NetworkManager.singleton.matchMaker.ListMatches(0, 10, matchName, true, 0, 0, OnInternetMatchList3);
        }
    }

    private void OnInternetMatchList3(bool success, string extendedInfo, List<MatchInfoSnapshot> matches)
    {
        if (success)
        {
            Debug.Log(matches.Count);
            this.matches = matches;
            if (matches.Count != 0)
            {
                Debug.Log("Matches!!");
                GameObject.FindGameObjectWithTag("MainCanvas").transform.Find("Background").Find("Network Menu").Find("JoinGame").gameObject.SetActive(true);
                GameObject.FindGameObjectWithTag("MainCanvas").transform.Find("Background").Find("Network Menu").Find("StartHost").gameObject.SetActive(false);
                // Change game button to join game
            }
            else
            {
                // change game button to start game
                GameObject.FindGameObjectWithTag("MainCanvas").transform.Find("Background").Find("Network Menu").Find("JoinGame").gameObject.SetActive(false);
                GameObject.FindGameObjectWithTag("MainCanvas").transform.Find("Background").Find("Network Menu").Find("StartHost").gameObject.SetActive(true);
            }
        }
        else
        {
            Debug.LogError("Couldn't connect to match maker");
        }
    }

    public void JoinGame()
    {
        NetworkManager.singleton.matchMaker.JoinMatch(matches[matches.Count - 1].networkId, "", "", "", 0, 0, OnJoinInternetMatch);
    }

}

