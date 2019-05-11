using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ConnectionManager : NetworkBehaviour {

    public static bool host = true;
    //public static bool singleplayer = false
    public string synced_map = "mp_Test";
    public static string map = "mp_Test";
    public static string ip = "localhost";
    public static int port = 630;

    private NetworkManager network;

    public bool connected = false;
    
    private void Start()
    {
        network = GetComponent<NetworkManager>();

        connect();

        StartCoroutine(later());
        
    }

    IEnumerator later()
    {
        yield return new WaitForSeconds(1f);

        Initialize();
    }

    private void connect()
    {
        network.networkAddress = ip;
        network.networkPort = port;

        if (host)
        {
            network.StartHost();
        }
        else
        {
            network.StartClient();
        }
    }

    private void Update()
    {
        if (NetworkClient.active && !ClientScene.ready)
        {
        }
    }

    public void Initialize()
    {
        if (isServer)
            synced_map = map;
        else
            map = synced_map;

        DontDestroyOnLoad(gameObject);
        SceneManager.LoadScene(map, LoadSceneMode.Single);

    }
}
