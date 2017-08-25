using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Billiards
{
    public class Ball : MonoBehaviour
    {

        public float stopSpeed = 0.2f;
        public bool GameObject;

        private Rigidbody rigid;
        // Use this for initialization
        void Start()
        {
            rigid = GetComponent<Rigidbody>();
            GameObject = true;
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            // Creating velocity variable called vel
            Vector3 vel = rigid.velocity;

            // Check if velocity is going up
            if (vel.y > 0)
            {
                // Cap velocity
                vel.y = 0;
            }

            // IF the velocity's speed is less than the stop speed
            if (vel.magnitude < stopSpeed)
            {
                // Cancel out velocity
                vel = Vector3.zero;
            }

            rigid.velocity = vel;
        }

        public void Hit(Vector3 dir, float impactForce)
        {
            rigid.AddForce(dir * impactForce, ForceMode.Impulse);
        }

        void Update()
        {
            if (transform.position.y < -50)
            {
                Destroy(gameObject);
                
            }
            
        }
    }
}