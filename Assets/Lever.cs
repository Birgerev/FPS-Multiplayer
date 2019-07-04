using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : Interactable
{
    public override string popup_title { get; } = "Lever";
    public override string popup_text { get; } = "Press F To Turn Lever";

    public bool active = false;
    public Transform lever_stick;

    public Vector3 defaultRotation;
    public Vector3 activeRotation;

    // Start is called before the first frame update
    void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();

        lever_stick.localRotation = (active) ? Quaternion.Euler(activeRotation) : Quaternion.Euler(defaultRotation);
    }

    public override void Interact()
    {
        base.Interact();

        active = !active;
    }
}
