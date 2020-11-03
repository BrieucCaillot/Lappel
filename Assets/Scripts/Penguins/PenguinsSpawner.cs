using UnityEngine;
using System.Collections.Generic;
 
public class PenguinsSpawner : MonoBehaviour
{
    public GameObject PrefabToSpawn;
    public int MaxNumberOfActors = 15;
    public float MinX = -2f;
    public float MaxX = 2f;
    public float MinY = -2f;
    public float MaxY = 4f;
 
    [Range(min: 0.1f, max: 1f)] // clamp Step to some reasonable values
    public float Step = 1f;
 
    private int activeActorCount;
    private List<Vector2> spawnablePositions;
 
    void Start()
    {
        activeActorCount = 0;
        spawnablePositions = new List<Vector2>();
    }
 
    void Update()
    {
        // untested
        // preferably move all this to a separate method and call only under some condition
        // f.e. time based
        // missing: actors should -- activeActorCount when they're destroyed
 
        if (activeActorCount >= MaxNumberOfActors)
        {
            return;
        }
 
        for (float x = MinX; x < MaxX; x += Step)
        {
            for (float y = MinY; x < MaxY; y += Step)
            {
                if (Physics.OverlapSphere(new Vector2(x, y), Step).Length == 0)
                {
                    spawnablePositions.Add(new Vector2(x, y));
                }
            }
        }
 
        if (spawnablePositions.Count == 0)
        {
            return; // nowhere to spawn
        }
 
        for (int i = activeActorCount; i <= MaxNumberOfActors; i++)
        {
            int randomPosition = Random.Range(0, spawnablePositions.Count);
            GameObject.Instantiate(PrefabToSpawn, spawnablePositions[randomPosition], Quaternion.identity);
            spawnablePositions.RemoveAt(randomPosition);
        }
 
        spawnablePositions.Clear();
    }
}