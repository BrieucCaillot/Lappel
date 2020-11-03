using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PenguinsManager : Singleton<PenguinsManager>
{
    [Tooltip("Penguin prefab to instantiate")]
    public GameObject penguinPrefab;
    
    // Start is called before the first frame update
    void Start()
    {
        if (GameManager.Instance.DebugMode) return; 
        InstantiatePenguinsLocations();
    }

    private void InstantiatePenguinsLocations()
    {
        int locationsCount = transform.childCount;
        for (int i = 0; i < locationsCount; i++)
        {
            GameObject location = transform.GetChild(i).gameObject;
            Vector3 position = location.transform.position;
            
            Instantiate(penguinPrefab, position, Quaternion.Euler(0, 0, 0), transform);
            Destroy(location);
        }
    }
}
