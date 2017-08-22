using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Breakout
{
    [RequireComponent(typeof(Renderer))] // Force Renderer component to be attached
    public class PinBall : MonoBehaviour
    {
        public float speed = 10f; // Speed of the ball

        private Renderer rend; // Renderer attached to the object
        private Camera cam; // Camera container (variable)
        private Bounds camBounds; // Camera Bounds structure
        private float camWidth, camHeight;
        private Vector3 velocity; // Speed X Direction

        void Start()
        {
            // Set Camera variable to main camera 
            cam = Camera.main;
            // Get the Renderer component attached to this object
            rend = GetComponent<Renderer>();
        }

        // Fires off ball in a given direction
        public void Fire(Vector3 direction)
        {
            // Calculate velocity
            velocity = direction * speed;
        }

        // Detect collision
        void OnCollisionEnter2D(Collision2D other)
        {
            // Grab contact point of collision
            ContactPoint2D contact = other.contacts[0];
            // Calculate the reflection point of the ball using velocity & contact normal
            Vector3 reflect = Vector3.Reflect(velocity, contact.normal);
            // Calculate new velocity from reflection multiply by the same speed (velocity.magnitude)
            velocity = reflect.normalized * velocity.magnitude;
            // Destorys Blocks when the ball collides with it
            if (other.gameObject.tag == "Block")
            {
                Destroy(other.gameObject);
            }

        }
        // Update is called once per frame
        private void Update()
        {
            // Moves ball using velocity & deltaTime
            transform.position += velocity * Time.deltaTime;

            // Update the camera bounds
            UpdateCamBounds();
            // Set the position after checking bounds
            transform.position = CheckBounds();
        }

        // Updates the camBounds variable with the camera values
        void UpdateCamBounds()
        {
            // Calculate camera bounds
            camHeight = 2f * cam.orthographicSize; // height = 2 x orthographic size
            camWidth = camHeight * cam.aspect; // width = height x aspect
            camBounds = new Bounds(cam.transform.position, new Vector3(camWidth, camHeight));
        }

        Vector3 CheckBounds()
        {
            Vector3 pos = transform.position;
            Vector3 size = rend.bounds.size;
            float halfWidth = size.x * 0.5f;
            float halfHeight = size.y * 0.5f;
            float halfCamWdith = camWidth * 0.5f;
            float halfCamHeight = camHeight * 0.5f;

            // Check left 
            if (pos.x - halfWidth < camBounds.min.x)
            {
                pos.x = camBounds.min.x + halfWidth;
            }

            // Check right
            if (pos.x + halfWidth > camBounds.max.x)
            {
                pos.x = camBounds.max.x - halfWidth;
            }



            // Check up 
            if (pos.y + halfHeight > camBounds.max.y)
            {
                pos.y = camBounds.max.y - halfHeight;
            }
            return pos; // Returns adjusted position
        }
    }
}

