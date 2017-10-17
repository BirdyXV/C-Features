using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Inheritence
{
    [RequireComponent(typeof(Rigidbody))]
    public class Enemy : MonoBehaviour
    {
        public Transform target;
        public int health = 100;
        public int damage = 10;
        public float attackDuration = 2f;
        public float attackRate = 5;
        public float attackRadius = 10f;


        private float attackTimer = 0f;
        protected NavMeshAgent nav;
        protected Rigidbody rigid;

        protected virtual void Awake()
        {
            nav = GetComponent<NavMeshAgent>();
            rigid = GetComponent<Rigidbody>();
        }

        protected virtual void Attack()
        {

        }

        protected virtual void OnAttackEnd()
        {

        }

        IEnumerator AttackDelay(float delay)
        {
            // STOP nav
            nav.Stop();
            yield return new WaitForSeconds(delay);
            // RESUME nav
            nav.Resume();
            // CALL OnAttackEnd
            OnAttackEnd();
        }

        protected virtual void Update()
        {
            if (target == null)
            {
                return;
            }
            // Set navigation to follow target
            nav.SetDestination(target.position);

            // If timer reaches attack rate
            attackTimer += Time.deltaTime;
            if (attackTimer >= attackRate)
            {
                float distance = Vector3.Distance(transform.position, target.position);
                // IF distance is within attack range
                if (distance <= attackRadius)
                {
                    // CALL Attack()
                    Attack();
                    // Reset attackTimer 
                    attackTimer = 0f;
                    // StartCoroutine AttackDelay
                    StartCoroutine(AttackDelay(attackDuration));
                }
            }
        }
    }
}