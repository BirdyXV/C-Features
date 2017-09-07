using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefence
{

    public class Projectile : MonoBehaviour
    {
        public float damage = 50f; // Damage dealt to whatever gets hit
        public float speed = 50f; // Speed the projectile travels
        public Vector3 direction; // Direction the projectile travels
     
        void Update()
        {
            // LET velocity = direction.normalized x speed
            Vector3 velocity = direction.normalized * speed;
            // SET projectile's position += velocity x deltaTime
            transform.position += velocity * Time.deltaTime;
        }

        void OnTriggerEnter(Collider col)
        {
            // LET e = col's Enemy component
            
            // IF e != null
            if (e != null)
            {
                e.DealDamage(damage);
                Destroy(gameObject);                    
            }

            // IF col's name == "Ground"
            if (col.name == "Ground")
            {
                // Destroy the projectile
                Destroy(gameObject);
            }
        }
    }
}