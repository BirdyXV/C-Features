using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefence
{

    public class Tower : MonoBehaviour
    {
        public Cannon cannon; // Reference to cannon inside of tower
        public float attackRate = 0.25f; // Rate of attack in seconds
        public float attackRadius = 5f; // Distance of atack in world units
        public float minDistance = 0.5f;
        public float distance = 1f;

        private float attackTimer = 0f; // Timer to count up to attackRate
        private List<Enemy> enemies = new List<Enemy>(); // List of enemies within radius

        void OnTriggerEnter(Collider col)
        {
            // LET e = col's Enemy component
            Enemy e = col.GetComponent<Enemy>();
            // IF e != null
            if (e != null)
            {
                // Add e to enemies list
                enemies.Add(e);
            }
        }

        void OnTriggerExit(Collider col)
        {
            // LET e = col's Enemy component
            Enemy e = col.GetComponent<Enemy>();
            // IF e != null
            if (e != null)
            {
                // Remove e from enemies list
                enemies.Remove(e);
            }


        }

        Enemy GetClosestEnemy()
        {
            // SET enemies = RemoveAllNulls(enemies)
            enemies = RemoveAllNulls(enemies);
            // LET closest = null
            Enemy closest = null;
            // LET minDistance = float.maxValue
            minDistance = float.MaxValue;
            // FOREACH enemy in enemies
            foreach (var Enemy in enemies)
            {
                // LET distance = the distance between transform's position and enemy's position
                distance = Vector3.Distance(transform.position, Enemy.transform.position);
                // IF distance < minDistance
                if (distance < minDistance)
                {
                    // SET minDistance = distance
                    minDistance = distance;
                    // SET closest = enemy
                    closest = Enemy;
                }
            }
            return closest;
        }

        void Attack()
        {
            // LET closest to GetClosestEnemy();
            Enemy closest = GetClosestEnemy();

            // IF closest != null
            if (closest != null)
            {
                // CALL cannon.Fire() and pass closest as argument
                cannon.Fire(closest);
            }
        }

        void Update()
        {
            // SET attackTimer = attackTimer + deltaTime
            attackTimer = attackTimer + Time.deltaTime;
            // IF attack >= attackRate
            if (attackTimer >= attackRate)
            {
                // CALL Attack()
                Attack();
                // SET attackTimer = 0
                attackTimer = 0;
            }
        }

        List<Enemy> RemoveAllNulls(List<Enemy> listWithNulls)
        {
            // LET listWithoutNulls = new list
            List<Enemy> listWithoutNulls = new List<Enemy>();

            // Loop through entire listWithNulls
            foreach (var Enemy in listWithNulls)
            {
                
            }
            // IF element is NOT null
            if ()
            {
                // Add element to listWithoutNulls
            }

            // RETURN listWithoutNulls
            return listWithNulls;
        }
    }
}