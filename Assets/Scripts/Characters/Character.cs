using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MEC;

public abstract class Character : MonoBehaviour
{
    [SerializeField] float maxHealth;
    public float currentHealth { get; private set; }
    [SerializeField] protected float movementSpeed;
    bool isMoving = false;
    public float lookDirection { get; protected set; }

    public Rigidbody2D characterRb { get; protected set; }
    protected Animator characterAnim;
    protected SpriteRenderer characterRenderer;

    public Vector2 lastTakenHitDirection;

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

        currentHealth = maxHealth;
    }

    private void OnEnable()
    {
        if (characterAnim != null)
            characterAnim.enabled = true;

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
            Timing.RunCoroutine(_FlashOnHit());
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

    protected IEnumerator<float> _FlashOnHit()
    {
        float timePassed = 0;
        while (timePassed < 0.2)
        {
            // Lerp from white to red and ping-pong back to sygnalize taken damage
            characterRenderer.color = Color.Lerp(Color.white, CommonValues.hitColor, Mathf.PingPong(timePassed, 0.1f) * 10);

            timePassed += Time.deltaTime;
            yield return 0f;
        }
    }

    public void ApplyKnockback(Vector2 direction, float force)
    {
        characterRb.AddRelativeForce(direction.normalized * force * 500);
    }

    abstract public void OnDeath();

    protected IEnumerator<float> _PlayDeathAnim(Vector2 lastHit)
    {
        // Stop animating
        characterAnim.enabled = false;
        
        // Gives the character a knockback and fades it away
        characterRb.AddForce(lastHit.normalized * 1500);

        float timePassed = 0f;
        while (timePassed < 1f)
        {
            characterRenderer.color = Color.Lerp(Color.white, Color.clear, Mathf.PingPong(timePassed, 1f));

            timePassed += Time.deltaTime;
            yield return 0f;
        }

        gameObject.SetActive(false);
    }
}
