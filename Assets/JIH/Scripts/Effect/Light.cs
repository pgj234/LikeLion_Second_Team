using System.Collections;
using UnityEngine;

public class Light : LightCollisionState
{
    private ParticleSystem particleSystem; // 자식 오브젝트의 파티클 시스템
    private Light lightComponent; // 라이트 컴포넌트
    private bool isLightOn = false;
    public float delayTime = 0.5f; // 빛 켜짐 딜레이 시간

    public override void Start()
    {
        base.Start();
        // 자식 오브젝트에서 ParticleSystem 찾기
        particleSystem = GetComponentInChildren<ParticleSystem>();
        if (particleSystem == null)
        {
            Debug.LogWarning("자식 오브젝트에 ParticleSystem이 없습니다!");
        }

        // Light 컴포넌트 가져오기
        lightComponent = GetComponent<Light>();
        if (lightComponent == null)
        {
            Debug.LogWarning("Light 컴포넌트가 없습니다!");
        }
    }

    public override void Update()
    {
        base.Update();
    }

    // 외부에서 빛 켜기 호출
    public void TurnOnLight()
    {
        if (!isLightOn)
        {
            StartCoroutine(TurnOnLightWithDelay());
        }
    }

    private IEnumerator TurnOnLightWithDelay()
    {
        yield return new WaitForSeconds(delayTime);

        isLightOn = true;
        if (lightComponent != null)
        {
            lightComponent.enabled = true; // 빛 켜기
        }
        if (particleSystem != null)
        {
            particleSystem.Play(); // 파티클 재생
        }
    }

    public void TurnOffLight()
    {
        if (isLightOn)
        {
            isLightOn = false;
            if (lightComponent != null)
            {
                lightComponent.enabled = false; // 빛 끄기
            }
            if (particleSystem != null)
            {
                particleSystem.Stop(); // 파티클 멈춤
            }
        }
    }
}
