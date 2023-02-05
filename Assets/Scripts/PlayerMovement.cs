using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;

    private Rigidbody2D rigidbody;
    public Vector2 movement;
    public Animator animator;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (FindObjectOfType<GameManager>().isGameRunning && !FindObjectOfType<GameManager>().isGameOver &&
            !FindObjectOfType<GameManager>().isGamePaused)
        {
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");
            movement = movement.normalized;


            if (movement.x != 0 || movement.y != 0)
            {
                animator.SetFloat("Horizontal", movement.x);
                animator.SetFloat("Vertical", movement.y);
            }

            animator.SetFloat("Speed", movement.magnitude);

            if (Input.GetMouseButton(0))
            {
                animator.SetTrigger("Attack");
            }
        }
    }

    private void FixedUpdate()
    {
        if (FindObjectOfType<GameManager>().isGameRunning && !FindObjectOfType<GameManager>().isGameOver &&
           !FindObjectOfType<GameManager>().isGamePaused)
        {
            rigidbody.MovePosition(rigidbody.position + speed * Time.fixedDeltaTime * movement);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("SpeedBoost"))
        {
            speed = 1.4f;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("SpeedBoost"))
        {
            speed = 1f;
        }
    }


}
