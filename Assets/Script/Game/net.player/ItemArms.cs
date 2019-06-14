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
        public Item item
        {
            get
            {
                return model.item;
            }
        }

        //public Player player;
        public CharacterModel model;

        public Transform weaponSlot;
        public Transform magazineSlot;

        public GameObject itemModel;


        // Start is called before the first frame update
        void Start()
        {
            model = GetComponentInParent<CharacterModel>();
        }

        // Update is called once per frame
        void Update()
        {
            anim = GetComponent<Animator>();

            //If current weapon has been changed, update our weapon
            if (lastItem != item)
            {
                lastItem = item;

                EquipModel(lastItem);
            }

            animate();
        }

        private void LateUpdate()
        {
            if (GetComponentInParent<Player>() == null || !model.firstPerson)
                return;
            //Set arms to camera position, so that the arms pivot around the camera
            Transform camera = GetComponentInParent<Player>().cameraController.camera.transform;

            transform.position = camera.position;
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, camera.localRotation.eulerAngles.x - 90);

        }

        private void animate()
        {
            anim.SetBool("aiming", model.aim);
            anim.SetBool("ready", model.ready);
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