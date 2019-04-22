using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnMenu : MonoBehaviour
{
    public GameObject spawnpointUiPrefab;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(updateMap());
    }

    // Update is called once per frame
    void Update()
    {
        PlayerInstanceInput.showMouse = true;

        if (PlayerInstance.localInstance.player != null)
            Destroy(gameObject);
    }

    IEnumerator updateMap()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            UpdateSpawnpoints();
        }
    }

    void UpdateSpawnpoints()
    {
        Spawnpoint[] spawnpoints = FindObjectsOfType<Spawnpoint>();

        //clear canvas
        for (int i = 0; i < transform.Find("Canvas").childCount; i++) {
            GameObject obj = transform.Find("Canvas").GetChild(i).gameObject;

            Destroy(obj);
        }

        foreach (Spawnpoint spawnpoint in spawnpoints)
        {
            GameObject obj = Instantiate(spawnpointUiPrefab);
            obj.transform.position = spawnpoint.transform.position;
            obj.transform.parent = transform.Find("Canvas");
        }
    }
}
