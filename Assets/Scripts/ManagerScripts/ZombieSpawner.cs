using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    [SerializeField]
    private int numberOfZombiesToSpawn;
    public GameObject[] zombiePrefab;
    public SpawnerVolume[] spawnVolumes;

    private void Start()
    {
        for (int i = 0; i < numberOfZombiesToSpawn; i++)
        {
            SpawnZombie();
        }
    }

    private void SpawnZombie()
    {
        GameObject zombieToSpawn = zombiePrefab[Random.Range(0, zombiePrefab.Length)];
        SpawnerVolume spawnVolume = spawnVolumes[Random.Range(0, spawnVolumes.Length)];

        // object pooling can be referenced here
        GameObject zombie = Instantiate(zombieToSpawn, spawnVolume.GetPositionInBounds(), spawnVolume.transform.rotation);
    }
}
