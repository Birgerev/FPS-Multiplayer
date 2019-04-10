using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;

public class RuntimeWeapon : MonoBehaviour {

    public Weapon weapon;               // Stats for weapon

    private bool _initialized = false;  // Is the Initialize() funtion called yet? if not, show errors

    public bool aiming = false;
    private bool mouse = false;
    private bool mouseDown = false;
    private bool mouseUp = false;

    public bool reloading;

    public int bulletsLeft;


    #region Local Initiation
    public void Reset()
    {
        bulletsLeft = weapon.clipSize;
    }

    public void Initialize()
    {
        Reset();

        if (weapon.id == -1)
            Debug.LogError("Weapon id is -1 for "+weapon.Name);
    }

    #endregion

    private void Start()
    {
        //Initialize();
    }


    private void Update()
    {
        //Make sure the aimfov for camera is set to this guns specified value
        //TODO
        CameraController.aimingFoV = weapon.aimFoV;
        //Check and update server input
        input();

        if (mouseDown)
        {
            shoot();
        }else if(weapon.fireMode == FireMode.Automatic)
        {
            if (mouse)
                shoot();
        }

        //since these variables should only be set to true one frame at a time, we mark them as false
        mouseDown = false;
        mouseUp = false;
    }

    public void shoot()
    {
        if (bulletsLeft > 0 && !reloading)
        {
            //stop player sprinting
            //model.playerModel.transform.parent.GetComponent<Player>().controller.sprinting = false;
            Shoot();
            bulletsLeft--;
        }
        //TODO click noise
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
    
    public void reload()
    {
        if (!reloading)
        {
            if (bulletsLeft != 0)
            {
                StartCoroutine(Reload());
            }
            else StartCoroutine(Reload());
        }
    }

    IEnumerator Reload()
    {
        reloading = true;
        yield return new WaitForSeconds(weapon.reloadTime);
        bulletsLeft = weapon.clipSize;
        reloading = false;
    }

    public void Aim(bool aim)
    {
        if (reloading)
            return;

        //TODO remove AimController
       // AimController.aiming = aim;
        CameraController.aiming = aim;

        this.aiming = aim;
        if (aim)
        {

        }
        else
        {

        }

        /*if (model != null)
        {
            model.playerModel.headTilt = aim;

            if (aiming)
                model.playerModel.armAim = true;
        }*/
    }

    public void Shoot()
    {
        print("shoot");
        GameObject obj = Instantiate(weapon.projectile);//TODO projectile position
        obj.transform.rotation = transform.Find("Camera").GetComponentInChildren<PlayerCamera>().transform.rotation;
        obj.transform.position = transform.Find("Camera").GetComponentInChildren<PlayerCamera>().transform.position;
/*
        if (model != null)
        {
            model.playerModel.armAim = true;
            model.playerModel.Quickdraw();
        }*/
    }
}
