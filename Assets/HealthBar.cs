using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

namespace net.bigdog.game.player.ui
{
    public class HealthBar : MonoBehaviour
    {
        public float value;
        public Image filler;
        public Color damagedColor;

        private float colorChangeTime = 2;
        private float lastValue;

        private float barVelocity;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            value = PlayerUI.instance.playerInstance.player.health / 100;

            float visualValue = value;
            bool valueChanged = (lastValue != value);

            //Colour
            if (valueChanged)
                filler.color = damagedColor;
            else
                filler.color = Color.Lerp(filler.color, Color.white, colorChangeTime * Time.deltaTime);
            
            //Apply value
            filler.fillAmount = visualValue;


            //update last value for next frame
            lastValue = value;
        }
    }
}
