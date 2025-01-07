using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfiniteLevelLoop : MonoBehaviour
{
    public GameObject obstaclePrefab;
    public GameObject Player;
    public float spawnInterval = 2f;

    void Start()
    {
        InvokeRepeating(nameof(SpawnObstacle), 2f, spawnInterval);
    }

    void SpawnObstacle()
    {
        Instantiate(obstaclePrefab, new Vector3(Player.transform.position.x + 10f, Random.Range(-1f, 1f), 0f), Quaternion.identity);
    }
}
