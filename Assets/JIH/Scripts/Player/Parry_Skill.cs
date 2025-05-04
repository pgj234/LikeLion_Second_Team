using UnityEngine;

public class Parry_Skill : Skill
{
    [Header("패링 VFX")]
    [SerializeField] private GameObject vfxPrefab; // 패링 VFX 프리팹
    [SerializeField] private Transform vfxParent;

    [Header("패링 방향 화살표")]
    [SerializeField] private GameObject directionArrow;
    [SerializeField] private float arrowDistance = 10f;
    [SerializeField] private float arrowScale = 1f;

    private Camera mainCamera;
    private ParticleSystem vfxParticleSystem; // Particle System 컴포넌트
    // private Animator vfxAnimator; // Animator 컴포넌트 (필요시 사용)

    protected override void Start()
    {
        player = PlayerManager.Instance.player;
        mainCamera = Camera.main;

        // vfxPrefab 확인
        if (vfxPrefab == null)
        {
            Debug.LogError("VFX Prefab is not assigned!");
            return;
        }

        // vfxParent 설정
        if (vfxParent == null)
        {
            vfxParent = new GameObject("ParryVfxParent").transform;
            vfxParent.parent = player.transform;
        }

        // VFX 프리팹을 vfxParent의 자식으로 설정 (씬에 없으면 인스턴스화)
        if (!vfxPrefab.activeSelf)
        {
            vfxPrefab = Object.Instantiate(vfxPrefab, player.ParryingCheck.position, Quaternion.identity, vfxParent);
        }

        // Particle System 또는 Animator 컴포넌트 캐싱
        vfxParticleSystem = vfxPrefab.GetComponent<ParticleSystem>();
        // vfxAnimator = vfxPrefab.GetComponent<Animator>(); // Animator 사용 시
        if (vfxParticleSystem == null /* && vfxAnimator == null */)
        {
            Debug.LogWarning("VFX Prefab has no ParticleSystem or Animator component!");
        }

        vfxPrefab.SetActive(false); // 초기 비활성화
    }

    // VFX 활성화/비활성화
    public void VfxActive(bool isActive)
    {
        if (vfxPrefab == null) return;

        vfxPrefab.SetActive(isActive);

        if (isActive && vfxParticleSystem != null)
        {
            vfxParticleSystem.Play(); // Particle System 재생
        }
        else if (!isActive && vfxParticleSystem != null)
        {
            vfxParticleSystem.Stop(); // Particle System 정지
        }

        /*
        // Animator 사용 시
        if (isActive && vfxAnimator != null)
        {
            vfxAnimator.Play("YourAnimationName"); // 애니메이션 이름 지정
        }
        else if (!isActive && vfxAnimator != null)
        {
            vfxAnimator.StopPlayback();
        }
        */
    }

    // VFX 위치 및 스케일 업데이트
    public void UpdateVfx()
    {
        if (vfxPrefab == null || !vfxPrefab.activeSelf) return;

        // 위치를 ParryingCheck에 맞춤
        vfxPrefab.transform.position = player.ParryingCheck.position;

        // 스케일을 ParryingCheckRadius에 맞춤
        float radius = player.ParryingCheckRadius;
        vfxPrefab.transform.localScale = Vector3.one * radius * 2; // 반지름에 맞게 스케일 조정
    }

    public void LineActive(bool isActive)
    {
        if (directionArrow == null)
        {
            Debug.LogWarning("DirectionArrow is null in LineActive");
            return;
        }
        directionArrow.SetActive(isActive);
    }

    public void UpdateLine()
    {
        if (directionArrow == null || !directionArrow.activeSelf) return;

        Vector3 mousePos = GetMouseWorldPosition();
        mousePos.z = player.ParryingCheck.position.z;
        Vector3 direction = (mousePos - player.ParryingCheck.position).normalized;

        directionArrow.transform.position = player.ParryingCheck.position + direction * arrowDistance;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        directionArrow.transform.rotation = Quaternion.Euler(0, 0, angle);
        directionArrow.transform.localScale = Vector3.one * arrowScale;
    }

    public Vector3 GetMouseWorldPositionPublic()
    {
        return GetMouseWorldPosition();
    }

    private Vector3 GetMouseWorldPosition()
    {
        Vector2 mousePos = Input.mousePosition;
        Ray ray = mainCamera.ScreenPointToRay(mousePos);
        Plane groundPlane = new Plane(Vector3.up, player.transform.position);
        if (groundPlane.Raycast(ray, out float distance))
        {
            Vector3 point = ray.GetPoint(distance);
            Debug.DrawRay(ray.origin, ray.direction * distance, Color.green, 1f);
            return point;
        }
        Vector3 fallback = ray.GetPoint(10f);
        Debug.LogWarning($"Mouse World Position fallback: {fallback}");
        return fallback;
    }
}