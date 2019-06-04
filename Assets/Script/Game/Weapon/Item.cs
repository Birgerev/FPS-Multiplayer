using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Item
{
    public string Name = "Weapon name is null";     // Example: "Ppsh-41"

    public GameObject Model;                 // The Item Model Prefab

    public string runtimeScript = "RuntimeItem";
    
    public int id = -1;


    //Animators
    public RuntimeAnimatorController firstPersonAnimator;
    public RuntimeAnimatorController thirdPersonAnimator;

    [Header("Item Specific Data")]
    public WeaponData weaponData;
    public MagazineData magazineData;


    public object Clone()
    {
        return this.MemberwiseClone();
    }
}

[System.Serializable]
public class WeaponData
{
    public FireMode fireMode;                       // Weapons availible FireModes TODO multiple fire modes

    public int clipSize;                            // The amount of bullets in each Magazine
    public int startClipAmount;                     // How many Magazines does the player spawn with
    
    public float aimFoV = 30;
    public Vector3 visualRecoil = Vector3.zero;     //How much the weapon recoils upon firing
    public float maxVisualRecoil = 0.01f;     //How much the weapon recoils upon firing

    public float handlePullSpeed;                        // How long the pull handle animation will take
    public float handleReleaseSpeed;                        // How long the release handle animation will take

    public float reloadTime;                        // How long reloading of the weapon lasts

    public GameObject projectile;

    public bool reloading;
    public int bulletsLeft;
    public bool isLoaded = false;                   //ie when there is a magazine in the gun
}

[System.Serializable]
public class MagazineData
{
    public int cartridges;
}