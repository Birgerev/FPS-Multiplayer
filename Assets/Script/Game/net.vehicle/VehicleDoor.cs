using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleDoor : Interactable
{
    public override string popup_title { get; } = "Car";
    public override string popup_text { get; } = "Press F To Enter";

    public int seatId = 0;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
    }

    public override void Interact()
    {
        base.Interact();

        GetComponentInParent<Vehicle>().Enter(triggered_player, seatId);
    }
}