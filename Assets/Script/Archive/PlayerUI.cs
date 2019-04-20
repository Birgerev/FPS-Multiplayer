using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PlayerUI : MonoBehaviour {
    
    private Player player;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if(player == null)
        {
            player = transform.GetComponentInParent<Player>();
        }

        //Destroy UI so that we only show the local ui
        if (player.networkInstance == null || !player.networkInstance.isLocalPlayer)
            Destroy(gameObject);
    }
}
