using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StoryManager : MonoBehaviour
{
    [Header("스토리 설정")]
    [SerializeField] private Story[] stories;  // 스토리 배열
    [SerializeField] private Image leftImage;       // 왼쪽 이미지 UI
    [SerializeField] private Image rightImage;      // 오른쪽 이미지 UI
    [SerializeField] private TextMeshProUGUI dialogueText;  // 대화 텍스트 UI

    private int currentStoryIndex = 0;  // 현재 스토리 인덱스
    private int currentDialogueIndex = 0;  // 현재 대화문 인덱스

    private void Start()
    {
        // 초기 스토리 표시
        ShowDialogue(currentStoryIndex, currentDialogueIndex);
    }

    private void Update()
    {
        // 스페이스바를 누르면 다음 대화문으로 넘어감
        if (Input.GetKeyDown(KeyCode.Space))
        {
            NextDialogue();
        }
    }

    private void ShowDialogue(int storyIndex, int dialogueIndex)
    {
        if (storyIndex < 0 || storyIndex >= stories.Length)
        {
            Debug.LogWarning("스토리 인덱스가 범위를 벗어났습니다.");
            return;
        }

        if (dialogueIndex < 0 || dialogueIndex >= stories[storyIndex].dialogues.Length)
        {
            Debug.LogWarning("대화문 인덱스가 범위를 벗어났습니다.");
            return;
        }

        // 현재 대화문의 이미지와 텍스트 표시
        leftImage.sprite = stories[storyIndex].dialogues[dialogueIndex].leftImage;
        rightImage.sprite = stories[storyIndex].dialogues[dialogueIndex].rightImage;
        dialogueText.text = stories[storyIndex].dialogues[dialogueIndex].text;
    }

    private void NextDialogue()
    {
        currentDialogueIndex++;
        
        // 현재 스토리의 모든 대화문을 다 보여줬으면 다음 스토리로 넘어감
        if (currentDialogueIndex >= stories[currentStoryIndex].dialogues.Length)
        {
            currentStoryIndex++;
            currentDialogueIndex = 0;
            
            // 모든 스토리를 다 보여줬으면 종료
            if (currentStoryIndex >= stories.Length)
            {
                Debug.Log("모든 스토리가 끝났습니다.");
                // 여기에 스토리 종료 후 처리할 로직 추가
                return;
            }
        }

        // 다음 대화문 표시
        ShowDialogue(currentStoryIndex, currentDialogueIndex);
    }

    // 특정 스토리로 이동하는 함수
    public void GoToStory(string storyName)
    {
        for (int i = 0; i < stories.Length; i++)
        {
            if (stories[i].storyName == storyName)
            {
                currentStoryIndex = i;
                currentDialogueIndex = 0;
                ShowDialogue(currentStoryIndex, currentDialogueIndex);
                return;
            }
        }
        Debug.LogWarning($"스토리 '{storyName}'을 찾을 수 없습니다.");
    }
} 