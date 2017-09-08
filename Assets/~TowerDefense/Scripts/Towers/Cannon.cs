using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefence
{

    public class Cannon : MonoBehaviour
    {
        public Transform barrel; // Reference to barrel where bullet will be shot from
        public GameObject projectilePrefab; // Prefab of projectile to instantiate when firing

        public void Fire(Enemy targetEnemy)
        {
            // LET targetPos = targetEnemy's position
            Vector3 targetPos = transform.position;
            // LET barrelPos = barrel's position
            Vector3 barrelPos = transform.position;
            // LET barrelRot = barrel's rotation
            Quaternion barrelRot = transform.rotation;
            // LET fireDirection = targetPos - barrelPos
            Vector3 fireDirection = targetPos - barrelPos;
            // SET cannon's rotation = Quarternion.LookRotation(fireDirection, Vector3.up)
            transform.rotation = Quaternion.LookRotation(fireDirection, Vector3.up);
            // LET clone = Instantiate(projectilePrefab, barrelPos, barrelRot
            GameObject clone = Instantiate(projectilePrefab, barrelPos, barrelRot);
            // LET p = clone's Projectile component
            Projectile p = clone.GetComponent<Projectile>();
            // SET p.direction = fireDirection
            p.direction = fireDirection;
        }
    }
}