using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public static WeaponManager instance;

    public List<Weapon> weapons = new List<Weapon>();

    void Start()
    {
        instance = this;    //So that refering to class is made easy

        for (int i = 0; i < weapons.Count; i++)
        {
            weapons[i].id = i;
        }
    }

    public RuntimeWeapon createInstance(GameObject obj, int id)
    {
        //Create and initialize a RuntimeWeapon
        RuntimeWeapon weapon = obj.GetComponent<RuntimeWeapon>();
        weapon.weapon = weapons[id];
        weapon.Initialize();

        print("weapon: " + weapon);
        return weapon;
    }
}
