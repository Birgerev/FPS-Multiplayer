using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Weapon {

    public string Name = "Weapon name is null";     // Example: "Ppsh-41"

    public WeaponModel WeaponModel;                 // The Weapon Model Prefab

    public FireMode fireMode;                       // Weapons availible FireModes TODO multiple fire modes

    public int clipSize;                            // The amount of bullets in each Magazine
    public int startClipAmount;                     // How many Magazines does the player spawn with

    public int id = -1;

    public float aimFoV = 30;

    public float handlePullSpeed;                        // How long the pull handle animation will take
    public float handleReleaseSpeed;                        // How long the release handle animation will take

    public GameObject projectile;
}
