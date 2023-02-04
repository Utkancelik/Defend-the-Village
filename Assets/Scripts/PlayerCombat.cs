using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField] private GameObject leftHitBox, rightHitBox, upHitBox, downHitBox;
    [SerializeField] private float damage;
    [SerializeField] private Slider healthSlider;
    [SerializeField] private float maxHealth, currentHealth;

    private PlayerMovement playerMovement;

    private void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        currentHealth = maxHealth;
        healthSlider.value = currentHealth;
    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Vector2 movementDirection = new Vector2(playerMovement.animator.GetFloat("Horizontal"), playerMovement.animator.GetFloat("Vertical"));


            if (movementDirection.x >= 1)
            {
                rightHitBox.SetActive(true);
            }
            else if (movementDirection.x <= -1)
            {
                leftHitBox.SetActive(true);
            }
            else if (movementDirection.y >= 1)
            {
                upHitBox.SetActive(true);
            }
            else if (movementDirection.y <= -1)
            {
                downHitBox.SetActive(true);
            }

        }
        if (Input.GetMouseButtonUp(0))
        {
            rightHitBox.SetActive(false);
            leftHitBox.SetActive(false);
            upHitBox.SetActive(false);
            downHitBox.SetActive(false);
        }
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Enemy enemy = (Enemy)collision.GetComponent<Enemy>();
            enemy.TakeDamage(damage);
        }
    }

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
