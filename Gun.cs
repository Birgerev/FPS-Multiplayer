using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;

public class Gun : NetworkBehaviour {
    
    public Magazine magazine;
    public Projectile loadedProjectile;

    public Animator anim;
    public Animator handler;

    public bool HandlePulledBack = false;
    public int totalBullets;

    public GameObject barrel;
    public GameObject bullet;

    public float aimFoV = 50;
    public float handleTime = 0;

    private Player player = null;

    public FiringType fireType = FiringType.Automatic;
    
    public void Update()
    {
        //Print errors for missing required objects
        if (barrel == null)
            Debug.LogError("Barrel Object Is Missing!");


        if (player == null) //Get Player parent if it is not already asigned
        {
            //Iterate through parents until a Player script is found
            Transform lastparent = transform.parent;
            while (lastparent.GetComponent<Player>() == null) 
            {
                lastparent = lastparent.parent;
            }
            //Assign player variable once the player is found
            player = lastparent.GetComponent<Player>();
        }

        //If this instance of the script is being run on the client which owns the gun
        if (player.isLocalPlayer)
        {
            //Make sure the aimfov for camera is set to this guns specified value
            CameraController.aimingFoV = aimFoV;

            //Chech whether were aiming or not
            AimController.aiming = Input.GetKey(InputManager.interact2);
            CameraController.aiming = Input.GetKey(InputManager.interact2);
            //Update UI ammo text
            WeaponUI.instance.UpdateText(magazine.amount(), totalBullets);
        }
        if (isServer)   //If instance of script is being run on the server
        {
            //If the handle uses animation length instead of time, assign the state of the handle from the Animator
            //if (handleTime == 0)
            //    HandlePulledBack = handler.GetCurrentAnimatorStateInfo(0).IsName("handle_pulled");

            //If the gun is an Automatic, we will not have to wait for another OnMouseDown event
            if (fireType == FiringType.Automatic)
            {
                if (player.GetComponent<Player>().input.interact1)
                {
                    shoot();
                }
            }

            //check whether we should call reload() funtion
            if (player.input.reload)
                reload();
        }
    }

    public void Start()
    {
        if (player.isServer)   //If instance of script is being run on the server
        {
            reload();
        }
    }

    public void reload()
    {
        if (totalBullets > 0)
        {
            totalBullets -= magazine.reload(totalBullets);
        }

        RpcReload();
    }

    [ClientRpc]
    public void RpcReload() //Client Reload
    {
        //TODO animation
        StartCoroutine(pullHandle());
        print("server reload confirmed");
    }

    IEnumerator pullHandle()
    {
        int frames = 0;
        while ((HandlePulledBack))
        {
            yield return new WaitForSeconds(0.05f);
            frames++;
        }
        yield return new WaitForSeconds(handleTime);
        HandlePulledBack = true;

        loadedProjectile = null;//Todo chamber ejection
        loadedProjectile = magazine.removeBullet();
        handler.SetBool("pulled", HandlePulledBack);
    }

    IEnumerator releaseHandle()
    {
        int frames = 0;
        while (!HandlePulledBack)
        {
            yield return new WaitForSeconds(0.01f);
            frames++;
            if(frames > 50)
            {
                yield break;
            }
        }
        if (handleTime != 0)
        {
            yield return new WaitForSeconds(handleTime);
            HandlePulledBack = false;
        }
    }

    public void shoot()
    {
        if (!HandlePulledBack)
            return;

        RpcShoot();
    }

    [ClientRpc]
    public void RpcShoot()
    {
        StartCoroutine(releaseHandle());    //Handle Animation
        if (loadedProjectile == null)
        {
            //TODO click noise
            return;
        }
        else
        {
            loadedProjectile.shoot(barrel, bullet, gameObject);
            
            anim.SetTrigger("shoot");
        }

        if (fireType != FiringType.Bolt)
            StartCoroutine(pullHandle());
    }
}

public enum FiringType
{
    Automatic,
    Semi_Automatic,
    Bolt
}