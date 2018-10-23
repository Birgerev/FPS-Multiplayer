using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;

public class WeaponHolder : NetworkBehaviour {

    public RuntimeWeapon weapon;

    [SyncVar]
    public int weaponType = -1;

    public List<int> weapons = new List<int>();

    private void Start()
    {
        if (isLocalPlayer)
            SetRuntimeWeapon(0);
    }

    private void Update()
    {
        SyncWeapon();
    }

    private void SyncWeapon()
    {
        if (!isLocalPlayer)
        {
            bool synced = true;
            if (weapon != null)
                if (weapon.weapon.id != weaponType)
                    synced = false;
            if (weapon == null && weaponType != -1)
                synced = false;

            //if weapons are already synced, we do not need to sync again
            if (synced)
                return;

            print("Weapons aren't synced");
            SetRuntimeWeapon(weaponType);
        }
    }

    public void SetRuntimeWeapon(int id)
    {
        weapon = WeaponManager.instance.createInstance(gameObject, id);

        CmdSyncWeaponId(id);
        assignWeaponModel(weapon);
    }

    [Command]
    void CmdSyncWeaponId(int id)
    {
        if (isServer)
            weaponType = id;
    }

    private void assignWeaponModel(RuntimeWeapon weapon)
    {
        clearWeaponModel();

        WeaponModel model = weapon.model;
        model.transform.parent = GetComponent<Player>().model.weaponModelHolder.transform;
        if (isLocalPlayer)
            SetLayerRecursively(model.gameObject, 12);
        model.initializeAnimator();
        model.resetPosition();
    }

    private void clearWeaponModel()
    {
        WeaponModel model = GetComponent<Player>().model.weaponModelHolder.transform.GetComponentInChildren<WeaponModel>();

        if (model != null)
            Destroy(model.gameObject);
    }

    private void SetLayerRecursively(GameObject go, int layerNumber)
    {
        foreach (Transform trans in go.GetComponentsInChildren<Transform>(true))
        {
            trans.gameObject.layer = layerNumber;
        }
    }
}
