using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;

public class PlayerInstance : NetworkBehaviour
{
    public static PlayerInstance localInstance;

    public const int TickRate = 40;

    public GameObject playerPrefab;
    public GameObject spawnMenuPrefab;

    public Player player;

    [SyncVar]
    public float health = 0;

    [SyncVar]
    public bool spawned = false;
    public bool client_spawned = false;
    [SyncVar]
    public Vector3 spawnPosition;

    [SyncVar]
    public bool limitPitch = false;

    [SyncVar]
    public PlayerInstanceInput.InputData input;
    
    //[SyncVar]
    //public InventoryData inventoryData;

    // Start is called before the first frame update
    void Start()
    {
        //Spawn on Game Start

        StartCoroutine(tick());

        if (isLocalPlayer)
            localInstance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null && client_spawned)
        {
            client_spawned = false;
            if (isServer)
                spawned = false;
        }

        if (!client_spawned && spawned)
        {
            Client_Spawn();

            client_spawned = true;
        }

        if (isLocalPlayer)
        {
            if (player == null)
                if(FindObjectOfType<SpawnMenu>() == null)
                    Instantiate(spawnMenuPrefab);
        }

        //Always syncing local values, so that local players get a smooth experience
        /*if(isLocalPlayer && !isServer)
        {
            input = GetComponent<PlayerInstanceInput>().input;
        }*/
    }

    IEnumerator tick()
    {
        if (!isLocalPlayer)
            yield break;

        while (true)
        {
            yield return new WaitForSeconds(0.05f);
            PlayerInstanceInput inputData = GetComponent<PlayerInstanceInput>();

            CmdSyncInput(inputData.input);
        }
    }

    IEnumerator syncPositionLoop()
    {
        if (!isLocalPlayer)
            yield break;

        while (true)
        {
            yield return new WaitForSeconds(1);
            CmdSyncPosition(player.transform.position);
        }
    }

    #region Net Functions
    //Sync Position
    [Command]
    public void CmdSyncPosition(Vector3 position)
    {
        //Vector3 position = player.transform.position;

        RpcSyncPosition(position);
    }

    [ClientRpc]
    public void RpcSyncPosition(Vector3 pos)
    {
        player.transform.position = pos;
    }

    //Spawn
    [Command]
    public void CmdSpawn(Vector3 position)
    {
        spawnPosition = position;
        spawned = true;
    }
    
    public void Client_Spawn()
    {
        print("client spawn");
        GameObject obj = Instantiate(playerPrefab);

        player = obj.GetComponent<Player>();

        player.networkInstance = this;

        obj.transform.position = spawnPosition;

        print("client spawn done");

        StartCoroutine(syncPositionLoop());
    }
    
    /*[Command]
    public void CmdUpdateInventoryData(InventoryData data)
    {
        inventoryData = data;
    }*/

    [Command]
    public void CmdSyncInput(PlayerInstanceInput.InputData inputData)
    {
        input = inputData;
    }
    
    #endregion
}
