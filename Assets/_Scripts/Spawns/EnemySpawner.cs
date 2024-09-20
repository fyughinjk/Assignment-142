using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    public Transform[] enemySpawnPoints;


    public GameObject enemyPrefab;


    public float enemySpawnInterval = 2f;

    void Start()
    {

        StartCoroutine(SpawnEnemies());
    }


    IEnumerator SpawnEnemies()
    {
        while (true)
        {
            yield return new WaitForSeconds(enemySpawnInterval);


            int randomIndex = Random.Range(0, enemySpawnPoints.Length);


            Instantiate(enemyPrefab, enemySpawnPoints[randomIndex].position, enemySpawnPoints[randomIndex].rotation);
        }
    }
}
