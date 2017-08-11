using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float spawnRate = 1f;
    public float spawnRadius = 1f;
    public float force = 300f;

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, spawnRadius);
    }

    // Use this for initialization
    void Start()
    {
        //InvokeRepeating(functionName, time, repeatRate
        //functionName    = name of the function you want to call
        //time            = delay for when the function gets called the first time
        //repeatRate      = how often the function gets called
        InvokeRepeating("Spawn", 0, spawnRate);
    }

    void Spawn()
    {
        // Instantiate a new GameObject
        GameObject enemy = Instantiate(enemyPrefab);
        // Position to a random place with in the spawn radius
        enemy.transform.position = Random.insideUnitCircle * spawnRadius;
        // Apply force to a rigidbody 
    }
}
