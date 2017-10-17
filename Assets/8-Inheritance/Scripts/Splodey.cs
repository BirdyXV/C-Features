using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Inheritence
{

    public class Splodey : Enemy
    {
        [Header("Splodey")]
        public float explosionRadius = 0.5f;
        public float impactForce = 5f;
        public float explosionRate = 1f;
        public GameObject explosionParticles;

        private float explosionTimer = 0f;

        protected override void Awake()
        {
            base.Awake();
        }
        protected override void Update()
        {
            base.Update();

            // START Explosion Timer
            explosionTimer += Time.deltaTime;
        }
        protected override void Attack()
        {
            // IF Explosion Timer reaches rate
            if (explosionTimer >= explosionRate)
            {
                // CALL Splode
                Splode();
            }
        }

        protected override void OnAttackEnd()
        {
            // RESET Expolision Timer
            explosionTimer = 0f;
        }

        public void Splode()
        {
            // Perform overlap sphere
            Collider[] hits = Physics.OverlapSphere(transform.position, explosionRadius);

            // FOREACH hit in hits
            foreach (Collider hit in hits)
            {
                Health h = hit.GetComponent<Health>();
                // IF hit player
                if (h != null)
                {
                    // Decrease health from player
                    h.TakeDamage(damage);
                    rigid.AddExplosionForce(impactForce, transform.position, explosionRadius);
                }
            }

        }
    }
}