using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;

namespace net.bigdog.game.gamemode
{
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
            new Player(1);
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
}