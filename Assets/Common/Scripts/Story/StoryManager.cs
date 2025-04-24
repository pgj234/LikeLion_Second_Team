using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StoryManager : MonoBehaviour
{
    public static StoryManager instance { get; private set; }

    [Header("스토리 설정")]
    [SerializeField] private Story[] stories;  // 스토리 배열
    [SerializeField] private Image leftImage;       // 왼쪽 이미지 UI
    [SerializeField] private Image rightImage;      // 오른쪽 이미지 UI
    [SerializeField] private TextMeshProUGUI dialogueText;  // 대화 텍스트 UI
    [SerializeField] private GameObject storyPanel;  // 스토리 패널

    private int currentStoryIndex = 0;  // 현재 스토리 인덱스
    private int currentDialogueIndex = 0;  // 현재 대화문 인덱스
    private bool isStoryActive = false;  // 스토리가 활성화되어 있는지 여부

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        // 초기에는 스토리 패널 비활성화
        if (storyPanel != null)
        {
            storyPanel.SetActive(false);
        }
    }

    private void Update()
    {
        // 스토리가 활성화되어 있을 때만 스페이스바 입력 처리
        if (isStoryActive && Input.GetKeyDown(KeyCode.Space))
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
        
        // 현재 스토리의 모든 대화문을 다 보여줬으면 종료
        if (currentDialogueIndex >= stories[currentStoryIndex].dialogues.Length)
        {
            // 스토리 패널 비활성화
            if (storyPanel != null)
            {
                storyPanel.SetActive(false);
                isStoryActive = false;
            }
            return;
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
                
                // 스토리 패널 활성화
                if (storyPanel != null)
                {
                    storyPanel.SetActive(true);
                    isStoryActive = true;
                }
                
                ShowDialogue(currentStoryIndex, currentDialogueIndex);
                return;
            }
        }
        Debug.LogWarning($"스토리 '{storyName}'을 찾을 수 없습니다.");
    }
} 