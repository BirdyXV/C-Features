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
            Enemy e = col.GetComponent<Enemy>();
            // IF e != null
            if (e != null)
            {
                // CALL e.DealDamage(damage) (DealDamage is from Enemy script
                e.DealDamage(damage);
                // Destroy the gameObject
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