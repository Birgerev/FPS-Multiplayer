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

    public PriorityData priorityData;

    [Header("Item Specific Data")]
    public WeaponData weaponData;
    public MagazineData magazineData;


    public object Clone()
    {
        object thisItem = this.MemberwiseClone();

        //Clone each item specific data instance individually
        //,as to avoid just referencing the old one
        ((Item)thisItem).weaponData = (WeaponData)((Item)thisItem).weaponData.Clone();
        ((Item)thisItem).magazineData = (MagazineData)((Item)thisItem).magazineData.Clone();
        ((Item)thisItem).priorityData = (PriorityData)((Item)thisItem).priorityData.Clone();

        return thisItem;
    }
}

[System.Serializable]
public class PriorityData
{
    public bool reloading;

    public int cartridges;
    public bool isLoaded = false;                   //ie when there is a magazine in the gun

    public object Clone()
    {
        return this.MemberwiseClone();
    }
}


[System.Serializable]
public class WeaponData
{
    public FireMode fireMode;                       // Weapons availible FireModes TODO multiple fire modes

    public float aimFoV = 30;
    public Vector3 visualRecoil = Vector3.zero;     //How much the weapon recoils upon firing
    public float maxVisualRecoil = 0.01f;     //How much the weapon recoils upon firing
    
    public Vector2 maxCameraRecoil = Vector2.zero;     //How much the camera recoils upon firing

    public float reloadTime;                        // How long reloading of the weapon lasts
    public float projectileVelocity;                // Speed of fired projectiles
    public float rpm;                               // How many rounds can be fired a minute
    public AnimationCurve damageCurve;

    public GameObject projectile;
    
    public object Clone()
    {
        return this.MemberwiseClone();
    }
}

[System.Serializable]
public class MagazineData
{
    public int cartridgeCapacity;

    public object Clone()
    {
        return this.MemberwiseClone();
    }
}