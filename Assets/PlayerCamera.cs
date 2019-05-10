using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public Player player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        DestroyIfNotLocal();

        if (player.networkInstance == null)
            return;

        if (player.networkInstance.isLocalPlayer)
            transform.localEulerAngles = new Vector3(player.networkInstance.GetComponent<PlayerInstanceInput>().input.pitch, 0, 0.0f);
        else
            transform.localEulerAngles = new Vector3(player.pitch, 0, 0.0f);
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
