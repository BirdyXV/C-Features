using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PaperMarioClone
{
    [RequireComponent(typeof(PlayerController))]
    public class Mario : MonoBehaviour
    {
        public float rayDistance;

        private PlayerController pC;
        private Ray stompRay;

        private void OnDrawGizmos()
        {
            RecalculateRay();
            Gizmos.color = Color.cyan;
            Gizmos.DrawLine(stompRay.origin, stompRay.origin + stompRay.direction * rayDistance);
        }
        void Awake()
        {
            pC = GetComponent<PlayerController>();
        }

        void Update()
        {
            if (!pC.isGrounded)
            {
                CheckStomp();
            }
        }

        void RecalculateRay()
        {
            stompRay = new Ray(transform.position, Vector3.down);
        }

        void CheckStomp()
        {
            // Perform raycast 
            RaycastHit hit;
            if (Physics.Raycast(stompRay, out hit, rayDistance))
            {
                // Are we are on top of enemy
                Enemy e = hit.collider.GetComponent<Enemy>();
                if (e != null)
                {
                    // Add force up
                    e.Damage();
                }
            }
        }
    }
}