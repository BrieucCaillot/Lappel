using System.Collections.Generic;
using UnityEngine;

public class PenguinSpawner : MonoBehaviour
{
    [SerializeField] private GameObject penguinPrefab = null;
    [SerializeField] private GameObject destinations = null;
    [SerializeField] private int penguinsCount = 0;
    [SerializeField] private int rangeMin = -10;
    [SerializeField] private int rangeMax = 10;
    
    private GameObject[] penguins = null;
    
    private void Start()
    {
        for (var i = 0; i < penguinsCount; i++)
        {
            var position = new Vector3(Random.Range(rangeMin, rangeMax), 0, Random.Range(rangeMin, rangeMax));
            GameObject penguin = Instantiate(penguinPrefab, position, Quaternion.identity);
            penguin.GetComponent<PenguinController>().destinations = destinations;
            penguin.transform.SetParent(transform);
            // penguins[i] = penguin;
        }
    }
    
    public void DestroyAllPenguins()
    {
        print("DESTROY PENGUINS");
        // print(penguins.Length);
    }
}
