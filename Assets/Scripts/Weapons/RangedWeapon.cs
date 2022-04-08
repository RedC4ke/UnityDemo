using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedWeapon : Weapon
{
    [SerializeField] string projectileTag;
    GameObject projectileSpawner;

    [SerializeField] float projectileSpeed = 200f;

    private void Start()
    {
        projectileSpawner = transform.Find("ProjectileSpawner").gameObject;
    }

    protected override void Attack()
    {
        Vector3 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float rotation = (Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg) - 90f;

        GameObject projectileObject = ObjectPooler.Instance.SpawnFromPool(projectileTag, projectileSpawner.transform.position, Quaternion.Euler(0f, 0f, rotation));
        Projectile projectile = projectileObject.GetComponent<Projectile>();

        projectile.Launch(projectileSpeed, direction, this);
    }
}
