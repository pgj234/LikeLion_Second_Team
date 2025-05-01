using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private GameObject targetDoor; // 이동할 문
    [SerializeField] private float delay = 0.5f; // 텔레포트 딜레이
    [SerializeField] private float fadeDuration = 0.5f; // 페이드 인/아웃 지속 시간
    [SerializeField] private bool isCrazy; // 미친 문 여부
    private bool isPlayerInRange = false;
    private Player player; // 플레이어 인스턴스 저장
    private float timer = 0f;
    private Animator animator; // 애니메이터 컴포넌트

    private void Start()
    {
        // 애니메이터 컴포넌트 가져오기
        animator = GetComponent<Animator>();
        // 미친 문 여부 설정
        animator.SetBool("DoorCrazy", isCrazy);
    }

    private void Update()
    {
        if (isPlayerInRange && InputManager.instance.upHold)
        {
            timer += Time.deltaTime;
            if (timer >= delay)
            {
                // 문 열기 애니메이션 재생
                animator.SetTrigger("DoorOpen");
                TeleportPlayer();
                timer = 0f;
            }
        }
        else
        {
            timer = 0f;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerInRange = true;
            player = collision.GetComponent<Player>();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerInRange = false;
            player = null;
            timer = 0f;
        }
    }

    private void TeleportPlayer()
    {
        if (targetDoor != null && player != null)
        {
            player.transform.position = targetDoor.transform.position;
        }
    }
} 