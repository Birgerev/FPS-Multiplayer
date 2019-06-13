using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using net.bigdog.game.player.camera;

namespace net.bigdog.game.player
{
    public class ItemArms : MonoBehaviour
    {
        public Animator anim;

        public Item lastItem;
        public Player player;

        public bool ready = false;

        public Transform weaponSlot;
        public Transform magazineSlot;

        public GameObject itemModel;


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
            if (lastItem != player.item.item)
            {

                print("h");
                lastItem = player.item.item;

                EquipModel(lastItem);
            }

            //ready or not
            if (player.networkInstance != null)
            {
                if (player.networkInstance.input.sprint)
                    ready = false;
                if (player.networkInstance.input.aim || player.networkInstance.input.shoot)
                    ready = true;
            }


            animate();
        }

        private void LateUpdate()
        {
            if (player == null || !player.networkInstance.isLocalPlayer)
                return;
            //Set arms to camera position, so that the arms pivot around the camera
            Transform camera = GetComponentInParent<Player>().transform.Find("Camera").GetComponentInChildren<PlayerCamera>().transform;

            transform.position = camera.position;
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, camera.localRotation.eulerAngles.x - 90);

        }

        private void animate()
        {
            if (player.networkInstance != null)
            {
                anim.SetBool("aiming", player.networkInstance.input.aim);
                anim.SetBool("ready", ready);
            }
        }

        private void EquipModel(Item item)
        {
            EquipMagazine(null);

            if (weaponSlot.childCount > 0)
                Destroy(weaponSlot.GetChild(0).gameObject);

            if (weaponSlot == null)
            {
                Debug.LogError("Weapon Slot is null");
                return;
            }

            GameObject obj = Instantiate(item.Model.gameObject);

            obj.transform.parent = weaponSlot;

            obj.name = obj.name.Replace("(Clone)", "");

            //Reset orientations, scale and position
            obj.transform.localPosition = Vector3.zero;
            obj.transform.localRotation = Quaternion.identity;
            obj.transform.localScale = Vector3.one;

            itemModel = obj;

            //Apply animator
            anim.runtimeAnimatorController = item.firstPersonAnimator;
        }

        private void EquipMagazine(Item magazine)
        {
            if (magazineSlot.childCount > 0)
                Destroy(magazineSlot.GetChild(0).gameObject);

            if (magazineSlot == null)
            {
                Debug.LogError("Magazine Slot is null");
                return;
            }

            if (magazine == null)
                return;

            GameObject obj = Instantiate(magazine.Model.gameObject);

            obj.transform.parent = magazineSlot;

            obj.name = obj.name.Replace("(Clone)", "");

            //Reset orientations, scale and position
            obj.transform.localPosition = Vector3.zero;
            obj.transform.localRotation = Quaternion.identity;
            obj.transform.localScale = Vector3.one;
        }

        public void Reload(Item magazine)
        {
            anim.SetBool("reload", true);
            EquipMagazine(magazine);
        }

        public void ReloadComplete()
        {
            anim.SetBool("reload", false);
        }
    }
}