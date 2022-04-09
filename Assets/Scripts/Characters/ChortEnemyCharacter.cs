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
        characterRb.AddForce(attackVector.normalized * attackForce * 500);
        isOnCooldown = true;

        yield return Timing.WaitForSeconds(attackCooldown);
        isOnCooldown = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null && collision.gameObject.CompareTag("PlayerHitbox"))
        {
            PlayerCharacter player = collision.gameObject.transform.root.GetComponent<PlayerCharacter>();
            player.ChangeHealth(-attackDamage);

            Vector2 knockbackDirection = -(collision.attachedRigidbody.position - characterRb.position).normalized;
            ApplyKnockback(knockbackDirection, attackForce / 1.2f);
        }
    }
}
