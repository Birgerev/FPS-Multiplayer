using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;
using net.bigdog.game.player;
using net.bigdog.game.player.camera;

public class RuntimeWeapon : RuntimeItem {

    
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
        
        if (model == null)
            model = GetComponent<Player>().model.armAnimator.GetComponent<ItemArms>().itemModel.GetComponent<WeaponModel>();

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
        if (item.weaponData.bulletsLeft > 0 && item.weaponData.isLoaded && !item.weaponData.reloading)
        {
            //stop player sprinting
            //model.playerModel.transform.parent.GetComponent<Player>().controller.sprinting = false;
            Shoot();
            item.weaponData.bulletsLeft--;
        }
        //TODO click noise
    }

    public override void Aim(bool aim)
    {
        //prevent aiming while reloading
        if(!item.weaponData.reloading)
            base.Aim(aim);

        //change camera FoV
        CameraController.aiming = aim;
        if (aim)
        {
            CameraController.aimingFoV = item.weaponData.aimFoV;
        }
    }

    public override void reload()
    {
        base.reload();

        //Wont continiue if we're still reloading
        if (item.weaponData.reloading)
            return;

        int slot = bestMagazineSlot();

        if (slot == -1)
            return;

        Item newMag = (Item)GetComponent<InventoryManager>().items[slot];
        
        //replace new magazine with the old one in inventory
        Item oldMag = (Item)newMag.Clone();
        oldMag.magazineData.cartridges = item.weaponData.bulletsLeft;
        GetComponent<InventoryManager>().items[slot] = oldMag;

        //Reload animations
        GetComponent<Player>().model.armAnimator.GetComponent<ItemArms>().Reload(newMag);

        //Apply new magazine stats
        item.weaponData.reloading = true;
        item.weaponData.isLoaded = false;
        item.weaponData.bulletsLeft = newMag.magazineData.cartridges;

        //Wait untill reload animation is done to call 'reloadComplete()'
        StartCoroutine(waitForReload());
    }

    IEnumerator waitForReload()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.1f);
            if (GetComponent<Player>().model.armAnimator.GetComponent<ItemArms>().isReloadComplete())
                reloadComplete();
        }
    }

    public void reloadComplete()
    {
        item.weaponData.isLoaded = true;
        item.weaponData.reloading = false;
    }

    private int bestMagazineSlot()
    {
        int bestSlot = -1;
        int bestCartridgeCount = 0;

        for(int slot = 0; slot < GetComponent<InventoryManager>().items.Count; slot++)
        {
            Item iteratedItem = GetComponent<InventoryManager>().items[slot];
            if(iteratedItem.magazineData.cartridges > 0)
                if(iteratedItem.magazineData.cartridges > bestCartridgeCount)
                {
                    bestSlot = slot;
                    bestCartridgeCount = iteratedItem.magazineData.cartridges;
                }
        }

        return bestSlot;
    }

    public void Shoot()
    {
        GameObject projectile = Instantiate(item.weaponData.projectile);//TODO projectile position
        projectile.transform.rotation = transform.Find("Camera").GetComponentInChildren<PlayerCamera>().transform.rotation;
        projectile.transform.position = transform.Find("Camera").GetComponentInChildren<PlayerCamera>().transform.position;

        projectile.GetComponent<Bullet>().ownerId = GetComponent<Player>().networkInstance.id;

        model.cocked = item.weaponData.isLoaded;
        model.Shoot();

        //Camera spring recoil
        GetComponent<Player>().cameraController.Recoil(item.weaponData.visualRecoil, item.weaponData.maxVisualRecoil);
        /*
        if (model != null)
        {
            model.playerModel.armAim = true;
            model.playerModel.Quickdraw();
        }*/
    }
}
