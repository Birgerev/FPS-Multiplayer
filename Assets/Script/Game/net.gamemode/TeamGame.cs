using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;

namespace net.bigdog.game.gamemode
{
    public class TeamGame : Gamemode
    {
        public static int localTeam;

        [SyncVar]
        public Team team1 = new Team("USA");
        [SyncVar]
        public Team team2 = new Team("Germany");
        
        public override void onStart()
        {
            base.onStart();
        }

        public Team getPlayerTeam(Player player)
        {
            if(team1.ContainsPlayer(player))
                return team1;
            if (team2.ContainsPlayer(player))
                return team2;

            return null;
        }
        
        public override void PlayerJoin(Player player)
        {
            base.PlayerJoin(player);

            if (isServer)
                joinRandomTeam(player);
        }

        public override void PlayerLeave(Player player)
        {
            base.PlayerLeave(player);

            if(isServer)
                getPlayerTeam(player).Leave(player);
        }

        private void joinRandomTeam(Player player)
        {
            Team team = team1;
            if(team1.players.Length >= team2.players.Length)
                team = team2;
            
            team.Join(player);
        }
    }

    [System.Serializable]
    public class Team
    {
        public string name = "Mexico";
    
        public Player[] players;

        public Team()
        {

        }

        public Team(string name)
        {
            this.name = name;

            players = new Player[0];
        }

        public bool ContainsPlayer(Player player)
        {
            foreach(Player i in players)
            {
                if (i.id == player.id)
                {
                    return true;
                }
            }

            return false;
        }

        public void Join(Player player)
        {
            List<Player> list = new List<Player>(players);

            list.Add(player);

            players = list.ToArray();

            if (player.id == game.player.PlayerInstance.localInstance.id)
                TeamGame.localTeam = (this == ((TeamGame)Gamemode.instance).team1) ? 1 : 2;
        }

        public void Leave(Player player)
        {
            List<Player> list = new List<Player>(players);

            if(list.Contains(player))
                list.Remove(player);

            players = list.ToArray();
        }
    }
}
