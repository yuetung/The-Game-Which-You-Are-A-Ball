using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SetParent : NetworkBehaviour {
    [SyncVar]
    public NetworkInstanceId nId;
    
    void Start()
    {
        if (nId != null)
        {
            GameObject parent = ClientScene.FindLocalObject(nId);
            transform.SetParent(parent.transform, false);
        } 
        else
        {
            Debug.Log("No parent");
        }
    }
}
