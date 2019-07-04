using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using net.bigdog.game.player;


public class PauseMenu : MonoBehaviour
{
    private PlayerInstanceInput input;

    // Start is called before the first frame update
    void Start()
    {
        input = PlayerInstance.localInstance.GetComponent<PlayerInstanceInput>();
    }

    // Update is called once per frame
    void Update()
    {
        SetMenu(input.input_Paused);
    }

    public void SetMenu(bool active)
    {
        CanvasGroup group = GetComponent<CanvasGroup>();
        
        if(active)
            PlayerInstanceInput.showMouse = true;
        if(group.interactable && !active)
            PlayerInstanceInput.showMouse = false;

        group.interactable = active;
        group.alpha = (active) ? 1 : 0;
        group.blocksRaycasts = active;
    }

    public void LeaveGame()
    {
        ConnectionManager.instance.LeaveServer();
    }
}
