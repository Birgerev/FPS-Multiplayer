using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;

namespace net.bigdog.game.player
{
    public class PlayerInstance : NetworkBehaviour
    {
        public static PlayerInstance localInstance;

        public const int TickRate = 60;

        public GameObject playerPrefab;
        public GameObject spawnMenuPrefab;

        public Player player;

        [SyncVar]
        public GameProfile profile;

        [SyncVar]
        public float health = 0;

        [SyncVar]
        public bool spawned = false;
        public bool client_spawned = false;
        [SyncVar]
        public Vector3 spawnPosition;

        [SyncVar]
        public bool limitPitch = false;

        public PlayerInstanceInput.InputData input;

        [SyncVar]
        public int id;

        //[SyncVar]
        //public InventoryData inventoryData;

        // Start is called before the first frame update
        void Start()
        {
            //Spawn on Game Start
            DontDestroyOnLoad(gameObject);

            StartCoroutine(tick());

            if (isLocalPlayer)
            {
                localInstance = this;
                Cmd_InformServerOfProfileToken(MenuManager.instance.playerProfile.gameToken);
            }

            if(isServer)
            {
                id = (int)Random.Range(1, 999);

                Invoke("joinEvent", 4);
            }
        }

        private void joinEvent()
        {
            gamemode.Gamemode.instance.Rpc_PlayerJoin(
                new gamemode.Player(id));
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

            //Always syncing local values, so that local players get a smooth experience
            /*if(isLocalPlayer && !isServer)
            {
                input = GetComponent<PlayerInstanceInput>().input;
            }*/


            if (isLocalPlayer)
                if(player == null)
                    if(FindObjectOfType<SpawnMenu>() == null)
                        if(FindObjectOfType<DeathScreen>() == null)
                            OpenSpawnMenu();
        }

        IEnumerator tick()
        {
            if (!(isLocalPlayer || isServer))
                yield break;

            while (true)
            {
                yield return new WaitForSeconds(1/TickRate);

                if (isLocalPlayer)
                { //Sync local input to server
                    PlayerInstanceInput inputData = GetComponent<PlayerInstanceInput>();

                    CmdSyncInput(inputData.input);
                }
            }
        }

        IEnumerator syncPositionLoop()
        {
            if (!isLocalPlayer)
                yield break;

            while (true)
            {
                yield return new WaitForSeconds(4);
                if(player != null)
                    CmdSyncPosition(player.transform.position);
            }
        }

        /// <summary>
        /// Get a PlayerInstance class object by the id's shared globally
        /// </summary>
        public static PlayerInstance byId(int id)
        {
            PlayerInstance result = null;

            foreach (PlayerInstance instance in FindObjectsOfType<PlayerInstance>())
            {
                if (instance.id == id)
                    result = instance;
            }

            if (result == null)
                Debug.Log("An unsuccesful 'PlayerInstance.byID()' search was carried out, failed id was '"+id+"'.");

            return result;
        }

        public void OpenSpawnMenu()
        {
            if (player == null)
                if (FindObjectOfType<SpawnMenu>() == null)
                    Instantiate(spawnMenuPrefab);
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

        [Command]
        public void Cmd_InformServerOfProfileToken(string token)
        {
            if (!Database.VerifyToken(token))
                Debug.LogError("Invalid Token");

            this.profile = new GameProfile(Database.GetIdFromToken(token));
        }

        //Spawn
        [Command]
        public void CmdSpawn(Vector3 position)
        {
            //return if spawnpoint wasn't succesfully verified
            if (!verifySpawnpoint(position))
                return;

            spawnPosition = position;
            spawned = true;
        }

        private bool verifySpawnpoint(Vector3 pos)
        {
            //Get all spawnpoints in scene
            Spawnpoint[] spawnpoints = FindObjectsOfType<Spawnpoint>();

            //Find the distance to the closest spawnpoint to the given position
            int closestDistance = 10000;
            foreach (Spawnpoint spawnpoint in spawnpoints)
            {
                int distance = (int)Vector3.Distance(pos, spawnpoint.transform.position);

                if (distance < closestDistance)
                    closestDistance = distance;
            }

            //verified if distance is less than 10 meters
            return (closestDistance < 10);
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

            RpcSyncInput(inputData);
        }

        [ClientRpc]
        public void RpcSyncInput(PlayerInstanceInput.InputData inputData)
        {
            input = inputData;
        }

        #endregion
    }
}