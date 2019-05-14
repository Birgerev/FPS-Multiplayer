using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpineRotation : MonoBehaviour {

    public Transform spine1;
    public Transform spine2;
    public Transform spine3;

    public float spine1Weight;
    public float spine2Weight;
    public float spine3Weight;

    public float spine1Offset;
    public float spine2Offset;
    public float spine3Offset;

    public float pitch;
    public float pitchOffset;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        float totalWeight = spine1Weight + spine2Weight + spine3Weight;
        //(spine / totalWeight) * pitch
        spine1.localEulerAngles = new Vector3(spine1.localEulerAngles.x, spine1.localEulerAngles.y, spine1Offset - ((spine1Weight / totalWeight) * (pitchOffset - pitch)));
        spine2.localEulerAngles = new Vector3(spine2.localEulerAngles.x, spine2.localEulerAngles.y, spine2Offset + ((spine1Weight / totalWeight) * (pitchOffset - pitch)));
        spine3.localEulerAngles = new Vector3(spine3.localEulerAngles.x, spine3.localEulerAngles.y, spine3Offset - ((spine1Weight / totalWeight) * (pitchOffset - pitch)));

    }
}
