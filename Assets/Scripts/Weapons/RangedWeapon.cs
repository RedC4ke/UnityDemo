using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedWeapon : Weapon
{
    [SerializeField] string projectileTag;
    GameObject projectileSpawner;
    Transform crosshair;

    [SerializeField] float projectileSpeed = 2000f;

    private void Start()
    {
        projectileSpawner = transform.Find("ProjectileSpawner").gameObject;
        crosshair = transform.parent.Find("Crosshair");
    }

    protected override void Attack()
    {
        Vector3 direction = (crosshair.position - transform.position).normalized;
        float rotation = (Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg) - 90f;

        GameObject projectileObject = ObjectPooler.Instance.SpawnFromPool(projectileTag, projectileSpawner.transform.position, Quaternion.Euler(0f, 0f, rotation));
        Projectile projectile = projectileObject.GetComponent<Projectile>();

        projectile.Launch(projectileSpeed, direction, this);
    }
}
