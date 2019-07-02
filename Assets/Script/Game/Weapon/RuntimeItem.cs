using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using net.bigdog.game.player.camera;
using net.bigdog.game.player;

public class RuntimeItem : MonoBehaviour
{
    public Item item;               // Stats for weapon

    public bool aiming = false;
    protected bool mouse = false;
    protected bool mouseDown = false;
    protected bool mouseUp = false;
    protected bool lastFrameReload = false;

    private bool lastFrameMouse = false;

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
        lastFrameMouse = mouse;
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
        mouseDown = (mouse && !lastFrameMouse);
        mouseUp = (!mouse && lastFrameMouse);

        if (input.reload && !lastFrameReload)
        {
            reload();
        }

        lastFrameReload = input.reload;

    }

    public virtual void Aim(bool aim)
    {
        GetComponent<Player>().cameraController.aiming = aim;
        PlayerInstanceInput.aimingSensitivity = item.weaponData.aimFoV / CameraController.normalFoV;

        this.aiming = aim;
    }

    public virtual void reload()
    {
        //lets weapon override this function
    }
}
