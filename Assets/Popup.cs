using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Popup : MonoBehaviour
{
    public static string title = "";
    public static string text = "";

    public Text ui_title;
    public Text ui_text;

    private void Update()
    {
        ui_title.text = title;
        ui_text.text = text;

        GetComponent<CanvasGroup>().alpha = (text == "") ? 0 : 1;
    }
}
