using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using net.bigdog.game.player.camera;

namespace net.bigdog.game.player
{
    public class Nametag : MonoBehaviour
    {
        public Text playerNameField;

        private Player player;

        // Start is called before the first frame update
        void Start()
        {
            player = GetComponentInParent<Player>();
        }

        // Update is called once per frame
        void Update()
        {
            deleteIfLocal();
            lookTowardsCamera();
            changeInformation();
        }

        private void changeInformation()
        {
            playerNameField.text = player.networkInstance.profile.name;
        }

        private void lookTowardsCamera()
        {
            transform.LookAt(Player.localPlayer.cameraController.camera.transform);
        }

        private void deleteIfLocal()
        {
            if(player.networkInstance.isLocalPlayer){
                Destroy(gameObject);
            }
        }
    }
}