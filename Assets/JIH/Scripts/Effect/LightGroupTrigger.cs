using System.Collections;
using UnityEngine;

public class LightGroupTrigger : MonoBehaviour
{
    public string playerTag = "Player"; // 플레이어 태그
    public LightObject[] lightsGroupA; // 그룹 A: 라이트 1, 3
    public float[] delaysGroupA; // 그룹 A 커스텀 딜레이
    public LightObject[] lightsGroupB; // 그룹 B: 라이트 2, 4
    public float[] delaysGroupB; // 그룹 B 커스텀 딜레이
    public GameObject[] particlePrefabs; // 파티클 프리팹 (라이트 1, 2, 3, 4)
    private float groupToggleDelay = 2f; // 그룹 A와 B 사이 2초 간격
    private bool isTriggered = false; // 단일 트리거 방지
    private bool isGroupAOn = true; // 토글 상태 추적

    private void Awake()
    {
    }

    private void Start()
    {
        // 초기 상태: 모든 라이트 꺼짐
        foreach (LightObject light in lightsGroupA)
        {
            if (light != null)
                light.TurnOffLight();
        }
        foreach (LightObject light in lightsGroupB)
        {
            if (light != null)
                light.TurnOffLight();
        }

        // 파티클 프리팹 초기화: 비활성화
        foreach (GameObject prefab in particlePrefabs)
        {
            if (prefab != null)
            {
                prefab.SetActive(false);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isTriggered) return;

        if (other.gameObject.CompareTag(playerTag))
        {
            isTriggered = true;
            // 파티클 프리팹 활성화 및 재생
            foreach (GameObject prefab in particlePrefabs)
            {
                if (prefab != null)
                {
                    prefab.SetActive(true);
                    var ps = prefab.GetComponent<ParticleSystem>();
                    if (ps != null)
                    {
                        Debug.Log($"Playing particle prefab: {prefab.name}");
                        ps.Play();
                    }
                }
                
            }
            // 주기적 토글 시작
            StartCoroutine(ActivateLightSequence());
        }
    }

    private IEnumerator ActivateLightSequence()
    {
        while (true)
        {
            // 그룹 A 켜기, 그룹 B 끄기 또는 반대
            yield return StartCoroutine(ToggleLights(
                isGroupAOn ? lightsGroupA : lightsGroupB,
                isGroupAOn ? delaysGroupA : delaysGroupB,
                isGroupAOn ? lightsGroupB : lightsGroupA
            ));

            // 상태 토글 및 2초 대기
            isGroupAOn = !isGroupAOn;
            yield return new WaitForSeconds(groupToggleDelay);
        }
    }

    private IEnumerator ToggleLights(LightObject[] lightsToTurnOn, float[] delays, LightObject[] lightsToTurnOff)
    {
        // 끄기: 즉시 실행
        foreach (LightObject light in lightsToTurnOff)
        {
            if (light != null)
            {
                var lightScript = light.GetComponent<LightObject>();
                if (lightScript != null)
                {
                    lightScript.TurnOffLight();
                }
            }
           
        }

        // 켜기: 딜레이 적용
        for (int i = 0; i < lightsToTurnOn.Length; i++)
        {
            LightObject light = lightsToTurnOn[i];
            float delay = i < delays.Length ? delays[i] : 0f;
            if (light != null)
            {
                yield return new WaitForSeconds(delay);
                light.gameObject.SetActive(true);
                var lightScript = light.GetComponent<LightObject>();
                if (lightScript != null)
                {
                    lightScript.TurnOnLight();
                }
            }
           
        }
    }
}