using UnityEngine;

public class MovingLava : MonoBehaviour
{
    public static MovingLava Instance { get; private set; }

    [SerializeField] private float speed = 2f;
    private Vector3 moveDirection = Vector3.right;  // 이동 방향

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        // position을 직접 변경하면 자식 오브젝트도 자동으로 따라감
        transform.position += moveDirection * speed * Time.deltaTime;
    }

    // 외부에서 속도 변경
    public void SetSpeed(float newSpeed)
    {
        speed = newSpeed;
    }

    // 현재 속도 반환
    public float GetSpeed()
    {
        return speed;
    }

    // 이동 방향 변경
    public void SetMoveDirection(Vector3 direction)
    {
        moveDirection = direction.normalized;
    }
} 