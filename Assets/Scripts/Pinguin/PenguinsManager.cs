using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PenguinsManager : Singleton<PenguinsManager>
{
    public GameObject penguin;
    
    // Start is called before the first frame update
    void Start()
    {
        InstantiatePenguinsLocations();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void InstantiatePenguinsLocations()
    {
        var locations = GameObject.FindGameObjectsWithTag("Penguin Location");

        foreach (var location in locations)
        {
            Instantiate(penguin, location.transform.position, Quaternion.Euler(0, 0, 0), location.transform.parent);
            Destroy(location.gameObject);
        }
    }
}
