using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;

public class RuntimeWeapon : NetworkBehaviour {

    public Weapon weapon;               // Stats for weapon
    public WeaponModel model;           // Model class

    public bool handlePulledBack;

    private bool _initialized = false;  // Is the Initialize() funtion called yet? if not, show errors

    public bool aiming = false;
    private bool mouse = false;
    private bool mouseDown = false;
    private bool mouseUp = false;

    public bool reloading;

    [SyncVar]
    public int bulletsLeft;


    #region Local Initiation
    public void Reset()
    {
        //weapon = null;
        if(model != null)
            Destroy(model.gameObject);

        bulletsLeft = weapon.clipSize;
    }

    public void Initialize()
    {
        Reset();
        if(model == null)   // If the creator of the RuntimeWeapon hasn't yet created a model, do so
        {
            createModel();
        }

        if (weapon.id == -1)
            Debug.LogError("Weapon id is -1 for "+weapon.Name);
    }

    private GameObject createModel()
    {
        GameObject obj = Instantiate(weapon.WeaponModel.gameObject);

        model = obj.GetComponent<WeaponModel>();
        model.playerModel = GetComponent<Player>().model;

        return obj;
    }

    #endregion


    private void Update()
    {
        if (isLocalPlayer)
        {
            //Make sure the aimfov for camera is set to this guns specified value
            CameraController.aimingFoV = weapon.aimFoV;
            //Check and update server input
            input();
        }

        if (model != null)
        {
            //handlePulledBack = (model.handler.GetCurrentAnimatorStateInfo(0).IsName("handle_pulled")
            //    || model.handler.GetCurrentAnimatorStateInfo(0).IsName("handle_pulled -> handle_normal"));

            //Set weapon animation times
            //model.ChangeTransitionDuration("handle_normal", "", 0, weapon.shootSpeed / 2);
            //model.ChangeTransitionDuration("handle_pulled", "", 0, weapon.shootSpeed / 2);
        }

        if (isServer)
        {
            if (mouseDown)
            {
                shoot();
            }else if(weapon.fireMode == FireMode.Automatic)
            {
                if (mouse)
                    shoot();
            }

        }

        //since these variables should only be set to true one frame at a time, we mark them as false
        mouseDown = false;
        mouseUp = false;
    }

    IEnumerator release_handle()
    {
        yield return new WaitForSeconds(weapon.handleReleaseSpeed);
        model.handler.Play("handle_normal");//.SetBool("pulled", false);

        if (isServer)
            handlePulledBack = false;

        //if automatic gun, automaticly pull pack handle to ready another shot
        if (weapon.fireMode == FireMode.Automatic)
        {
            StartCoroutine(pull_handle());
        }
    }
    IEnumerator pull_handle()
    {
        yield return new WaitForSeconds(weapon.handlePullSpeed);
        model.handler.Play("handle_pulled");//.SetBool("pulled", true);
        if (isServer)
            handlePulledBack = true;
    }

    public void shoot()
    {
        if (isServer)
        {
            if (bulletsLeft > 0 && !reloading)
            {
                //stop player sprinting
                model.playerModel.transform.parent.GetComponent<Player>().controller.sprinting = false;
                if (handlePulledBack)
                {
                    RpcVisual_shoot();
                    bulletsLeft--;
                }

            }
            //TODO click noise
        }
    }

    public void input()
    {
        if (Input.GetButtonDown("Fire2") || (!aiming && Input.GetAxis("Fire2") > 0))
            CmdInput_Aim(true);
        if (Input.GetButtonUp("Fire2") || (aiming && Input.GetAxis("Fire2") == 0))
            CmdInput_Aim(false);
        if((mouse && Input.GetAxis("Fire1") == 0))
            print(!mouse + "" + Input.GetAxis("Fire1"));
        if (Input.GetButtonDown("Fire1") || (!mouse && Input.GetAxis("Fire1") > 0))
        {
            if (!isServer)
                mouse = true;
            CmdInput_Mouse(true);
        }
        if (Input.GetButtonUp("Fire1") || (mouse && Input.GetAxis("Fire1") == 0))
        {
            if(!isServer)
                mouse = false;
            CmdInput_Mouse(false);
        }

        if (Input.GetButtonDown("Reload"))
        {
            CmdReload();
        }
    }

    [Command]
    public void CmdReload()
    {
        if (!reloading)
        {
            if (bulletsLeft != 0 && !handlePulledBack)
            {
                RpcVisual_HandPullHandle();  //TODO animations with hands
            }
            else RpcReload();
        }
    }

    [ClientRpc]
    public void RpcVisual_HandPullHandle()
    {
        //TODO animations
        StartCoroutine(pull_handle());
    }

    [ClientRpc]
    public void RpcReload()
    {
        //TODO animations
        model.playerModel.armAnimator.SetTrigger("reloading");
        if (isServer)
            StartCoroutine(Reload());
    }

    IEnumerator Reload()
    {
        reloading = true;
        yield return new WaitForSeconds(weapon.reloadTime);
        bulletsLeft = weapon.clipSize;
        reloading = false;
    }

    [Command]
    public void CmdInput_Aim(bool aim)
    {
        //stop player sprinting
        model.playerModel.transform.parent.GetComponent<Player>().controller.sprinting = false;

        //Show results to all connected clients
        if(!reloading)
            RpcVisual_aim(aim);
    }

    [Command]
    public void CmdInput_Mouse(bool value)
    {
        print("mouse: "+value);
        //Set input variables for later use
        if (value)
            mouseDown = true;
        else mouseUp = true;

        mouse = value;
    }

    [ClientRpc]
    public void RpcVisual_aim(bool aim)
    {
        if (isLocalPlayer)
        {
            //TODO remove AimController
            AimController.aiming = aim;
            CameraController.aiming = aim;
        }

        this.aiming = aim;
        if (aim)
        {

        }
        else
        {

        }

        if (model != null)
        {
            model.playerModel.headTilt = aim;

            if (aiming)
                model.playerModel.armAim = true;
        }
    }

    [ClientRpc]
    public void RpcVisual_shoot()
    {
        StartCoroutine(release_handle());

        GameObject obj = Instantiate(weapon.projectile);
        obj.transform.rotation = model.barrel.transform.rotation;
        obj.transform.position = model.barrel.transform.position;

        if (model != null)
        {
            model.playerModel.armAim = true;
            model.playerModel.Quickdraw();
        }
    }
}
