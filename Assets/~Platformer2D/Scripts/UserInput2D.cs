using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer2D
{
    // Forces unity to add 'Controller2D' component to 
    // the same object as 'UserInput2D' as soon as
    // you attach this script to a GameObject
    [RequireComponent(typeof(Controller2D))]
    public class UserInput2D : MonoBehaviour
    {
        // Variable to store the Controller2D component
        private Controller2D controller;

        // Use this for initialization
        void Awake()
        {
            controller = GetComponent<Controller2D>();
        }

        // Update is called once per frame
        void Update()
        {
            // Get input on the horizontal axis (A or D)
            float inputH = Input.GetAxis("Horizontal");
            // Move controller using input axis
            controller.Move(inputH);
            // Check if space is down
            if (Input.GetKeyDown(KeyCode.Space)
                && controller.isGrounded)
            {
                // Get player to jump
                controller.Jump();
            }
        }
    }
}