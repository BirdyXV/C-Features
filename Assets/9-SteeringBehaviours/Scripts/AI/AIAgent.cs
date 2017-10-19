using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace AI
{
    public class AIAgent : MonoBehaviour
    {
        public float maxVelocity = 100f; // max amount of velocity
        public float maxDistance = 10f;

        [HideInInspector]
        public Vector3 velocity;

        private Vector3 force;
        // List of behaviours (i.e, Seek, Flee, Wander, etc)
        private List<SteeringBehaviour> behaviours;
        private NavMeshAgent nav;

        void Awake()
        {
            nav = GetComponent<NavMeshAgent>();
            behaviours = new List<SteeringBehaviour>(GetComponents<SteeringBehaviour>());
        }

        void ComputeForces()
        {
            // SET force to zero
            force = Vector3.zero;
            // FOR i :(defined to)= 0 < behaviours.Count
            for (int i = 0; i < behaviours.Count; i++)
            {
                // LET behaviour = behaviours[i]
                SteeringBehaviour behaviour = behaviours[i];
                // IF behaviour.isActiveAndEnabled == false
                if (behaviour.isActiveAndEnabled == false)
                {
                    // continue
                    continue;
                }
                // SET force = force + behaviour.GetForce()
                force = force + behaviour.GetForce();
                // IF force > maxVelocity
                if (force.magnitude > maxVelocity)
                {
                    // SET force = force.normalized * maxVelocity
                    force = force.normalized * maxVelocity;
                    // break
                    break;
                }
            }
        }

        void ApplyVelocity()
        {
            // SET velocity to velocity + force * deltaTime
            velocity = velocity + force * Time.deltaTime;
            // IF velocity.magnitude > maxVelocity
            if (velocity.magnitude > maxVelocity)
            {
                // SET velocity = velocity.normalized * maxVeloctiy
                velocity = velocity.normalized * maxVelocity;
            }
            // IF velocity.magnitude == 0
            if (velocity.magnitude > 0)
            {
                // SET transform.position = transform.position + velocity * deltaTime
                transform.position += velocity * Time.deltaTime;
                // SET transform.rotation = Quaternion LookRotation (velocity)
                transform.rotation = Quaternion.LookRotation(velocity);
            }
        }

        void FixedUpdate()
        {
            ComputeForces();
            ApplyVelocity();
        }
    }
}