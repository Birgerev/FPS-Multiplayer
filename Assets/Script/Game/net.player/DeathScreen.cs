using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace net.bigdog.game.player
{
    public class DeathScreen : MonoBehaviour
    {
        public float deadTime = 5;

        // Start is called before the first frame update
        void Start()
        {
            Invoke("Respawn", deadTime);
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void Respawn()
        {
            PlayerInstance.localInstance.OpenSpawnMenu();

            Destroy(gameObject);
        }
    }
}