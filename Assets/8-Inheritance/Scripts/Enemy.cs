using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Inheritence
{

    public class Enemy : MonoBehaviour
    {
        public int health = 100;
        public int damage = 10;
        public float attackRate = 5;
        public float attackRadius = 10f;

        private float attackTimer = 0f;

        void Update()
        {
            attackTimer += Time.deltaTime;
            if (attackTimer >= attackRate)
            {
                Attack();
                attackTimer = 0f;
            }
        }

        public virtual void Attack()
        {

        }
    }
}