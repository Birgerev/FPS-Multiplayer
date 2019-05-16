using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace net.bigdog.game.ui
{
    public class ProgressBar : MonoBehaviour
    {
        public float value = 0;

        public GameObject bar;

        // Start is called before the first frame update
        void Start()
        {
            bar = transform.Find("bar").gameObject;
        }

        // Update is called once per frame
        void Update()
        {
            print("progress");
            bar.transform.localScale = new Vector3(value, 1, 1);
            //transform.localScale = new Vector3(value, 1, 1);
        }
    }
}
