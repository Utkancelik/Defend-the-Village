using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField] private GameObject leftHitBox, rightHitBox, upHitBox, downHitBox;
    [SerializeField] private float damage;
    [SerializeField] private UnityEngine.UI.Slider healthSlider;
    [SerializeField] private float maxHealth, currentHealth;

    private PlayerMovement playerMovement;
    private bool clicked = false, trig = false;
    private void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        currentHealth = maxHealth;
        healthSlider.value = currentHealth;
    }

    private void Update()
    {
        if (Input.GetMouseButton(0) && !clicked)
        {
            clicked = true;

            Vector2 movementDirection = new Vector2(playerMovement.animator.GetFloat("Horizontal"), playerMovement.animator.GetFloat("Vertical"));
            Debug.Log("Movement direction " + movementDirection);

            if (movementDirection.x >= .9f)
            {
                rightHitBox.SetActive(true);
            }
            if (movementDirection.x <= -.9f)
            {
                leftHitBox.SetActive(true);
            }
            if (movementDirection.y >= .9f)
            {
                upHitBox.SetActive(true);
            }
            if (movementDirection.y <= -.9f)
            {
                downHitBox.SetActive(true);
            }

        }
        if (Input.GetMouseButtonUp(0) && clicked)
        {
            clicked = false;

            rightHitBox.SetActive(false);
            leftHitBox.SetActive(false);
            upHitBox.SetActive(false);
            downHitBox.SetActive(false);
        }
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Triggera girdi");
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Enemye vurdu.");
            Enemy enemy = (Enemy)collision.gameObject.GetComponent<Enemy>();
            enemy.rigidbody.AddForce(((Vector2)transform.position - (Vector2)enemy.transform.position).normalized * Time.fixedDeltaTime * 100f);
            enemy.TakeDamage(damage);
        }
    }
    //private void OnTriggerStay2D(Collider2D collision)
    //{
    //    Debug.Log("Triggera girdi");
    //    if (collision.CompareTag("Enemy"))
    //    {
    //        Debug.Log("Enemye vurdu.");
    //        Enemy enemy = (Enemy)collision.GetComponent<Enemy>();
    //        enemy.TakeDamage(damage);
    //    }
    //}

    public void TakeDamage(float damage)
    {
        currentHealth += damage;
        healthSlider.value = currentHealth;
        if (currentHealth >= healthSlider.maxValue)
        {
            Destroy(gameObject);
        }
    }
}
