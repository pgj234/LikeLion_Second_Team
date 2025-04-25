using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player; // 플레이어 Transform
    public Transform monster; // 몬스터 Transform
    public float moveSpeed = 2f; // 카메라 이동 속도
    public float stayDuration = 2f; // 몬스터 위치에서 머무르는 시간

    private Vector3 originalOffset; // 플레이어와 카메라 간 초기 오프셋
    private bool isMovingToMonster = false;

    void Start()
    {
        // 초기 오프셋 저장 (카메라와 플레이어 간 거리)
        originalOffset = transform.position - player.position;
    }

    void Update()
    {
        // 기본적으로 플레이어 따라가기
        if (!isMovingToMonster)
        {
            transform.position = Vector3.Lerp(transform.position, player.position + originalOffset, moveSpeed * Time.deltaTime);
        }
    }

    public void MoveToMonster()
    {
        if (!isMovingToMonster)
        {
            isMovingToMonster = true;
            StartCoroutine(MoveCameraToMonster());
        }
    }

    private System.Collections.IEnumerator MoveCameraToMonster()
    {
        // 몬스터 위치로 이동
        Vector3 targetPos = new Vector3(monster.position.x, monster.position.y, transform.position.z);
        while (Vector3.Distance(transform.position, targetPos) > 0.1f)
        {
            transform.position = Vector3.Lerp(transform.position, targetPos, moveSpeed * Time.deltaTime);
            yield return null;
        }

        // 몬스터 위치에서 대기
        yield return new WaitForSeconds(stayDuration);

        // 플레이어 위치로 복귀
        isMovingToMonster = false;
    }
}