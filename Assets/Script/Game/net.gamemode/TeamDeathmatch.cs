using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace net.bigdog.game.gamemode
{
    public class TeamDeathmatch : TeamGame
    {
        public int maxPoints = 100;

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

        public override void OnPlayerKilled(Player victim, Player killer)
        {
            base.OnPlayerKilled(victim, killer);

            if (isServer)
            {
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