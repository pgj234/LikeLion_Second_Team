using UnityEngine;
using System.Collections.Generic;

public class GameTimeManager : MonoBehaviour
{
    public static GameTimeManager instance;

    private float normalTimeScale = 1f;
    private float slowMotionScale = 0.3f;
    private float slowDuration = 2f;
    private bool isSlowed;
    private float slowEndTime;
    private List<Rigidbody2D> projectiles = new List<Rigidbody2D>();

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddProjectile(Rigidbody2D projectileRb)
    {
        if (projectileRb != null && !projectiles.Contains(projectileRb))
        {
            projectiles.Add(projectileRb);
            Debug.Log($"Projectile added, Total: {projectiles.Count}, RB: {projectileRb}");
        }
    }

    public void RemoveProjectile(Rigidbody2D projectileRb)
    {
        if (projectileRb != null)
        {
            projectiles.Remove(projectileRb);
            Debug.Log($"Projectile removed, Total: {projectiles.Count}");
        }
    }

    public void ApplySlowMotion()
    {
        if (!isSlowed)
        {
            isSlowed = true;
            slowEndTime = Time.time + slowDuration;
            Time.timeScale = slowMotionScale;
            Debug.Log($"Slow motion applied, TimeScale: {Time.timeScale}, Projectiles: {projectiles.Count}");

            foreach (var projectile in projectiles)
            {
                if (projectile != null)
                {
                    Vector2 oldVelocity = projectile.linearVelocity;
                    projectile.linearVelocity *= slowMotionScale;
                    Debug.Log($"Projectile {projectile.gameObject.name} velocity reduced from {oldVelocity} to {projectile.linearVelocity}");
                }
            }
        }
    }

    private void Update()
    {
        if (isSlowed && Time.time >= slowEndTime)
        {
            isSlowed = false;
            Time.timeScale = normalTimeScale;
            Debug.Log($"Slow motion ended, TimeScale: {Time.timeScale}, Projectiles: {projectiles.Count}");

            foreach (var projectile in projectiles)
            {
                if (projectile != null)
                {
                    Vector2 oldVelocity = projectile.linearVelocity;
                    projectile.linearVelocity /= slowMotionScale;
                    Debug.Log($"Projectile {projectile.gameObject.name} velocity restored from {oldVelocity} to {projectile.linearVelocity}");
                }
            }
        }
    }
}