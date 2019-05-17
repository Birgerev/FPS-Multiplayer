using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

namespace net.bigdog.menu.resultScreen
{
    public class ResultScreen : MonoBehaviour
    {
        public string mainMenuScene;

        // Start is called before the first frame update
        void Start()
        {
            Invoke("shutdownServer", 1);
        }

        private void shutdownServer()
        {
            ConnectionManager.instance.CloseServer();
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void ReturnToMenu()
        {
            SceneManager.LoadScene(mainMenuScene);
        }
    }
}