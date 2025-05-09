using UnityEngine;
using System.Collections;

public class StoryTrigger : MonoBehaviour
{
    [SerializeField] private string targetStoryName; // 실행할 스토리의 이름
    [SerializeField] private float holdTime = 0.5f;  // 키를 눌러야 하는 시간
    
    private bool isPlayerInRange = false;
    private float holdTimer = 0f;
    private bool wasStoryActive = false;  // 이전 프레임의 스토리 활성화 상태

    private void Update()
    {
        // 스토리 종료 감지
        if (wasStoryActive && !StoryManager.instance.IsStoryActive())
        {
            // 스토리가 방금 끝났고, 현재 트리거의 스토리였다면
            if (StoryManager.instance.GetCurrentStoryName() == targetStoryName)
            {
                OnStoryEnd();
            }
        }

        // 현재 스토리 상태 저장
        wasStoryActive = StoryManager.instance.IsStoryActive();

        if (isPlayerInRange && InputManager.instance.upHold)
        {
            holdTimer += Time.deltaTime;
            
            if (holdTimer >= holdTime)
            {
                // 스토리 실행
                StoryManager.instance.GoToStory(targetStoryName);
                holdTimer = 0f;
            }
        }
        else
        {
            holdTimer = 0f;
        }
    }

    // 스토리가 끝났을 때 실행될 함수
    private void OnStoryEnd()
    {
        // 여기에 스토리 종료 후 실행할 코드를 작성하세요
        Debug.Log($"{targetStoryName} 스토리가 끝났습니다!");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
            holdTimer = 0f;
        }
    }
} 