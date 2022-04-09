using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour
{
    [SerializeField] float maxHealth;
    public float currentHealth { get; private set; }
    [SerializeField] protected float movementSpeed;
    bool isMoving = false;
    public float lookDirection { get; protected set; }

    protected Rigidbody2D characterRb;
    protected Animator characterAnim;
    protected SpriteRenderer characterRenderer;

    protected virtual void Start()
    {
        characterRb = GetComponent<Rigidbody2D>();
        characterAnim = GetComponent<Animator>();
        characterRenderer = GetComponent<SpriteRenderer>();

        if (characterRb == null)
        {
            Debug.LogWarning("Character must have a Rigidbody component!");
        }
        if (characterAnim == null)
        {
            Debug.LogWarning("Character must have an Animator component!");
        }
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        
    }

    protected virtual void FixedUpdate()
    {

    }

    public virtual void ChangeHealth(float amount)
    {
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);

        if (amount < 0)
        {
            StartCoroutine(FlashOnHit());
        }

        if (currentHealth == 0)
        {
            OnDeath();
        }

    }

    public void Move(Vector2 direction)
    {
        Vector2 position = characterRb.position;
        position += direction * movementSpeed * Time.deltaTime;

        characterRb.position = position;

        if (direction.magnitude > 0.1f)
        {
            isMoving = true;
        } else
        {
            isMoving = false;
        }
        characterAnim.SetBool("IsMoving", isMoving);
    }

    protected IEnumerator FlashOnHit()
    {
        float timePassed = 0;
        while (timePassed < 0.4)
        {
            // Lerp from white to red and ping-pong back to sygnalize taken damage
            characterRenderer.color = Color.Lerp(Color.white, CommonValues.hitColor, Mathf.PingPong(timePassed, 0.2f) * 5);

            timePassed += Time.deltaTime;
            yield return null;
        }
    }

    public void ApplyKnockback(Vector2 direction, float force)
    {
        characterRb.AddRelativeForce(direction.normalized * force * 500);
    }

    abstract public void OnDeath();
}
