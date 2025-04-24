using UnityEngine;
using System.Collections;

public class StoryTrigger : MonoBehaviour
{
    [SerializeField] private string targetStoryName; // 실행할 스토리의 이름
    [SerializeField] private float holdTime = 0.5f;  // 키를 눌러야 하는 시간
    
    private bool isPlayerInRange = false;
    private float holdTimer = 0f;

    private void Update()
    {
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