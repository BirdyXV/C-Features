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
        public GameObject explosionParticles;

        public override void Attack()
        {

        }
    }
}