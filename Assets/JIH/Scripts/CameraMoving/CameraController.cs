using UnityEngine;
using Unity.Cinemachine;

public class CameraController : MonoBehaviour
{
    public string playerTag = "Player";
    public GameObject fallRock; // FallRock 오브젝트
    public CinemachineCamera virtualCamera; // 시네머신 가상 카메라
    private GameObject player; // 플레이어 오브젝트
    private Transform originalFollowTarget; // 원래 Follow 타겟 (플레이어)

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag(playerTag);
        originalFollowTarget = player.transform; // 초기 Follow 타겟 저장
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(playerTag))
        {
            // 카메라를 FallRock 위치로 이동
            virtualCamera.Follow = fallRock.transform;

            // 2초 후 원래 타겟(플레이어)으로 복귀
            Invoke(nameof(ResetCameraFollow), 2f);
        }
    }

    private void ResetCameraFollow()
    {
        virtualCamera.Follow = originalFollowTarget; // 플레이어로 Follow 복귀
    }
}