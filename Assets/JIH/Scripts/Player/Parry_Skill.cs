using UnityEngine;

public class Parry_Skill : Skill
{
    [Header("패링 영역")]
    [SerializeField] private int numberOfDots = 10;
    [SerializeField] private float spaceBetweenDots = 0.2f;
    [SerializeField] private GameObject dotPrefab;
    [SerializeField] private Transform dotsParent;

    [Header("패링 방향 화살표")]
    [SerializeField] private GameObject directionArrow; // 화살표 오브젝트
    [SerializeField] private float arrowDistance = 10f; // 플레이어와 화살표 간 거리
    [SerializeField] private float arrowScale = 1f; // 화살표 크기

    private GameObject[] dots;
    private Camera mainCamera;

    protected override void Start()
    {
        player = PlayerManager.Instance.player;
        mainCamera = Camera.main;
        GenerateDots();
    }

    public void DotsActive(bool isActive)
    {
        if (dots == null) return;
        foreach (var dot in dots)
        {
            dot.SetActive(isActive);
        }
    }

    public void UpdateDots()
    {
        if (dots == null) return;
        float radius = player.ParryingCheckRadius;
        Vector3 center = player.ParryingCheck.position;
        for (int i = 0; i < numberOfDots; i++)
        {
            float angle = i * 360f / numberOfDots;
            Vector3 pos = center + new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad), 0) * radius;
            dots[i].transform.position = pos;
        }
    }

    private void GenerateDots()
    {
        if (dots != null) return;
        if (player == null)
        {
            Debug.LogError("Player is null in GenerateDots");
            return;
        }
        dots = new GameObject[numberOfDots];
        if (dotsParent == null)
        {
            dotsParent = new GameObject("ParryDotsParent").transform;
            dotsParent.parent = player.transform;
        }
        if (dotPrefab == null)
        {
            Debug.LogError("Dot Prefab is not assigned!");
            return;
        }
        for (int i = 0; i < numberOfDots; i++)
        {
            dots[i] = Object.Instantiate(dotPrefab, player.ParryingCheck.position, Quaternion.identity, dotsParent);
            dots[i].SetActive(false);
        }
    }

    public void LineActive(bool isActive)
    {
        if (directionArrow == null)
        {
            Debug.LogWarning("DirectionArrow is null in LineActive");
            return;
        }
        directionArrow.SetActive(isActive);
        Debug.Log($"DirectionArrow active: {isActive}");
    }

    public void UpdateLine()
    {
        if (directionArrow == null || !directionArrow.activeSelf) return;

        Vector3 mousePos = GetMouseWorldPosition();
        mousePos.z = player.ParryingCheck.position.z; // Z-축 동기화
        Vector3 direction = (mousePos - player.ParryingCheck.position).normalized;

        // 화살표 위치 설정
        directionArrow.transform.position = player.ParryingCheck.position + direction * arrowDistance;

        // 화살표 회전
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        directionArrow.transform.rotation = Quaternion.Euler(0, 0, angle);

        // 화살표 크기 설정
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