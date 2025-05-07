using UnityEngine;
using Unity.Cinemachine;

public class CameraController : MonoBehaviour
{
    public string playerTag = "Player";
    public GameObject fallRock; // FallRock ������Ʈ
    public CinemachineCamera virtualCamera; // �ó׸ӽ� ���� ī�޶�
    private GameObject player; // �÷��̾� ������Ʈ
    private Transform originalFollowTarget; // ���� Follow Ÿ�� (�÷��̾�)

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag(playerTag);
        originalFollowTarget = player.transform; // �ʱ� Follow Ÿ�� ����
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(playerTag))
        {
            // ī�޶� FallRock ��ġ�� �̵�
            virtualCamera.Follow = fallRock.transform;

            // 2�� �� ���� Ÿ��(�÷��̾�)���� ����
            Invoke(nameof(ResetCameraFollow), 3f);
        }
    }

    private void ResetCameraFollow()
    {
        virtualCamera.Follow = originalFollowTarget; // �÷��̾�� Follow ����
    }
}