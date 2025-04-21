using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerLight : LightInBlackArea
{
    [Space, Header("플레이어")]
    [SerializeField] private Player player;

    [Space, Header("빛 관련 설정")]
    [SerializeField] private float startTime;
    [SerializeField] private float remainTime; // 남은 시간(남은 시간은 상위 콜라이더에서 줄일 것)
    [SerializeField] private float smoothTime = 0.1f; // 작을수록 즉각적인 크기 반영이 됨
    private Vector3 refScaleVelocity;

    [Space, Header("조건")]
    public bool isInBlackArea = false; // 빛이 줄어드는 공간에 있는지 여부
    public bool isInLight = false; // 빛을 충전할 수 있는 곳에 있는지 여부

    void Start()
    {
        if (player == null)
        {
            Debug.LogError("PlayerLight에 플레이어가 등록되지 않았습니다!");
        }
    }

    void Update()
    {
        UpdateScale(); // 빛의 범위를 항상 업데이트 하기

        if (player != null) // 플레이어가 지역에 들어왔다면
        {
            if (isInBlackArea && !isInLight) // 어둠 속에 있으면서 빛 충전을 받지 못하면
            {
                remainTime -= Time.deltaTime; // 남은 시간 감소
            }
            else
            {
                remainTime = startTime;
            }

            if (remainTime <= 0) // 남은 시간이 없어졌다면
            {
                // player 죽는 함수 추가하거나 죽은 상태로 변경하기
            }
        }
    }

    void UpdateScale()
    {
        float percent = Mathf.Clamp01(remainTime / startTime); // 남은 시간의 비율을 구한 후

        light2D.pointLightOuterRadius = OuterRadius * percent;
        light2D.pointLightInnerRadius = Mathf.Clamp(InnerRadius, 0, OuterRadius) * percent;
    }
}
