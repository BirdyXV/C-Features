using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace TowerDefence
{
    public class AIAgentSpawner : MonoBehaviour
    {
        public GameObject aiAgentPrefab; // Prefab of AI Agent
        public Transform target; //Target that each AI Agent should travel to
        public float spawnRate = 1f; // Rate of spawn
        public float spawnRadius = 1f; // Radius of spawn


        // Visualization code
        void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            // Draw a sphere to indicate the spawn radius
            Gizmos.DrawWireSphere(transform.position, spawnRadius);
        }

        void Spawn()
        {

            // LET - means to create a new variable at that line
            // SET - means to modify the variable that already exists

            // LET clone = new instance of aiAgentPrefab
            GameObject clone = Instantiate(aiAgentPrefab);
            // LET rand = Random Inside Unit Sphere
            Vector3 rand = Random.insideUnitSphere;
            // SET rand.y = 0
            rand.y = 0;
            // SET clone's position to transform's position + rand x spawnRadius
            clone.transform.position = transform.position + rand * spawnRadius;
            // SET aiAgent = clone's AIAgent component
            AIAgent aiAgent = clone.GetComponent<AIAgent>();
            // SET aiAgent.target = target
            aiAgent.target = target;
        }

        void Start()
        {
            // InvokeRepeating(functionName, time, repeatRate)        
            // functionName = name of the function you want to call in the class
            // time         = delay for when the function gets called the first time
            // repeatRate   = how often the function gets called
            InvokeRepeating("Spawn", 0, spawnRate);
        }
    }
}