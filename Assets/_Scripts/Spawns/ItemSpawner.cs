using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    // Array to store the spawn points for items
    public Transform[] itemSpawnPoints;

    // Item prefab to spawn
    public GameObject itemPrefab;

    // Whether the item has already been spawned
    private bool itemSpawned = false;

    void Start()
    {
        // Spawn the item once at a random spawn point
        SpawnItem();
    }

    // Function to handle spawning the item at a random spawn point
    void SpawnItem()
    {
        if (!itemSpawned)
        {
            // Pick a random spawn point from the array
            int randomIndex = Random.Range(0, itemSpawnPoints.Length);

            // Spawn the item at the selected spawn point
            Instantiate(itemPrefab, itemSpawnPoints[randomIndex].position, itemSpawnPoints[randomIndex].rotation);

            // Mark item as spawned
            itemSpawned = true;
        }
    }
}
