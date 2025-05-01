using UnityEngine;
using System.Collections;

public class SlimeController : MonoBehaviour
{
    public GameObject projectilePrefab;
    public float fireInterval = 2f;
    public float projectileSpeed = 5f;
    public Transform firePoint;
    public bool shootRight = true;
    private float fireTimer = 0f;

    void Start()
    {
        if (projectilePrefab == null || firePoint == null)
        {
            Debug.LogError("ProjectilePrefab or FirePoint is not assigned!");
        }
    }

    void Update()
    {
        fireTimer += Time.deltaTime;
        if (fireTimer >= fireInterval)
        {
            FireProjectile();
            fireTimer = 0f;
        }
    }

    void FireProjectile()
    {
        if (projectilePrefab != null && firePoint != null)
        {
            GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
            Vector2 direction = shootRight ? Vector2.right : Vector2.left;
            Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.linearVelocity = direction * projectileSpeed;
                Debug.Log($"Projectile {projectile.name} initial velocity: {rb.linearVelocity}");
            }
            else
            {
                Debug.LogError("Projectile missing Rigidbody2D!");
            }
        }
    }
}