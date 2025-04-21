using UnityEngine;

public class EffectManager : MonoBehaviour
{
    public static EffectManager instance;

    private void Awake()
    {
        // 이미 존재하면 삭제 (중복 방지)
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject); // 씬 이동 시에도 유지하고 싶다면
    }

    [Header("(기본지속시간0.5초)")]
    [Header("이펙트 프리팹들")]

    public GameObject dashEffectPrefab;
    public GameObject jumpEffectPrefab;

    /// 위치에 이펙트를 생성하고 지정 시간 후 삭제
    public void SpawnEffect(GameObject effectPrefab, Vector3 position, Quaternion quaternion, float duration = 0.5f)
    {
        if (effectPrefab == null)
        {
            Debug.LogWarning("이펙트 프리팹이 설정되지 않았습니다.");
            return;
        }
        GameObject effect = Instantiate(effectPrefab, position, quaternion);
        Destroy(effect, duration); // 지정된 시간 후 제거
    }

    // 사용 예시: EffectManager.instance.SpawnDashEffect(transform.position);
    public void SpawnDashEffect(Vector3 position, Quaternion quaternion, float duration = 0.5f)
    {
        SpawnEffect(dashEffectPrefab, position, quaternion, duration);
    }

    public void SpawnJumpEffect(Vector3 position, Quaternion quaternion, float duration = 0.5f)
    {
        SpawnEffect(jumpEffectPrefab, position, quaternion, duration);
    }

}
