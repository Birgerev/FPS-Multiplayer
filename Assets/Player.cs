using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;

public class Player : NetworkBehaviour {

    public static Player localPlayer;

    public CharacterModel model;
    public GameObject modelPrefab;

    [SyncVar]
    public float pitch;
    public float yaw;
    public float localpitch;


    public const int maxHealth = 100;

    [SyncVar(hook = "RpcOnChangeHealth")]
    public float health = maxHealth;
    
    public void TakeDamage(float amount)
    {
        if (isServer)
        {
            print("take damage");
            if (!isServer)
                return;

            health -= amount;
            if (health <= 0)
            {
                health = 0;
                Debug.Log("Dead!");
            }
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

    [ClientRpc]
    void RpcOnChangeHealth(float health)
    {
        if(!isServer)
            this.health = health;
    }

    // At initialazation of component
    private void Start()
    {
        ApplyModel();
        removeForeingComponents();
    }

    private void Update()
    {
        if (isLocalPlayer)
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
                    Destroy(GetComponent<PlayerController>());
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
