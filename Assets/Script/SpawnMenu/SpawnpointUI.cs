using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnpointUI : MonoBehaviour
{
    private float groundMargin = 20;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = transform.position + new Vector3(0, groundMargin, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Spawn()
    {
        PlayerInstance.localInstance.CmdSpawn(transform.position - new Vector3(0, groundMargin, 0));
    }
}
