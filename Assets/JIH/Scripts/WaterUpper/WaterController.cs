using UnityEngine;

public class WaterController : MonoBehaviour
{

    public float riseSpeed = 1f; // 물 상승 속도
    public float maxHeight = 5f; // 물의 최대 높이 (Y 위치)
    private bool isRising = false;
    private Vector3 initialPosition;

    void Start()
    {
        initialPosition = transform.position; // 초기 위치 저장
    }

    void Update()
    {
        if (isRising && transform.position.y < initialPosition.y + maxHeight)
        {
            // 물을 점진적으로 상승
            transform.Translate(Vector2.up * riseSpeed * Time.deltaTime);
        }
    }

    public void StartRising()
    {
        isRising = true; // 물 상승 시작
    }
}
