using UnityEngine;

public class CheckingGhost : MonoBehaviour
{
    [SerializeField] private GameObject[] ghostObjects; // 체크할 유령 오브젝트들
    [SerializeField] private GameObject doorObject; // 활성화할 문 오브젝트
    private Door door; // Door 컴포넌트 참조

    private void Start()
    {
        // Door 컴포넌트 가져오기
        if (doorObject != null)
        {
            door = doorObject.GetComponent<Door>();
            // 초기에는 문 비활성화
            doorObject.SetActive(false);
        }
    }

    private void Update()
    {
        // 모든 유령 오브젝트가 파괴되었는지 체크
        bool allDestroyed = true;
        foreach (GameObject ghost in ghostObjects)
        {
            if (ghost != null)
            {
                allDestroyed = false;
                break;
            }
        }

        // 모든 유령이 파괴되었다면 문 활성화
        if (allDestroyed && doorObject != null)
        {
            doorObject.SetActive(true);
            // 이 스크립트는 더 이상 필요 없으므로 비활성화
            this.enabled = false;
        }
    }
} 