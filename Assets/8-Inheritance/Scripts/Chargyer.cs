﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Inheritence
{
    public class Chargyer : Enemy
    {
        [Header("Splodey")]
        public float impactForce = 10f;
        public float knockback = 2f;

        public override void Attack()
        {

        }
    }
}