using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;

namespace net.bigdog.game.gamemode
{
    public class Gamemode : NetworkBehaviour
    {
        public static Gamemode instance;

        public List<PlayerStats> stats = new List<PlayerStats>();


        public virtual void Start()
        {
            instance = this;

            if (isServer)
                Rpc_onStart();
        }

        [ClientRpc]
        public void Rpc_onStart()
        {
            onStart();
        }

        public virtual void onStart()
        {

        }

        [ClientRpc]
        public void Rpc_OnPlayerKilled(Player victim, Player killer)
        {
            OnPlayerKilled(victim, killer);
        }

        public virtual void OnPlayerKilled(Player victim, Player killer)
        {

        }

        [ClientRpc]
        public void Rpc_OnPlayerDamage(Player victim, Player attacker)
        {
            OnPlayerDamage(victim, attacker);
        }

        public virtual void OnPlayerDamage(Player victim, Player killer)
        {

        }

        [ClientRpc]
        public void Rpc_End()
        {
            End();
        }

        public virtual void End()
        {

        }

        [ClientRpc]
        public void Rpc_PlayerJoin(Player player)
        {
            PlayerJoin(player);
        }

        public virtual void PlayerJoin(Player player)
        {
            if (isServer)
                stats.Add(new PlayerStats(player));
        }

        [ClientRpc]
        public void Rpc_PlayerLeave(Player player)
        {
            PlayerLeave(player);
        }

        public virtual void PlayerLeave(Player player)
        {
            if (isServer)
                stats.Remove(new PlayerStats(player));
        }

        public virtual void Win()
        {

        }

        public virtual void Lose()
        {

        }
    }

    [System.Serializable]
    public class PlayerStats
    {
        public Player player;

        public int kills;
        public int deaths;
        public int score;

        public PlayerStats()
        {

            Reset();
        }

        public PlayerStats(Player player)
        {
            this.player = player;

            Reset();
        }

        public void Reset()
        {
            kills = 0;
            deaths = 0;
            score = 0;
        }
    }
}