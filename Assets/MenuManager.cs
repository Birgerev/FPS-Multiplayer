using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public static MenuManager instance;

    public GameProfile playerProfile;

    // Start is called before the first frame update
    void Start()
    {
        if (FindObjectsOfType<MenuManager>().Length > 1)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);

        instance = this;


        playerProfile = new GameProfile(1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
