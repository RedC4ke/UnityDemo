using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    protected Rigidbody2D projectileRb;
    [SerializeField] float damageMultiplier;
    [SerializeField] float knockbackMultiplier;
    protected bool isDamaging = true;

    RangedWeapon usedWeapon;
    private Vector3 projectileDirection;


    // Start is called before the first frame update
    void Awake()
    {
        projectileRb = GetComponent<Rigidbody2D>();
    }

    public void Launch(float force, Vector3 direction, RangedWeapon weapon)
    {
        usedWeapon = weapon;
        projectileDirection = direction;
        projectileRb.AddForce(direction * force);
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null && collision.gameObject.CompareTag("EnemyHitbox") && isDamaging) {
            Debug.Log("Hit!");
            // Deactivate projectile
            gameObject.SetActive(false);
            projectileRb.velocity = Vector3.zero;

            // Damage enemy
            EnemyCharacter enemy = collision.gameObject.transform.root.GetComponent<EnemyCharacter>();
            float finalDamage = usedWeapon.damage * damageMultiplier;
            enemy.ChangeHealth(-finalDamage);

            // Apply knockback
            enemy.ApplyKnockback(projectileDirection, usedWeapon.knockbackForce * knockbackMultiplier);
            
            // Set attack direction on enemy character
            enemy.lastTakenHitDirection = projectileDirection;
        }

    }
}
