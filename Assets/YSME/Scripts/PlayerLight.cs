using System.Collections.Generic;
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
    private float refOuterVelocity;
    private float refInnerVelocity;

    [SerializeField] private List<BlackArea> blackAreas = new List<BlackArea>();
    [SerializeField] private List<ObjectLight> objectLights = new List<ObjectLight>();

    [Space, Header("조건")]
    public bool isInLight = false; // 빛을 충전할 수 있는 곳에 있는지 여부

    void Start()
    {
        if (player == null)
        {
            Debug.LogError("PlayerLight에 플레이어가 등록되지 않았습니다!");
        }

        EventManager.instance.OnPlayerRespawned += Init;
    }

    void Update()
    {
        UpdateScale(); // 빛의 범위를 항상 업데이트 하기

        if (player != null) // 플레이어가 지역에 들어왔다면
        {
            if (blackAreas.Count > 0 && objectLights.Count <= 0) // 어둠 속에 있으면서 빛 충전을 받지 못하면
            {
                remainTime -= Time.deltaTime; // 남은 시간 감소
            }
            else
            {
                remainTime = startTime;
            }

            if (remainTime <= 0) // 남은 시간이 없어졌다면
            {
                player.stateMachine.ChangeState(player.playerDieState);
            }
        }
    }

    void Oestroy()
    {
        EventManager.instance.OnPlayerRespawned -= Init;
    }

    void Init()
    {
        remainTime = startTime;
    }

    void UpdateScale()
    {
        float percent = 0;
        if (blackAreas.Count > 0)
        {
            percent = Mathf.Clamp01(remainTime / startTime); // 남은 시간의 비율을 구한 후
        }
        else
        {
            percent = 0;
        }

        light2D.pointLightOuterRadius = Mathf.SmoothDamp(light2D.pointLightOuterRadius, OuterRadius * percent, ref refOuterVelocity, smoothTime);
        light2D.pointLightInnerRadius = Mathf.SmoothDamp(light2D.pointLightInnerRadius, InnerRadius * percent, ref refInnerVelocity, smoothTime);
    }

    public void AddArea(BlackArea area)
    {
        if (!blackAreas.Contains(area))
        {
            blackAreas.Add(area);
        }
    }

    public void RemoveArea(BlackArea area)
    {
        if (blackAreas.Contains(area))
        {
            blackAreas.Remove(area);
        }
    }

    public void AddLights(ObjectLight light)
    {
        if (!objectLights.Contains(light))
        {
            objectLights.Add(light);
        }
    }

    public void RemoveLights(ObjectLight light)
    {
        if (objectLights.Contains(light))
        {
            objectLights.Remove(light);
        }
    }
}
