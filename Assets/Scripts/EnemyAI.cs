using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private float movementSpeed;
    [SerializeField] private float checkRadius;
    [SerializeField] private float attackRadius;

    [SerializeField] private bool shouldRotate;

    [SerializeField] private LayerMask whatIsPlayer;

    [SerializeField] private Vector3 movementDirection;

    private Transform target;
    private Rigidbody2D rigidbody;
    private Animator animator;
    private Vector2 movement;

    private bool isInChaseRange;
    private bool isInAttackRange;

    private void Start()
    {
        animator = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        if (isInChaseRange && !isInAttackRange)
        {

        }
        else if (isInAttackRange)
        {
            animator.SetTrigger("Attack");

        }
        else
        {
            movement = Vector2.zero;
        }

        animator.SetFloat("Speed", movement.sqrMagnitude);

        movementDirection = target.position - transform.position;
        float angle = Mathf.Atan2(movementDirection.y, movementDirection.x) * Mathf.Rad2Deg;
        movementDirection.Normalize();
        movement = movementDirection;

        isInChaseRange = Physics2D.OverlapCircle(transform.position, checkRadius, whatIsPlayer);
        isInAttackRange = Physics2D.OverlapCircle(transform.position, attackRadius, whatIsPlayer);



        if (shouldRotate)
        {
            animator.SetFloat("Horizontal", movement.x);
            animator.SetFloat("Vertical", movement.y);
        }
    }

    private void FixedUpdate()
    {
        if (!isInAttackRange && isInChaseRange)
        {
            MoveCharacter(movement);
        }

        if (isInAttackRange)
        {
            rigidbody.velocity = Vector2.zero;
        }
    }

    private void MoveCharacter(Vector2 moveDirection)
    {
        rigidbody.MovePosition((Vector2)transform.position + movementSpeed * Time.fixedDeltaTime * moveDirection);
    }
}
