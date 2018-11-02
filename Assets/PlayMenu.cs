using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class PlayMenu : MonoBehaviour {

    public InputField IpInput;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Host()
    {
        print("host");
        ConnectionManager.host = true;
        ConnectionManager.ip = IpInput.text;
        SceneManager.LoadScene("Loading");
    }

    public void Join()
    {
        ConnectionManager.host = false;
        ConnectionManager.ip = IpInput.text;
        SceneManager.LoadScene("Loading");
    }
}
