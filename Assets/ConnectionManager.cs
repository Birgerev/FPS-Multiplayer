using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ConnectionManager : MonoBehaviour {

    public static bool host = true;
    //public static bool singleplayer = false
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

    public void Initialize()
    {
        DontDestroyOnLoad(gameObject);

        if (host)
            network.ServerChangeScene(map);
    }
}
