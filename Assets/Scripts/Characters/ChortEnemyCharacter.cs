using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MEC;

public class ChortEnemyCharacter : EnemyCharacter
{
    [SerializeField] float attackForce = 100f;

    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    protected override IEnumerator<float> _Attack(Vector2 attackVector)
    {
        isOnCooldown = true;
        // apply some wait time to make attack more dodgeable
        yield return Timing.WaitForSeconds(0.2f);
        characterRb.AddForce(attackVector.normalized * attackForce * 500);

        yield return Timing.WaitForSeconds(attackCooldown);
        isOnCooldown = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision != null && collision.gameObject.CompareTag("Player"))
        {
            PlayerCharacter player = collision.gameObject.GetComponent<PlayerCharacter>();
            player.ChangeHealth(-attackDamage);

            Vector2 knockbackDirection = -(player.characterRb.position - characterRb.position).normalized;
            ApplyKnockback(knockbackDirection, attackForce / 1.2f);
        }
    }
}
