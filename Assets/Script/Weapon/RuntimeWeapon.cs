using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;

public class RuntimeWeapon : RuntimeItem {

    
    public bool reloading;

    public int bulletsLeft;
    
    private void Start()
    {

    }


    public override void Update()
    {
        base.Update();
        //Make sure the aimfov for camera is set to this guns specified value
        //TODO
        CameraController.aimingFoV = item.weaponData.aimFoV;
        //Check and update server input

        if (mouseDown)
        {
            shoot();
        }else if(item.weaponData.fireMode == FireMode.Automatic)
        {
            if (mouse)
                shoot();
        }
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

    public override void Aim(bool aim)
    {
        //prevent aiming while reloading
        if(!reloading)
            base.Aim(aim);
    }

    public override void reload()
    {
        base.reload();

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
        yield return new WaitForSeconds(item.weaponData.reloadTime);
        bulletsLeft = item.weaponData.clipSize;
        reloading = false;
    }

    public void Shoot()
    {
        print("shoot");
        GameObject obj = Instantiate(item.weaponData.projectile);//TODO projectile position
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
