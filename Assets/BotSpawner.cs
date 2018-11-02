using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;

public class BotSpawner : NetworkBehaviour {

    public GameObject prefab;

	// Use this for initialization
	void Start () {
        if (isServer)
        {
            GameObject obj = Instantiate(prefab);

            NetworkServer.Spawn(obj);
        }
	}
}
