using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicalProjectile : Projectile
{

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);

        if (collision != null && collision.gameObject.name == "ProjectileCollider")
        {
            projectileRb.velocity = Vector3.zero;
            isDamaging = false;
        }
    }
}
