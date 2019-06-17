using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;
using net.bigdog.game.player;
using net.bigdog.game.player.camera;

public class RuntimeWeapon : RuntimeItem {

    
    public WeaponModel model;

    private float timeSinceFire;
    private Quaternion lastFrameCameraRotation;


    private void Start()
    {
    }


    public override void Update()
    {
        base.Update();

        //Make sure the aimfov for camera is set to this guns specified value
        //TODO
        GetComponent<Player>().cameraController.aimingFoV = item.weaponData.aimFoV;
        //Check and update server input
        
        if (model == null)
            model = GetComponent<Player>().model.armAnimator.GetComponent<ItemArms>().itemModel.GetComponent<WeaponModel>();

        timeSinceFire += Time.deltaTime;
        if (timeSinceFire > (60 / item.weaponData.rpm)) {
            if (mouseDown)
            {
                shoot();
            } else if (item.weaponData.fireMode == FireMode.Automatic)
            {
                if (mouse)
                    shoot();
            }
        }

        lastFrameCameraRotation = transform.Find("Camera").GetComponentInChildren<PlayerCamera>().transform.rotation;

    }

    public void shoot()
    {
        if (item.priorityData.cartridges > 0 && item.priorityData.isLoaded && !item.priorityData.reloading)
        {
            timeSinceFire = 0;
            //stop player sprinting
            //model.playerModel.transform.parent.GetComponent<Player>().controller.sprinting = false;
            Shoot();
            item.priorityData.cartridges--;
        }
        //TODO click noise
    }

    public override void Aim(bool aim)
    {
        //prevent aiming while reloading
        if(!item.priorityData.reloading)
            base.Aim(aim);

        //change camera FoV
        GetComponent<Player>().cameraController.aiming = aim;
        if (aim)
        {
            GetComponent<Player>().cameraController.aimingFoV = item.weaponData.aimFoV;
        }
    }

    public override void reload()
    {
        base.reload();

        //Wont continiue if we're still reloading
        if (item.priorityData.reloading)
            return;

        int slot = bestMagazineSlot();

        if (slot == -1)
            return;

        Item newMag = (Item)GetComponent<InventoryManager>().items[slot];
        
        //replace new magazine with the old one in inventory
        Item oldMag = (Item)newMag.Clone();
        oldMag.priorityData.cartridges = item.priorityData.cartridges;
        GetComponent<InventoryManager>().items[slot] = oldMag;

        //Reload animations
        GetComponent<Player>().model.armAnimator.GetComponent<ItemArms>().Reload(newMag);

        //Apply new magazine stats
        item.priorityData.reloading = true;
        item.priorityData.isLoaded = false;
        item.priorityData.cartridges = newMag.priorityData.cartridges;

        //Wait untill reload animation is done to call 'reloadComplete()'
        StartCoroutine(waitForReload());
    }

    IEnumerator waitForReload()
    {
        yield return new WaitForSeconds(item.weaponData.reloadTime);
        reloadComplete();
    }

    public void reloadComplete()
    {
        GetComponent<Player>().model.armAnimator.GetComponent<ItemArms>().ReloadComplete();
        item.priorityData.isLoaded = true;
        item.priorityData.reloading = false;
    }

    private int bestMagazineSlot()
    {
        int bestSlot = -1;
        int bestCartridgeCount = 0;

        for(int slot = 0; slot < GetComponent<InventoryManager>().items.Count; slot++)
        {
            Item iteratedItem = GetComponent<InventoryManager>().items[slot];
            if(iteratedItem.priorityData.cartridges > 0)
                if(iteratedItem.priorityData.cartridges > bestCartridgeCount)
                {
                    bestSlot = slot;
                    bestCartridgeCount = iteratedItem.priorityData.cartridges;
                }
        }

        return bestSlot;
    }

    public void Shoot()
    {
        GameObject projectile = Instantiate(item.weaponData.projectile);//TODO projectile position
        projectile.transform.rotation = transform.Find("Camera").GetComponentInChildren<PlayerCamera>().transform.rotation;
        projectile.transform.position = transform.Find("Camera").GetComponentInChildren<PlayerCamera>().transform.position;

        if(GetComponent<Player>().networkInstance.isLocalPlayer)
            projectile.transform.rotation = lastFrameCameraRotation;


        projectile.GetComponent<Bullet>().ownerId = GetComponent<Player>().networkInstance.id;

        projectile.GetComponent<Bullet>().damageCurve = item.weaponData.damageCurve;
        projectile.GetComponent<Bullet>().speed = item.weaponData.projectileVelocity;

        model.cocked = item.priorityData.isLoaded;
        model.Shoot();

        //Weapon camera spring recoil
        GetComponent<Player>().cameraController.Recoil(item.weaponData.visualRecoil, item.weaponData.maxVisualRecoil);

        //Camera Recoil
        if (GetComponent<Player>().networkInstance.isLocalPlayer) {
            GetComponent<Player>().networkInstance.GetComponent<PlayerInstanceInput>().input.yaw += Random.Range(-item.weaponData.maxCameraRecoil.x, item.weaponData.maxCameraRecoil.x);
            GetComponent<Player>().networkInstance.GetComponent<PlayerInstanceInput>().input.pitch -= Random.Range(item.weaponData.maxCameraRecoil.y / 4, item.weaponData.maxCameraRecoil.y);
        }
        /*
        if (model != null)
        {
            model.playerModel.armAim = true;
            model.playerModel.Quickdraw();
        }*/
    }
}
