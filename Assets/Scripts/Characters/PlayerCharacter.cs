using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : Character 
{
    Vector2 currentInput;

    [SerializeField] float invinciblityTime;
    float invinciblityTimer;
    bool isInvincible;

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();

        HandleInput();
        InvinciblityUpdate();
    }

    protected void FixedUpdate()
    {
        Move(currentInput);
    }

    public override void OnDeath()
    {
        MainController.Instance.GameOver();
    }

    public override void ChangeHealth(float amount)
    {
        if (isInvincible)
        {
            return;
        }

        isInvincible = true;
        invinciblityTimer = invinciblityTime;

        base.ChangeHealth(amount);
    }

    void HandleInput()
    {
        // MOVEMENT
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        currentInput = new Vector2(horizontal, vertical);
       
        lookDirection = Camera.main.ScreenToWorldPoint(Input.mousePosition).x - transform.position.x;
        characterAnim.SetFloat("LookDirection", lookDirection);
    }
    void InvinciblityUpdate()
    {
        if (isInvincible)
        {
            invinciblityTimer -= Time.deltaTime;
            if (invinciblityTimer < 0)
            {
                isInvincible = false;
            }
        }
    }

}
