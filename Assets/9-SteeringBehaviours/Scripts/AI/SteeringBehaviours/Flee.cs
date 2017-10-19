using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GGL;

namespace AI
{
    public class Flee : SteeringBehaviour
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
            Vector3 desiredForce = transform.position - target.position;
            // IF desiredForce.mangnitude > stoppingDistance 
            if (desiredForce.magnitude > stoppingDistance)
            {
                // SET desiredForce = desiredForce.normalized * weighting
                desiredForce = desiredForce.normalized * weighting;
                // SET force = desiredForce - owner.velocity
                force = desiredForce - owner.velocity;
            }

            #region GizmosGL
            GizmosGL.color = Color.red;
            GizmosGL.AddLine(transform.position, transform.position + force, 0.1f, 0.1f);
            GizmosGL.color = Color.green;
            GizmosGL.AddLine(transform.position, transform.position + desiredForce, 0.1f, 0.1f);
            #endregion
            // return the force
            return force;
        }
    }
}