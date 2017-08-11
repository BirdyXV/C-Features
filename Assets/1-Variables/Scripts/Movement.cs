using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Variables
{
    public class Movement : MonoBehaviour
    {
        public float movementSpeed = 20f;
        public float rotationSpeed = 20f;
        public Vector3 rotateDir = Vector3.forward;

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            // Get horizontal input
            float inputH = Input.GetAxis("Horizontal");
            // Get vertical input
            float inputV = Input.GetAxis("Vertical");
            // Move the player
            // Direction x Input(sign) x speed x DeltaTime
            transform.position += transform.right * inputV * movementSpeed * Time.deltaTime;

            // Direction x Input(sign) x speed x DeltaTime
            transform.eulerAngles += rotateDir * inputH * rotationSpeed * Time.deltaTime;
        }
    }
}
