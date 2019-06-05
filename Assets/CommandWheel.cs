using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class CommandWheel : MonoBehaviour
{
    public List<Command> commands = new List<Command>();
    public GameObject sectionPrefab;

    // Start is called before the first frame update
    void Start()
    {
        float degreesPerCommand = 360 / commands.Count;

        int index = 0;
        foreach(Command command in commands)
        {
            Transform obj = Instantiate(sectionPrefab).transform;
            obj.parent = transform;
            obj.localPosition = Vector3.zero;
            obj.localRotation = Quaternion.Euler(0, 0, degreesPerCommand*index);

            obj.Find("icon").GetComponent<Image>().sprite = command.icon;
            obj.Find("title").GetComponent<Text>().text = command.title;

            index++;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

[System.Serializable]
public class Command
{
    public Sprite icon;
    public string title;
}
