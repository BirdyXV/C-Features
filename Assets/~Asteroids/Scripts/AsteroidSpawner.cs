using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Asteroids
{

    public class AsteroidSpawner : MonoBehaviour
    {
        public GameObject[] asteroidPrefab;
        public float spawnRate = 1f;
        public float spawnRadius = 5f;
        // Use this for initialization
        void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, spawnRadius);
        }

        void Spawn()
        {
            Vector3 rand = Random.insideUnitSphere * spawnRadius;
            rand.z = 0f;
            Vector3 position = transform.position + rand;
            int randIndex = Random.Range(0, asteroidPrefab.Length);
            GameObject randAsteroid = asteroidPrefab[randIndex];
            GameObject clone = Instantiate(randAsteroid);
            clone.transform.position = position;
        }

        void Start()
        {
            InvokeRepeating("Spawn", 0f, spawnRate);
        }
    }
}