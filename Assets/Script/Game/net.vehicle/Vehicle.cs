using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;
using net.bigdog.game.player;

public class Vehicle : NetworkBehaviour
{
    public virtual int seat_amount { get; } = 1;

    public Transform seat;

    [SyncVar]
    public int passengerId;

    private bool lastFrameInteract;

    // Start is called before the first frame update
    public virtual void Start()
    {
        //Spawn vehicle on server
        if (isServer)
        {
            NetworkServer.Spawn(gameObject);
        }
    }

    // Update is called once per frame
    public virtual void Update()
    {
        if(passengerId != 0)
        {
            seatPlayer(passengerId);
            checkExit(PlayerInstance.byId(passengerId).player);
        }
    }

    private void checkExit(Player player)
    {
        if (player.networkInstance.input.interact && !lastFrameInteract)
        {
            Exit(player);
        }

        lastFrameInteract = player.networkInstance.input.interact;
    }

    private void seatPlayer(int passenger_id)
    {
        Player player = PlayerInstance.byId(passenger_id).player;

        player.controller.canMove = false;
        player.controller.canLook = true;
        player.controller.forceStand = true;
        player.controller.canCollide = false;

        player.transform.position = seat.position;
        player.transform.rotation = seat.rotation;
    }

    public void Exit(Player player)
    {
        if (hasAuthority)
        {
            passengerId = 0;
        }

        player.controller.canMove = true;
        player.controller.canLook = true;
        player.controller.forceStand = false;
        player.controller.canCollide = true;
    }

    public void Enter(Player player, int seatId)
    {
        if (hasAuthority)
        {
            passengerId = player.networkInstance.id;
        }
    }
}
