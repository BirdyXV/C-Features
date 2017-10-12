using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI
{
    public class Seek : SteeringBehaviour
    {
        public Transform target;
        public float stoppingDistance = 1f;

        public override Vector3 GetForce()
        {
            // LET force = vector3.zero
            Vector3 force = Vector3.zero;
            // IF target == null
            if (target == null)
            {
                // return force
                return force;
            }
            // LET desiredForce = target's position - transform's position
            Vector3 desiredForce = target.position - transform.position;
            // IF desiredForce.mangnitude > stoppingDistance 
            if (desiredForce.magnitude > stoppingDistance)
            {
                // SET desiredForce = desiredForce.normalized * weighting
                desiredForce = desiredForce.normalized * weighting;
                // SET force = desiredForce - owner.velocity
                force = desiredForce - owner.velocity;
            }
            // return the force
            return force;
        }
    }
}