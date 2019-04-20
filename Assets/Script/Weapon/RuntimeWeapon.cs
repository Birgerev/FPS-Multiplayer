using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;

public class RuntimeWeapon : RuntimeItem {

    
    public bool reloading;

    public int bulletsLeft;

    public bool isLoaded = false; //ie when there is a magazine in the gun

    public WeaponModel model;

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


        reloading = (GetComponent<Player>().model.armAnimator.GetBool("removeMagazine") ||
            GetComponent<Player>().model.armAnimator.GetBool("insertMagazine"));


        if (model == null)
            model = GetComponent<Player>().model.armAnimator.GetComponentInChildren<WeaponModel>();

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
        if (bulletsLeft > 0 && isLoaded && !reloading)
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

        //Wont continiue if we're still reloading
        if (reloading)
            return;

        if (isLoaded)//Remove Magazine
        {
            GetComponent<Player>().model.armAnimator.GetComponent<ItemArms>().RemoveMagazine();
            reloading = false;
            isLoaded = false;

            bulletsLeft = 0;
        }
        else
        {            //Insert magazine
            GetComponent<InventoryManager>().reloadMode = !GetComponent<InventoryManager>().reloadMode;
        }
    }
    public void insertMagazine(Item mag)        //Called by Inventory Manager when exiting reloading mode
    {
        bulletsLeft = mag.magazineData.cartridges;
        isLoaded = true;

        GetComponent<Player>().model.armAnimator.GetComponent<ItemArms>().InsertMagazine(mag);
    }

    public void Shoot()
    {
        print("shoot");
        GameObject obj = Instantiate(item.weaponData.projectile);//TODO projectile position
        obj.transform.rotation = transform.Find("Camera").GetComponentInChildren<PlayerCamera>().transform.rotation;
        obj.transform.position = transform.Find("Camera").GetComponentInChildren<PlayerCamera>().transform.position;

        model.Shoot();
        
/*
        if (model != null)
        {
            model.playerModel.armAim = true;
            model.playerModel.Quickdraw();
        }*/
    }
}
