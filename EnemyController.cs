﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
   private enum State
    {
        Walking,
        Knockback,
        Dead
    }

    private State currentState;

    [SerializeField]
    private float
        groundCheckDistance,
        wallCheckDistance,
        movementSpeed,
        maxHealth,
        knockbackDuration;

    [SerializeField]
    private Transform
        groundCheck,
        wallCheck;

    [SerializeField]
    private Vector2 knockbackSpeed;

    [SerializeField]
    private LayerMask whatIsGround;

    private float
        currentHealth,
        knockbackStartTime;

    private int facingDirection,
        damageDirection;

    private Vector2 movement;

    private bool
        groundDetected,
        wallDetected;

    private GameObject alive;
    private Rigidbody2D aliveRb;

    private void Start()
    {
        alive = transform.Find("Alive").gameObject;
        aliveRb = alive.GetComponent<Rigidbody2D>();
        facingDirection = 1;
    }

    private void Update()
    {
        switch (currentState)
        {
            case State.Walking:
                UpdateWalkingState();
                break;
            case State.Knockback:
                UpdateKnockbackState();
                break;
            case State.Dead:
                UpdateDeadState();
                break;
        }
    }

    //Walking---------------------------------

    private void EnterWalkingState()
    {

    }

    private void UpdateWalkingState()
    {
        groundDetected = Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, whatIsGround);
        wallDetected = Physics2D.Raycast(wallCheck.position, transform.right, wallCheckDistance, whatIsGround);

        if(!groundDetected || wallDetected)
        {
            Flip();
        }
        else
        {
            movement.Set(movementSpeed * facingDirection, aliveRb.velocity.y);
            aliveRb.velocity = movement;
        }
    }

    private void ExitWalkingState()
    {

    }

    //Knockback-------------------------------

    private void EnterKnockbackState()
    {

    }

    private void UpdateKnockbackState()
    {

    }

    private void ExitKnockbackState()
    {

    }

    //Dead------------------------------------

    private void EnterDeadState()
    {

    }

    private void UpdateDeadState()
    {

    }

    private void ExitDeadState()
    {

    }

    //Other shit-------------------------------

    private void Damage(float[] attackDetails)
    {
        currentHealth -= attackDetails[0];
        if(attackDetails[1] > alive.transform.position.x)
        {
            damageDirection = -1;
        }
        else
        {
            damageDirection = 1;
        }

        //blood particles

        if(currentHealth > 0.0f)
        {
            SwitchState(State.Knockback);
        }
        else if(currentHealth <= 0.0f)
        {
            SwitchState(State.Dead);
        }
    }

    private void Flip()
    {
        facingDirection *= -1;
        alive.transform.Rotate(0.0f, 180.0f, 0.0f);
    }

    private void SwitchState(State state)
    {
        switch (currentState)
        {
            case State.Walking:
                ExitWalkingState();
                break;
            case State.Knockback:
                ExitKnockbackState();
                break;
            case State.Dead:
                ExitDeadState();
                break;
        }

        switch (state)
        {
            case State.Walking:
                EnterWalkingState();
                break;
            case State.Knockback:
                EnterKnockbackState();
                break;
            case State.Dead:
                EnterDeadState();
                break;
        }

        currentState = state;
    }
}


