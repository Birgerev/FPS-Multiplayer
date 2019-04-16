using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemArms : MonoBehaviour
{
    public Animator anim;

    public Item lastItem;
    public Player player;

    public bool ready = false;

    public Transform weaponSlot;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null)//If player instance is null, find our player
            player = transform.GetComponentInParent<Player>();

        //If no player instance still was found, keep searching
        if (player == null)
            return;

        if (anim == null)
            anim = GetComponent<Animator>();

        if (player.item == null)
            return;

        //If current weapon has been changed, update our weapon
        if(lastItem != player.item.item)
        {

            print("h");
            lastItem = player.item.item;

            EquipModel(lastItem);
        }

        //ready or not
        if (player.networkInstance.input.sprint)
            ready = false;
        if (player.networkInstance.input.aim || player.networkInstance.input.shoot)
            ready = true;


        
        animate();
    }

    private void LateUpdate()
    {
        //Set arms to camera position, so that the arms pivot around the camera
        Transform camera = GetComponentInParent<Player>().transform.Find("Camera").GetComponentInChildren<PlayerCamera>().transform;

        transform.position = camera.position;
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, camera.localRotation.eulerAngles.x - 90);

    }

    private void animate()
    {
        anim.SetBool("aiming", player.networkInstance.input.aim);
        anim.SetBool("ready", ready);
    }

    private void EquipModel(Item weapon)
    {
        if (weaponSlot == null)
            Debug.LogError("Weapon Slot is null");

        if(weaponSlot.childCount > 0)
            Destroy(weaponSlot.GetChild(0));

        GameObject obj = Instantiate(weapon.Model.gameObject);

        obj.transform.parent = weaponSlot;

        obj.name = obj.name.Replace("(Clone)", "");

        //Reset orientations, scale and position
        obj.transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
        obj.transform.localScale = Vector3.one;
    }
}
