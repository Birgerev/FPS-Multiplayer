using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using net.bigdog.game.player;

public class Interactable : MonoBehaviour
{
    public virtual string popup_title { get; } = "Interactable";
    public virtual string popup_text { get; } = "Press F To Use";

    public Player triggered_player = null;
    private bool lastFrameInput = false;

    // Start is called before the first frame update
    public virtual void Start()
    {
        if (GetComponent<Collider>() == null)
            Debug.LogError(gameObject.name + " interactable has no Collider");
        if (GetComponent<Collider>().isTrigger == false)
            Debug.LogError(gameObject.name + " interactable: Collider must be trigger!");
    }

    // Update is called once per frame
    public virtual void Update()
    {
        if (triggered_player != null)
        {
            if(triggered_player.networkInstance.isLocalPlayer)
                showText();
            checkInput();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<Player>() != null)
        {
            triggered_player = other.GetComponent<Player>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Player>() == triggered_player)
        {
            triggered_player = null;
            hideText();
        }
    }

    private void showText()
    {
        Popup.title = popup_title;
        Popup.text = popup_text;
    }

    private void hideText()
    {
        Popup.title = "";
        Popup.text = "";
    }


    private void checkInput()
    {
        if (triggered_player.networkInstance != null)
        {
            if (!triggered_player.networkInstance.input.interact && lastFrameInput)
            {
                Interact();
            }

            lastFrameInput = triggered_player.networkInstance.input.interact;
        }
    }

    public virtual void Interact()
    {

    }
}
