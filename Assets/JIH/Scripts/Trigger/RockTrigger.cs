using UnityEngine;

public class RockTrigger : MonoBehaviour
{

    public GameObject rock; // 바위 오브젝트
    public Vector2 dropPosition; // 바위가 떨어질 위치 (2D)
    public float dropHeight = 10f; // 바위가 떨어지기 시작할 높이
    public float dropSpeed = 5f; // 떨어지는 속도 (옵션)

    private bool isTriggered = false; // 중복 트리거 방지

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isTriggered)
        {
            isTriggered = true;
            DropRock();
        }
    }

    private void DropRock()
    {
        if (rock == null)
        {
            Debug.LogError("Rock is not assigned!");
            return;
        }

        // 바위 시작 위치 설정 (떨어질 위치 위로)
        Vector3 startPos = new Vector3(dropPosition.x, dropPosition.y + dropHeight, rock.transform.position.z);
        rock.transform.position = startPos;

        // 바위 활성화 (비활성화 상태였다면)
        rock.SetActive(true);

        // Rigidbody2D로 자연스럽게 떨어지도록
        Rigidbody2D rb = rock.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.isKinematic = false; // 중력 적용
        }
    }
}
