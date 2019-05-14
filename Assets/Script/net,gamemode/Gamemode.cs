using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;

public class Gamemode : NetworkBehaviour
{
    public virtual void Start()
    {
        if (isServer)
            Rpc_onStart();
    }

    [ClientRpc]
    public virtual void Rpc_onStart()
    {
        new Player("");
    }

    [ClientRpc]
    public virtual void Rpc_OnPlayerKilled(int victim, int killer)
    {

    }

    [ClientRpc]
    public virtual void Rpc_OnPlayerDamage(int victim, int attacker)
    {

    }

    [ClientRpc]
    public virtual void Rpc_End()
    {

    }

    public virtual void Win()
    {

    }

    public virtual void Lose()
    {

    }
}

class Player
{
    public string name = "Mexico";

    public List<int> players = new List<int>();


    public GamePlayer(string name)
    {
        this.name = name;
    }
}