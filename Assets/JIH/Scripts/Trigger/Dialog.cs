using UnityEngine;
using TMPro; // TextMeshPro 네임스페이스
using System.Collections;

public class Dialog : MonoBehaviour
{
 
   public GameObject rock; // 바위 오브젝트
    public Transform dropPoint; // 바위가 떨어질 위치
    public float dropHeight = 10f; // 바위가 떨어지기 시작할 높이
    public GameObject dialogueBubble; // 말풍선 UI
    public TMP_Text dialogueText; // 말풍선 텍스트 (TextMeshPro)
    public Camera mainCamera; // 메인 카메라
    public Transform player; // 플레이어 Transform (카메라 복귀용)
    public float cameraMoveTime = 2f; // 카메라 이동 시간 (2초)
    public float shakeDuration = 1f; // 카메라 흔들림 지속 시간 (1초)
    public float shakeMagnitude = 0.1f; // 카메라 흔들림 크기

    private bool isTriggered = false; // 중복 트리거 방지
    private bool isDialogueActive = false; // 말풍선 활성화 상태
    private Vector3 originalCameraPos; // 카메라 원래 위치

    void Start()
    {
        dialogueBubble.SetActive(false); // 초기 말풍선 비활성화
        rock.SetActive(false); // 초기 바위 비활성화
        originalCameraPos = mainCamera.transform.position;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isTriggered)
        {
            ShowDialogue();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            HideDialogue();
        }
    }

    void Update()
    {
        if (isDialogueActive && Input.GetKeyDown(KeyCode.Return))
        {
            isTriggered = true;
            StartCoroutine(SequenceEvents());
        }
    }

    void ShowDialogue()
    {
        dialogueBubble.SetActive(true);
        dialogueText.text = "엔터를 누르면 바위가 떨어진다!";
        isDialogueActive = true;
    }

    void HideDialogue()
    {
        dialogueBubble.SetActive(false);
        isDialogueActive = false;
    }

    IEnumerator SequenceEvents()
    {
        // 말풍선 비활성화
        HideDialogue();

        // 1초 대기
        yield return new WaitForSeconds(1f);

        // 카메라를 바위 위치로 2초간 이동
        Vector3 targetPos = new Vector3(dropPoint.position.x, dropPoint.position.y, mainCamera.transform.position.z);
        float t = 0f;
        Vector3 startPos = mainCamera.transform.position;
        while (t < 1f)
        {
            t += Time.deltaTime / cameraMoveTime;
            mainCamera.transform.position = Vector3.Lerp(startPos, targetPos, t);
            yield return null;
        }

        // 카메라 흔들림 (1초)
        yield return StartCoroutine(ShakeCamera());

        // 1초 대기
        yield return new WaitForSeconds(1f);

        // 바위 떨어짐
        DropRock();

        // 카메라를 플레이어 위치로 2초간 복귀
        Vector3 playerCamPos = new Vector3(player.position.x, player.position.y, mainCamera.transform.position.z);
        t = 0f;
        startPos = mainCamera.transform.position;
        while (t < 1f)
        {
            t += Time.deltaTime / cameraMoveTime;
            mainCamera.transform.position = Vector3.Lerp(startPos, playerCamPos, t);
            yield return null;
        }
    }

    void DropRock()
    {
        rock.SetActive(true);
        rock.transform.position = new Vector2(dropPoint.position.x, dropPoint.position.y + dropHeight);

        Rigidbody2D rb = rock.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.bodyType = RigidbodyType2D.Dynamic; // 중력 적용
            rb.gravityScale = 1f;
            rb.linearVelocity = Vector2.zero; // 초기 속도 제거
        }
    }

    IEnumerator ShakeCamera()
    {
        float elapsed = 0f;
        Vector3 originalPos = mainCamera.transform.position;
        while (elapsed < shakeDuration)
        {
            float x = Random.Range(-1f, 1f) * shakeMagnitude;
            float y = Random.Range(-1f, 1f) * shakeMagnitude;
            mainCamera.transform.position = originalPos + new Vector3(x, y, 0);
            elapsed += Time.deltaTime;
            yield return null;
        }
        mainCamera.transform.position = originalPos;
    }
}
