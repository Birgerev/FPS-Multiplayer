using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using net.bigdog.game.player;

namespace net.bigdog.game.player.ui
{
    public class PlayerUI : MonoBehaviour
    {
        public static PlayerUI instance;
        public PlayerInstance playerInstance;

        public GameObject playerUI;
        public GameObject globalUI;


        // Use this for initialization
        void Start()
        {
            DontDestroyOnLoad(gameObject);
            instance = this;
            playerInstance = PlayerInstance.localInstance;
        }

        // Update is called once per frame
        void Update()
        {
            if (playerInstance == null)
            {
                Debug.LogError("PlayerUI: playerInstance is missing");
                playerInstance = PlayerInstance.localInstance;
                return;
            }

            playerUI.SetActive(playerInstance.player != null);
            globalUI.SetActive(playerInstance != null);
        }
    }
}
