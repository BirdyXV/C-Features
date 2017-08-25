using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Breakout
{
    public class Paddle : MonoBehaviour
    {
        public float movementSpeed = 20f; // Speed the paddle moves
        public PinBall currentBall; // Ball that should be attached to the Paddle as a child
        public Vector2[] directions; // List of directions for the ball to choose from
        public bool isBallAttached;
        void Start()
        {
            // Grabs currentBall from children of the paddle
            currentBall = GetComponentInChildren<PinBall>();
            isBallAttached = true;
        }

        void Update()
        {
            Movement();
            CheckInput();
        }

        void Fire()
        {
            // Detach as child
            currentBall.transform.SetParent(null);
            // Generate random dir form list of directions
            Vector3 randomDir = directions[Random.Range(0, directions.Length)];
            // Fire off ball in randomDirection
            currentBall.Fire(randomDir);
            isBallAttached = false;
        }

        void CheckInput()
        {
            if (Input.GetKeyDown(KeyCode.Space) && isBallAttached)
            {
                Fire();
            }
          
        }

        void Movement()
        {
            // Get input on the horizontal axis
            float inputH = Input.GetAxis("Horizontal");
            // Set force to direction (inputH to decide which direction
            Vector3 force = transform.right * inputH;
            // Apply movement speed to force
            force *= movementSpeed;
            // Apply delta time to force
            force *= Time.deltaTime;
            // Add force to position
            transform.position += force;
        }

    }
}