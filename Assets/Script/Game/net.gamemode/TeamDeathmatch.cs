using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace net.bigdog.game.gamemode
{
    public class TeamDeathmatch : TeamGame
    {
        public int maxPoints = 25;

        [SyncVar]
        public int team1Points;
        [SyncVar]
        public int team2Points;

        public override void Start()
        {
            base.Start();

            if (isServer)
            {
                team1Points = maxPoints;
                team2Points = maxPoints;
            }
        }

        public override void Update()
        {
            base.Update();

            //Check Win
            if (team1Points <= 0 || team2Points <= 0 && isServer)
                Rpc_End();
        }

        public override void OnPlayerKilled(Player victim, Player killer)
        {
            base.OnPlayerKilled(victim, killer);

            if (isServer)
            {
                print("errorbug lol");

                if (getPlayerTeam(victim) == team1)
                    team1Points--;
                else
                    team2Points--;
            }
        }

        public override void OnPlayerDamage(Player victim, Player attacker)
        {
            base.OnPlayerDamage(victim, attacker);

            print("damaged");
        }

    }
}