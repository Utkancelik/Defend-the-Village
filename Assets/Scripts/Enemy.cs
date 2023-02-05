using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Enemy : MonoBehaviour
{
    [SerializeField] private Slider healthSlider;
    [SerializeField] private float maxHealth, currentHealth, damage;

    public Rigidbody2D rigidbody;
    private void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
        healthSlider.value = currentHealth;
    }
    public void TakeDamage(float damage)
    {
        StartCoroutine(ChangeColorOnTakeDamage());
        currentHealth += damage;
        healthSlider.value = currentHealth;
        if (currentHealth >= 100)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && FindObjectOfType<GameManager>().isGameRunning)
        {
            PlayerCombat playerCombat = collision.gameObject.GetComponent<PlayerCombat>();
            if (playerCombat != null)
            {
                playerCombat.TakeDamage(damage);
            }
        }
    }

    private IEnumerator ChangeColorOnTakeDamage()
    {
        GetComponent<SpriteRenderer>().color = Color.red;
        yield return new WaitForSeconds(.1f);
        GetComponent<SpriteRenderer>().color = Color.white;
        yield return new WaitForSeconds(.1f);
    }
}
