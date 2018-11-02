using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Menu
{
    public class Startup : MonoBehaviour
    {
        public List<string> texts = new List<string>();
        public List<float> textSizes = new List<float>();

        public float textDuration = 3;

        public string nextScene = "Menu";

        public Text text;
        public Transform panel;

        private int textindex;

        // Use this for initialization
        void Start()
        {
            if (Debug.isDebugBuild)
                textDuration = 1f;

            StartCoroutine(loop());
        }

        IEnumerator loop()
        {
            while (true)
            {
                newText();
                yield return new WaitForSeconds(textDuration);
            }
        }

        private void newText()
        {
            if(textindex >= texts.Count)
            {
                loadMenu();
                return;
            }
            text.text = texts[textindex];
            panel.localScale = new Vector3(textSizes[textindex], panel.localScale.y, panel.localScale.z);

            textindex++;
        }

        public void loadMenu()
        {
            SceneManager.LoadScene(nextScene);
        }
    }
}
