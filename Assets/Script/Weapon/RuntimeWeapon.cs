using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;

public class RuntimeWeapon : NetworkBehaviour {

    public Weapon weapon;               // Stats for weapon
    public WeaponModel model;           // Model class

    public bool handlePulledBack;

    private bool _initialized = false;  // Is the Initialize() funtion called yet? if not, show errors

    private bool aiming = false;
    private bool mouse = false;
    private bool mouseDown = false;
    private bool mouseUp = false;

    #region Local Initiation
    public void Reset()
    {
        //weapon = null;
        if(model != null)
            Destroy(model.gameObject);
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
        model.armAnimatior = GetComponent<Player>().model.armAnimator;

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
        model.handler.SetBool("pulled", false);
        yield return new WaitForSeconds(weapon.handleReleaseSpeed/2);
        handlePulledBack = false;

        //if automatic gun, automaticly pull pack handle to ready another shot
        if (weapon.fireMode == FireMode.Automatic)
        {
            StartCoroutine(pull_handle());
        }
    }
    IEnumerator pull_handle()
    {
        model.handler.SetBool("pulled", true);
        yield return new WaitForSeconds(weapon.handlePullSpeed);
        handlePulledBack = true;
    }

    public void shoot()
    {
        if (isServer)
        {
            if (handlePulledBack)
            {
                StartCoroutine(release_handle());
                RpcVisual_shoot();
            }
        }
    }

    public void input()
    {
        if (Input.GetMouseButtonDown(1))
            CmdInput_Aim(true);
        if (Input.GetMouseButtonUp(1))
            CmdInput_Aim(false);

        if (Input.GetMouseButtonDown(0))
            CmdInput_Mouse(true);
        if (Input.GetMouseButtonUp(0))
            CmdInput_Mouse(false);
    }

    [Command]
    public void CmdInput_Aim(bool aim)
    {
        //Show results to all connected clients
        RpcVisual_aim(aim);
    }

    [Command]
    public void CmdInput_Mouse(bool mouse)
    {
        //Set input variables for later use
        if (mouse)
            mouseDown = true;
        else mouseUp = true;

        this.mouse = mouse;
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

        }else
        {

        }

        if (model != null)
            model.armAnimatior.SetBool("aiming", aiming);
    }

    [ClientRpc]
    public void RpcVisual_shoot()
    {
        GameObject obj = Instantiate(weapon.projectile);
        obj.transform.rotation = model.barrel.transform.rotation;
        obj.transform.position = model.barrel.transform.position;
    }
}
