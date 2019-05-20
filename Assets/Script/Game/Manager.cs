using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;

public class Manager : MonoBehaviour {

    public GameObject gamemode;

    private void Start()
    {
        Invoke("CreateGamemode", 4);
    }

    public void CreateGamemode()
    {
        if (ConnectionManager.host)
        {
            NetworkManager network = GetComponent<NetworkManager>();

            GameObject obj = Instantiate(gamemode);

            //ClientScene.RegisterPrefab();
            NetworkServer.Spawn(obj);
        }
    }

}
