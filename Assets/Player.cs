using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;

public class Player : NetworkBehaviour {

    public static Player localPlayer;

    public CharacterModel model;
    public GameObject modelPrefab;
    public CharacterController controller;

    [SyncVar]
    public float pitch;
    public float yaw;
    public float localpitch;
    
    public const int maxHealth = 100;

    [SyncVar(hook = "OnChangeHealth")]
    public float health = maxHealth;
    
    public void TakeDamage(float amount)
    {
        if (!isServer)
            return;

        health -= amount;
        if (health <= 0)
        {
            health = 0;
            Debug.Log("Dead!");
            CmdRespawn();
        }
    }

    [Command]
    public void CmdRespawn()
    {
        RpcRespawn();
    }

    [ClientRpc]
    public void RpcRespawn()
    {
        print("rpc");
        Respawn();
    }

    public void Respawn()
    {
        if (isServer)
        {
            health = maxHealth;
        }
        print("local: "+ isLocalPlayer);
        if (hasAuthority)
        {
            Spawnpoint[] spawnpoints = Object.FindObjectsOfType<Spawnpoint>();
            Spawnpoint spawnpoint = spawnpoints[Random.Range(0, spawnpoints.Length)];
            print("sp: "+spawnpoint.gameObject.name);

            transform.position = spawnpoint.transform.position;
            transform.rotation = spawnpoint.transform.rotation;
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
    
    void OnChangeHealth(float health)
    {
        if(!isServer)
            this.health = health;
    }

    // At initialazation of component
    private void Start()
    {
        controller = GetComponent<CharacterController>();
        print("player spawned");
        ApplyModel();
        removeForeingComponents();
        print("camera: "+ model.mainCamera.name);

        Respawn();
    }

    private void Update()
    {

        model.characterAnimator.SetFloat("horizontal", controller.velocityInput.x);
        model.characterAnimator.SetFloat("vertical", controller.velocityInput.z);

        model.characterAnimator.SetBool("crouching", controller.crouching); 
        model.characterAnimator.SetBool("sprinting", controller.sprinting);

        if (hasAuthority)
        {
            if (localPlayer == null)
                localPlayer = this;
            transform.localEulerAngles = new Vector3(0, yaw, 0.0f);

        }
        else
        {
            localpitch = pitch;
        }

        if(model != null)
            model.spineRotator.pitch = localpitch;
    }

    [Command]
    public void CmdSetPitch(float pitch)
    {
        if (isServer)
        {
            this.pitch = pitch;
        }
    }

    private void removeForeingComponents()
    {
        if(model != null)
            if (model.mainCamera != null)
            {
                if (!isLocalPlayer)
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
