using UnityEngine;

public class WaterUpCollision : MonoBehaviour
{

    public WaterController waterController; // 물 오브젝트의 컨트롤러 참조

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 플레이어와 충돌 확인
        if (collision.gameObject.CompareTag("Player"))
        {
            waterController.StartRising(); // 물 상승 시작
            gameObject.SetActive(false); // 충돌체 비활성화
        }
    }
}
