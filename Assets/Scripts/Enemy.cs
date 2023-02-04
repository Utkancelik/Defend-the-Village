using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Enemy : MonoBehaviour
{
    [SerializeField] private Slider healthSlider;
    [SerializeField] private float maxHealth, currentHealth;
    private void Start()
    {
        currentHealth = maxHealth;
        healthSlider.value = currentHealth;
    }

    private void Update()
    {
        
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

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerCombat playerCombat = collision.gameObject.GetComponent<PlayerCombat>();
            if (playerCombat != null)
            {
                playerCombat.TakeDamage(10);
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
