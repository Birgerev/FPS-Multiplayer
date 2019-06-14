using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace net.bigdog.game.player
{
    public class Hitbox : MonoBehaviour
    {

        public float damageMultiplier = 1;

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void hit(float damage, int damagerId)
        {
            if (gameObject.GetComponentInParent<Player>() == null)
                return;
            gameObject.GetComponentInParent<Player>().TakeDamage(damage * damageMultiplier, damagerId);
        }

        void OnCollisionEnter(Collision col)
        {

        }
    }
}
