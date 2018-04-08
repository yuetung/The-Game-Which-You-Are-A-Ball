using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SetParent : NetworkBehaviour {
    [SyncVar]
    public NetworkInstanceId nId;
	[SyncVar]
	public bool rotateWithParent = false;
	private bool alreadyHasParent = false;
	GameObject parent;
    
    void Start()
    {
		if (nId != null) {
			parent = ClientScene.FindLocalObject (nId);
		}
    }

	void Update() {
		if (nId!=null) {
			if (rotateWithParent && !alreadyHasParent) {
				gameObject.transform.SetParent (parent.transform);
				alreadyHasParent = true;
			} else if (!rotateWithParent) {
				gameObject.transform.SetParent (null);
				transform.position = parent.transform.position;
			}
		}
	}

}
