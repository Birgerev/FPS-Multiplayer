using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;
using net.bigdog.game.player;

public class Vehicle : NetworkBehaviour
{
    public virtual int seat_amount { get; } = 1;

    [SyncVar]
    public int passangerId;

    // Start is called before the first frame update
    public virtual void Start()
    {
    }

    // Update is called once per frame
    public virtual void Update()
    {
        
    }

    public void Enter(Player player, int seatId)
    {
        if (isLocalPlayer)
        {
            passangerId = player.networkInstance.id;
        }
    }
}
