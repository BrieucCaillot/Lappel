using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PenguinSpawner : MonoBehaviour
{
    [SerializeField] private GameObject penguinPrefab = null;
    [SerializeField] private int penguinsCount = 0;
    [SerializeField] private int rangeMin = -10;
    [SerializeField] private int rangeMax = 10;
    
    private void Start()
    {
        for (var i = 0; i < penguinsCount; i++)
        {
            var position = new Vector3(Random.Range(rangeMin, rangeMax), 0, Random.Range(rangeMin, rangeMax));
            var penguin = Instantiate(penguinPrefab, position, Quaternion.identity);
            penguin.transform.parent = transform;
        }
    }
}
