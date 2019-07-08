using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleManager : MonoBehaviour
{
    public List<GameObject> vehicles = new List<GameObject>(); 
    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject vehicle in vehicles)
        {
            GetComponent<ConnectionManager>().spawnPrefabs.Add(vehicle);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
