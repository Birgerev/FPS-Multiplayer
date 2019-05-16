using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace net.bigdog.game.gamemode
{
    [System.Serializable]
    public class Player
    {
        public int id = 0;

        public Player()
        {

        }

        public Player(int id)
        {
            this.id = id;
        }

        public Player(net.bigdog.game.player.PlayerInstance player)
        {
            this.id = player.id;
        }

        public net.bigdog.game.player.PlayerInstance playerInstance()
        {
            return net.bigdog.game.player.PlayerInstance.byId(id);
        }
    }
}
