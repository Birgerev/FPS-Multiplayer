using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;

public class NetworkDebug : NetworkBehaviour {

	// Use this for initialization
	void Start () {
        StartCoroutine(Debug());
	}
	
    IEnumerator Debug()
    {
        yield return new WaitForSeconds(2);
        print("Debug! "+ gameObject.name);
    }

    // Update is called once per frame
    void Update () {
		
	}
}
