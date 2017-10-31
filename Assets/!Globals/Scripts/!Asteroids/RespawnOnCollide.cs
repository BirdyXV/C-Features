using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Respawn))]
public class RespawnOnCollide : MonoBehaviour
{
    public string colliderTag;

    private Respawn res;
    // Use this for initialization
    void Awake()
    {
        res = GetComponent<Respawn>();
    }

    // Update is called once per frame
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.collider.tag == colliderTag)
        {
            res.Spawn();
        }
    }
}
