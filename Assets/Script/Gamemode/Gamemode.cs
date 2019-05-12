using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;

public class Gamemode : NetworkBehaviour
{
    public virtual void Start()
    {

    }

    [ClientRpc]
    public virtual void Rpc_OnPlayerKilled(PlayerInstance victim, PlayerInstance killer)
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
