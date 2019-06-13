using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ProfileUIField : MonoBehaviour
{
    public Text ProfileName;
    public Image ProfileIcon;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ProfileName.text = MenuManager.instance.playerProfile.name;
        ProfileIcon.sprite = MenuManager.instance.playerProfile.getIcon();
    }
}
