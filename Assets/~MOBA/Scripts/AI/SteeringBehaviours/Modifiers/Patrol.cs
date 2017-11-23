using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MOBA
{
    [RequireComponent(typeof(PathFollowing))]
    public class Patrol : MonoBehaviour
    {
        public AIAgentPatrolSelector patrolSelector;// The selector that is guiding the agent

        private int currentPoint = 0; // The current patrol point the agent is PathFollowing to
        private PathFollowing pathFollowing; // Reference to the attached PathFollowing script
        private List<Transform> patrolPoints; // List of patrol point (referring to the one in the patrolSelector)

        void Start()
        {
            pathFollowing = GetComponent<PathFollowing>();
        }

        void Update()
        {
            // Is there currently a patrol selector?
            if (patrolSelector != null)
            {
                // Grab the patrol points list from selecter
                patrolPoints = patrolSelector.patrolPoints;
                // Is there any patrol points added from the selector
                if (patrolPoints.Count > 0)
                {
                    // Reset the currentNode the agent seeks to
                    pathFollowing.currentNode = 0;
                    // Move to the next patrol point
                    currentPoint++;
                }
                // Is currentPoint outside of list count?
                if (currentPoint >= patrolPoints.Count)
                {
                    // Loop back at start
                    currentPoint = 0;
                }
                Transform point = patrolPoints[currentPoint];
                pathFollowing.target = point;
            }
        }
    }
}