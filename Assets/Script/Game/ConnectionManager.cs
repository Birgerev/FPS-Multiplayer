using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ConnectionManager : NetworkManager {

    public static bool host = true;
    //public static bool singleplayer = false
    public static string map = "mp_sarra";
    public static string ip = "localhost";
    public static int port = 630;

    public bool connected = false;

    public static ConnectionManager instance;
    
    private void Start()
    {
        instance = this;

        DontDestroyOnLoad(gameObject);

        establishServerOrConnection();
    }
    
    private void establishServerOrConnection()
    {
        networkAddress = ip;
        networkPort = port;

        if (host)
        {
            StartHost();
        }
        else
        {
            StartClient();
        }
    }

    public void LeaveServer()
    {
        if (host)
            CloseServer();

        StopClient();
    }

    public void CloseServer()
    {
        StopHost();

        Destroy(gameObject);
    }

    public override void OnStartHost()
    {
        base.OnStartHost();

        if (host)
            ServerChangeScene(map);
    }

    public override void OnClientDisconnect(NetworkConnection conn)
    {
        base.OnClientDisconnect(conn);

        performDisconnect();
    }

    public override void OnStopClient()
    {
        base.OnStopClient();

        performDisconnect();
    }

    public override void OnStopServer()
    {
        base.OnStopServer();

        performDisconnect();
    }

    private void performDisconnect()
    {
        SceneManager.LoadScene("cl_menu");

        Destroy(gameObject);
    }
}
