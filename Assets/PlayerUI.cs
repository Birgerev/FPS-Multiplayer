using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PlayerUI : MonoBehaviour {

    public Player player;
    public Text healthText;
    public GameObject healthFiller;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (!player.isLocalPlayer)
            Destroy(gameObject);

        HealthBar(player.health);
    }

    public void HealthBar(float health)
    {
        //Set text
        healthText.text = "" + (int)player.health;
        //Set Filler
        healthFiller.transform.localScale = new Vector3((health/Player.maxHealth), 1, 1);
    }
}
