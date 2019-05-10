using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public Player player;

    // Start is called before the first frame update
    void Start()
    {
        DestroyIfNotLocal();

        GetComponent<Camera>().enabled = true;

        if (player.networkInstance.isServer)
        {
            player.networkInstance.limitPitch = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        DestroyIfNotLocal();

        if (player.networkInstance == null)
            return;
        
        float pitch = 0;
    
        if (player.networkInstance.isLocalPlayer)
            pitch = player.networkInstance.GetComponent<PlayerInstanceInput>().input.pitch;
        else
            pitch = player.pitch;


        transform.localEulerAngles = new Vector3(pitch, 0, 0);
    }


    public void DestroyIfNotLocal()
    {
        if (player.networkInstance == null || !player.networkInstance.isLocalPlayer)
        {
            Destroy(GetComponent<UnityEngine.PostProcessing.PostProcessingBehaviour>());
            Destroy(GetComponent<Camera>());
            return;
        }
    }
}
