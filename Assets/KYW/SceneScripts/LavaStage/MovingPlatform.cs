using UnityEngine;
using System.Collections.Generic;

public class MovingPlatform : MonoBehaviour
{
    [Header("이동 설정")]
    [SerializeField] private List<Transform> waypoints = new List<Transform>();
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float waitTime = 1f;

    private int currentWaypointIndex = 0;
    private bool isMoving = false;
    private float waitTimer = 0f;
    private bool isWaiting = false;
    private Transform playerTransform;
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody2D>();
        }
        rb.gravityScale = 0f;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        rb.bodyType = RigidbodyType2D.Kinematic;
    }

    private void FixedUpdate()
    {
        if (!isMoving) return;

        if (isWaiting)
        {
            waitTimer += Time.fixedDeltaTime;
            if (waitTimer >= waitTime)
            {
                isWaiting = false;
                waitTimer = 0f;
                currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Count;
            }
            return;
        }

        if (waypoints.Count == 0) return;

        Vector3 targetPosition = waypoints[currentWaypointIndex].position;
        Vector3 direction = (targetPosition - transform.position).normalized;
        float distance = Vector3.Distance(transform.position, targetPosition);

        if (distance > 0.1f)
        {
            transform.Translate(direction * moveSpeed * Time.fixedDeltaTime);
        }
        else
        {
            isWaiting = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isMoving = true;
            playerTransform = collision.transform;
            playerTransform.SetParent(transform);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (playerTransform != null)
            {
                playerTransform.SetParent(null);
                playerTransform = null;
            }
        }
    }

    private void OnDrawGizmos()
    {
        if (waypoints.Count == 0) return;

        Gizmos.color = Color.cyan;
        
        for (int i = 0; i < waypoints.Count; i++)
        {
            if (waypoints[i] == null) continue;
            
            Vector3 current = waypoints[i].position;
            Vector3 next = waypoints[(i + 1) % waypoints.Count].position;
            
            Gizmos.DrawLine(current, next);
            Gizmos.DrawWireSphere(current, 0.2f);
        }
    }
} 