using UnityEngine;

public class LavaBox : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float smoothTime = 0.3f;
    [SerializeField] private float xOffset = 0f; // 플레이어 x좌표 오프셋
    [SerializeField] private LineRenderer pathLine;
    [SerializeField] private bool followPath = false;
    
    private Transform player;
    private Vector2 currentVelocity;
    private int currentPathIndex = 0;
    private Vector3[] pathPoints;
    private float pathProgress = 0f;

    private void Start()
    {
        // 플레이어 찾기
        player = GameObject.FindGameObjectWithTag("Player")?.transform;

        // 경로 초기화
        if (pathLine != null)
        {
            pathPoints = new Vector3[pathLine.positionCount];
            pathLine.GetPositions(pathPoints);
        }
    }

    private void Update()
    {
        if (followPath && pathLine != null)
        {
            FollowPath();
        }
        else if (player != null)
        {
            FollowPlayer();
        }
    }

    private void FollowPath()
    {
        if (pathPoints == null || pathPoints.Length < 2) return;

        // 현재 경로 세그먼트 계산
        int nextIndex = (currentPathIndex + 1) % pathPoints.Length;
        Vector3 currentPoint = pathPoints[currentPathIndex];
        Vector3 nextPoint = pathPoints[nextIndex];

        // 경로를 따라 부드럽게 이동
        pathProgress += moveSpeed * Time.deltaTime;
        float distance = Vector3.Distance(currentPoint, nextPoint);
        
        if (pathProgress >= distance)
        {
            pathProgress = 0f;
            currentPathIndex = nextIndex;
        }

        // 현재 위치에서 다음 위치까지 보간
        Vector3 targetPosition = Vector3.Lerp(currentPoint, nextPoint, pathProgress / distance);
        transform.position = Vector2.SmoothDamp(
            transform.position,
            targetPosition,
            ref currentVelocity,
            smoothTime,
            moveSpeed
        );
    }

    private void FollowPlayer()
    {
        // 플레이어를 부드럽게 따라가기 (x좌표에 오프셋 적용)
        Vector2 targetPosition = new Vector2(
            player.position.x + xOffset,
            player.position.y
        );
        transform.position = Vector2.SmoothDamp(
            transform.position,
            targetPosition,
            ref currentVelocity,
            smoothTime,
            moveSpeed
        );
    }

    private void OnDrawGizmos()
    {
        if (pathLine != null && followPath)
        {
            Gizmos.color = Color.yellow;
            for (int i = 0; i < pathLine.positionCount - 1; i++)
            {
                Gizmos.DrawLine(pathLine.GetPosition(i), pathLine.GetPosition(i + 1));
            }
            // 마지막 점과 첫 점 연결
            if (pathLine.positionCount > 2)
            {
                Gizmos.DrawLine(pathLine.GetPosition(pathLine.positionCount - 1), pathLine.GetPosition(0));
            }
        }
    }
} 