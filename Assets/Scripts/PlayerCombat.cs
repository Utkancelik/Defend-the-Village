using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField] private GameObject leftHitBox, rightHitBox, upHitBox, downHitBox, crosshair, bulletPrefab;
    [SerializeField] private float damage;
    [SerializeField] private UnityEngine.UI.Slider healthSlider;
    [SerializeField] private float maxHealth, currentHealth;
    [SerializeField] private float crosshairDistance;
    [SerializeField] private float bulletSpeed, fireRate = 5.0f;

    private PlayerMovement playerMovement;
    private bool clicked = false, trig = false, endOfAiming,shotFired;
    private Vector2 mousePos;

    float timer = 0f;
    private void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        currentHealth = maxHealth;
        healthSlider.value = currentHealth;
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer > fireRate)
        {
            fireRate = timer + 0.2f;
            shotFired = false;
        }
        Aim();
        Shoot();
        endOfAiming = Input.GetMouseButtonUp(0) && !shotFired;
        

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

    public void TakeDamage(float damage)
    {
        currentHealth += damage;
        healthSlider.value = currentHealth;
        if (currentHealth >= healthSlider.maxValue)
        {
            Destroy(gameObject);
        }
    }

    private void Aim()
    {
        if (playerMovement.movement != Vector2.zero)
        {
            crosshair.transform.localPosition = playerMovement.movement * crosshairDistance;
        }
    }

    private void Shoot()
    {
        Vector2 shootingDirection = crosshair.transform.localPosition;
        shootingDirection.Normalize();

        if (endOfAiming)
        {
            shotFired = true;
            GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            bullet.GetComponent<Rigidbody2D>().velocity = shootingDirection * bulletSpeed;
            bullet.transform.Rotate(0,0,Mathf.Atan2(shootingDirection.y, shootingDirection.x) * Mathf.Rad2Deg);
            Destroy(bullet, 1f);
        }

        
        
    }
}
