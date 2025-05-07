using UnityEngine;
using DG.Tweening;
using System.Collections.Generic;

public class MovingPlatform : MonoBehaviour
{
    [Header("이동 설정")]
    [SerializeField] private List<Transform> waypoints = new List<Transform>();
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float waitTime = 1f;
    [SerializeField] private bool moveOnlyWithPlayer = true; // true: 플레이어가 있을 때만 이동, false: 자동 이동

    private int currentWaypointIndex = 0;
    private bool isMoving = false;
    private Vector3 originalPosition;
    private bool isPlayerOnRock = false;
    private float waitTimer = 0f;
    private bool isWaiting = false;
    private Rigidbody2D rb;
    private Rigidbody2D playerRb;
    private bool isWallSliding = false;

    private void Start()
    {
        originalPosition = transform.position;
        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody2D>();
            rb.gravityScale = 0f;
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        }
        
        if (!moveOnlyWithPlayer && waypoints.Count > 0)
        {
            isMoving = true;
        }
    }

    private void FixedUpdate()
    {
        if (moveOnlyWithPlayer && !isPlayerOnRock)
        {
            rb.linearVelocity = Vector2.zero;
            return;
        }

        if (isWaiting)
        {
            rb.linearVelocity = Vector2.zero;
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
        Vector2 direction = (targetPosition - transform.position).normalized;
        float distance = Vector2.Distance(transform.position, targetPosition);

        if (distance > 0.1f)
        {
            rb.linearVelocity = direction * moveSpeed;
            if (isWallSliding && playerRb != null)
            {
                playerRb.linearVelocity = rb.linearVelocity;
            }
        }
        else
        {
            rb.linearVelocity = Vector2.zero;
            if (isWallSliding && playerRb != null)
            {
                playerRb.linearVelocity = Vector2.zero;
            }
            isWaiting = true;
        }
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            if (col.gameObject.TryGetComponent(out Player player))
            {
                if (player.stateMachine.currentState == player.idleState || 
                    player.stateMachine.currentState == player.moveState || 
                    player.stateMachine.currentState == player.wallslideState)
                {
                    isPlayerOnRock = true;
                    
                    if (player.stateMachine.currentState == player.wallslideState)
                    {
                        isWallSliding = true;
                        if (playerRb == null)
                        {
                            playerRb = col.GetComponent<Rigidbody2D>();
                        }
                    }
                    else
                    {
                        isWallSliding = false;
                    }
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            isPlayerOnRock = false;
            isWallSliding = false;
            playerRb = null;
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