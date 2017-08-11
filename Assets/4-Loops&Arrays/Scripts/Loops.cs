using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LoopsArrays
{
    public class Loops : MonoBehaviour
    {
        public int spawnAmount = 10;
        public float amplitude = 6;
        public float frequency = 3;
        public GameObject[] spawnPrefabs;
        public float spawnRadius = 5f;
        public string message = "Print This";
        public float printTime = 2f;

        private float timer = 0;

        void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(transform.position, spawnRadius);
        }

        // Use this for initialization
        void Start()
        {
            /* While (condition)
            {
                // Statement
            }
            */
            // Infinite loop
            SpawnObjectsWithSine();
        }
        // Update is called once per frame
        void Update()
        {
            // Count up timer in seconds 
            timer += Time.deltaTime;
            // Loop through until timer gets to printTime
            while (timer <= printTime) // STICK AROUND
            {
                print("PUT THE COOKIE DOWN!");
                // Count up timer in seconds 
                timer += Time.deltaTime;
            }
        }
        void SpawnObjectsWithSine()
        {
            {
                /*
                for(initialization; condition; iteration)
                {
                    // Statement(s)
                }
               */
                for (int i = 0; i < spawnAmount; i++)
                {
                    // Spawned new GameObject
                    int randomIndex = Random.Range(0, spawnPrefabs.Length);
                    // Store randomly selected prefab 
                    GameObject randomPrefab = spawnPrefabs[randomIndex];
                    // Instantiate randomly selected prefab
                    GameObject clone = Instantiate(randomPrefab);
                    // Grab the mesh renderer 
                    MeshRenderer rend = clone.GetComponent<MeshRenderer>();
                    // Change the color 
                    float r = Random.Range(0, 2);
                    float g = Random.Range(0, 2);
                    float b = Random.Range(0, 2);
                    float a = 1;
                    rend.material.color = new Color(r, g, b, a);
                    // Generated random position within circle (aka sphere)
                    float x = Mathf.Sin(i * frequency) * amplitude;
                    float y = i;
                    float z = 0;
                    Vector3 randomPos = transform.position + new Vector3(x, y, z);
                    // Cancel out the Z
                    randomPos.z = 0;
                    // Set spawned object's position
                    clone.transform.position = randomPos;
                }
            }
        }
        void SpawnObjects()
        {

        }
    }
}
