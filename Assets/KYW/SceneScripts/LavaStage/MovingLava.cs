using UnityEngine;

public class MovingLava : MonoBehaviour
{
    public static MovingLava Instance { get; private set; }

    [SerializeField] private float speed = 2f;

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
        transform.Translate(Vector3.right * speed * Time.deltaTime);
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
} 