using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuntimeItem : MonoBehaviour
{
    public Item item;               // Stats for weapon

    public bool aiming = false;
    public bool mouse = false;
    public bool mouseDown = false;
    public bool mouseUp = false;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public virtual void Update()
    {
        input();
    }

    public void LateUpdate()
    {
        //since these variables should only be set to true one frame at a time, we mark them as false
        mouseDown = false;
        mouseUp = false;
    }

    public void input()
    {
        if (GetComponent<Player>().networkInstance == null)
            return;

        PlayerInstanceInput.InputData input = GetComponent<Player>().networkInstance.input;

        if (input.aim)
            Aim(true);
        if (!input.aim)
            Aim(false);

        mouse = input.shoot;

        if (input.reload)
        {
            reload();
        }
    }

    public virtual void Aim(bool aim)
    {
        CameraController.aiming = aim;

        this.aiming = aim;
    }

    public virtual void reload()
    {
        //lets weapon override this function
    }
}
