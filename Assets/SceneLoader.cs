using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class SceneLoader : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        print("started lol");
        Scene scene = SceneManager.GetActiveScene();

        ConnectionManager.map = scene.name;
        ConnectionManager.host = true;

        SceneManager.LoadScene("cl_loadingmap");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
