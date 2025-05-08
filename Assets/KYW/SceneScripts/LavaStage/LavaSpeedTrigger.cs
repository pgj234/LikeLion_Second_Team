using UnityEngine;

public class LavaSpeedTrigger : MonoBehaviour
{
    [SerializeField] private float speed = 2f; // 속도
    [SerializeField] private Transform targetTransform; // 이동할 위치의 Transform
    [SerializeField] private bool changePosition = false; // 위치 변경 여부
    [SerializeField] private bool changeSpeed = false; // 속도 변경 여부

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // 위치 변경
            if (changePosition && targetTransform != null)
            {
                MovingLava.Instance.transform.position = targetTransform.position;
            }
            
            // 속도 변경
            if (changeSpeed)
            {
                MovingLava.Instance.SetSpeed(speed);
            }
            
            // 트리거 비활성화
            gameObject.SetActive(false);
        }
    }
} 