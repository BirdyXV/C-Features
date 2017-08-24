using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Breakout
{
    [RequireComponent(typeof(Renderer))] // Force Renderer component to be attached
    public class PinBall : MonoBehaviour
    {
        public float speed = 10f; // Speed of the ball
        public int scoreValue;
        private GameManager gameManager;

        private Vector3 velocity; // Speed X Direction

        void Start()
        {
            GameObject gameControllerObject = GameObject.FindWithTag("Block");
            if (gameControllerObject != null)
            {
                gameManager = gameControllerObject.GetComponent<GameManager>();
            }
            if (gameManager == null)
            {
                Debug.Log("Cant find script");
            }
        }

        float hitFactor(Vector2 ballPos, Vector2 wallPos, float wallHeight)
        {
            return (ballPos.y - wallPos.y) / wallHeight;
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

            
            if (other.gameObject.tag == "Block")
            {
                // Calculate hit Factor
                float y = hitFactor(transform.position,
                                    other.transform.position,
                                    other.collider.bounds.size.y);

                // Calculate direction, make length=1 via .normalized
                Vector2 dir = new Vector2(1, y).normalized;

                // Set Velocity with dir * speed
                GetComponent<Rigidbody2D>().velocity = dir * speed;


            }

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
        }
    }
}
