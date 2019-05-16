using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;
using net.bigdog.game.player.camera;

namespace net.bigdog.game.player
{
    public class Player : MonoBehaviour
    {

        public static Player localPlayer;

        public PlayerInstance networkInstance;

        public RuntimeItem item;

        public CharacterModel model;
        public GameObject modelPrefab;
        public CharacterController controller;
        public CameraController cameraController;

        public GameObject death;

        public float pitch;
        public float yaw;

        public const int maxHealth = 100;

        public float health
        {
            get
            {
                if (networkInstance == null)
                    return 100;

                return networkInstance.health;
            }

            set
            {
                if (networkInstance == null)
                    return;

                networkInstance.health = value;
            }
        }

        public void TakeDamage(float amount)
        {
            //Reduce player health
            health -= amount;
        }

        public void TakeDamage(float amount, int damagerId)
        {
            TakeDamage(amount);

            //Call gameemode events if we are the server
            if (networkInstance.isServer)
            {
                //Call Damage event
                print("damage1");
                gamemode.Gamemode.instance.Rpc_OnPlayerDamage(
                    new gamemode.Player(networkInstance.id),
                    new gamemode.Player(damagerId));

                //If our new health is 0, we report a killed event
                if(health <= 0)
                    gamemode.Gamemode.instance.Rpc_OnPlayerKilled(
                        new gamemode.Player(networkInstance.id),
                        new gamemode.Player(damagerId));
            }
        }

        public void Die()
        {
            GameObject boom = Instantiate(death);
            boom.transform.position = transform.position;

            Destroy(gameObject);
        }

        public void Spawn()
        {
            //if (isServer)
            {
                health = maxHealth;
            }
            //if (hasAuthority)
            {
                //              TODO

                //Spawnpoint[] spawnpoints = Object.FindObjectsOfType<Spawnpoint>();
                //Spawnpoint spawnpoint = spawnpoints[Random.Range(0, spawnpoints.Length)];
                //print("sp: "+spawnpoint.gameObject.name);

                //transform.position = spawnpoint.transform.position;
                //transform.rotation = spawnpoint.transform.rotation;
            }
        }

        public void ApplyModel()
        {
            GameObject obj = Instantiate(modelPrefab);
            obj.transform.parent = transform;
            obj.transform.localPosition = Vector3.zero;
            obj.transform.localRotation = Quaternion.identity;

            model = obj.GetComponent<CharacterModel>();
        }

        // At initialazation of component
        private void Start()
        {
            controller = GetComponent<CharacterController>();
            cameraController = GetComponentInChildren<CameraController>();

            print("player spawned");
            ApplyModel();
            removeForeingComponents();

            Spawn();

            if (networkInstance.isLocalPlayer)
                model.firstPerson = true;
        }

        private void Update()
        {
            //Update walk animations for character model
            model.characterAnimator.SetFloat("horizontal", controller.velocityInput.x);
            model.characterAnimator.SetFloat("vertical", controller.velocityInput.z);

            //Update state(crouch/sprint) animation variables for character model
            model.characterAnimator.SetBool("crouching", controller.crouching);
            model.characterAnimator.SetBool("sprinting", controller.sprinting);

            //Set global variable so that we easily can access the local player
            if (networkInstance != null)
                if (networkInstance.isLocalPlayer)
                    if (localPlayer == null)
                        localPlayer = this;

            //Rotate the player along the yaw axis
            if (networkInstance != null)
                if (networkInstance.isLocalPlayer)
                {
                    transform.localEulerAngles = new Vector3(0, networkInstance.GetComponent<PlayerInstanceInput>().input.yaw, 0.0f);
                }
                else
                {
                    transform.localEulerAngles = new Vector3(0, yaw, 0.0f);
                }

            //Animate spine / player model looks up or down
            if (model != null)   //Make sure we dont get a null pointer error
                if (networkInstance == null)
                    model.spineRotator.pitch = pitch;
                else if (!networkInstance.isLocalPlayer)  //Make sure we arent the local player, since animating the spine would mess up camera animations
                    model.spineRotator.pitch = pitch;

            //Runtime weapon instance
            item = transform.GetComponent<RuntimeItem>();


            if (health <= 0)
            {
                Die();
            }
        }

        public void CmdSetPitch(float pitch)
        {
            //if (isServer)
            {
                this.pitch = pitch;
            }
        }

        private void removeForeingComponents()
        {
            if (model != null)
                if (model.mainCamera != null)
                {
                    //        if (!isLocalPlayer)
                    {
                        Destroy(model.mainCamera.GetComponent<FlareLayer>());
                        Destroy(model.mainCamera.GetComponent<Camera>());
                        Destroy(model.mainCamera.GetComponent<AudioListener>());
                        Destroy(model.mainCamera.transform.Find("WeaponCamera").GetComponent<Camera>());
                        Destroy(model.mainCamera.GetComponent<CameraController>());
                        model.DisableCamera();
                    }
                }
        }
    }
}
