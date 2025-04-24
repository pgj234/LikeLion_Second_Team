using UnityEngine;

public class Parry_Skill : Skill
{
    [Header("패링 영역")]
    [SerializeField] private int numberOfDots = 10;
    [SerializeField] private float spaceBetweenDots = 0.2f;
    [SerializeField] private GameObject dotPrefab;
    [SerializeField] private Transform dotsParent;

    [Header("패링 방향 선")]
    [SerializeField] private float lineWidth = 0.2f;
    [SerializeField] private float lineLength = 2f;
    [SerializeField] private Color lineColor = Color.cyan;

    private GameObject[] dots;
    private GameObject lineObject;
    private LineRenderer lineRenderer;
    private Camera mainCamera;

    protected override void Start()
    {
        player = PlayerManager.Instance.player;
        mainCamera = Camera.main;
        GenerateDots();
        GenerateLine();
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
        if (lineRenderer == null)
        {
            Debug.LogWarning("LineRenderer is null in LineActive");
            return;
        }
        lineRenderer.enabled = isActive;
        Debug.Log($"LineRenderer active: {isActive}");
    }

    public void UpdateLine()
    {
        if (lineRenderer == null || !lineRenderer.enabled) return;
        Vector3 mousePos = GetMouseWorldPosition();
        mousePos.z = player.ParryingCheck.position.z; // Z-축 동기화
        Vector3 direction = (mousePos - player.ParryingCheck.position).normalized;
        lineRenderer.SetPosition(0, player.ParryingCheck.position);
        lineRenderer.SetPosition(1, player.ParryingCheck.position + direction * lineLength);
    }

    private void GenerateLine()
    {
        if (lineObject != null) return;
        if (player == null)
        {
            Debug.LogError("Player is null in GenerateLine");
            return;
        }
        lineObject = new GameObject("ParryDirectionLine");
        lineObject.transform.SetParent(player.transform);
        lineRenderer = lineObject.AddComponent<LineRenderer>();
        lineRenderer.startWidth = lineWidth;
        lineRenderer.endWidth = lineWidth;
        lineRenderer.positionCount = 2;
        Material lineMaterial = new Material(Shader.Find("Unlit/Color") ?? Shader.Find("Sprites/Default"));
        lineRenderer.material = lineMaterial;
        lineRenderer.startColor = lineColor;
        lineRenderer.endColor = lineColor;
        lineRenderer.sortingLayerName = "Default";
        lineRenderer.sortingOrder = 10;
        lineRenderer.enabled = false;
        Debug.Log("LineRenderer initialized in Parry_Skill");
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