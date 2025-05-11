using UnityEngine;
using DG.Tweening;

public class LavaShape : MonoBehaviour
{  public enum MoveDirection
    {
        None,
        LeftToRight,   // → →
        RightToLeft,   // ← ←
        TopToBottom,   // ↑ ↑
        BottomToTop    // ↓ ↓
    }

    [Header("움직임 설정")]
    [SerializeField] private float childMoveSpeed = 1f;    // 자식의 이동 속도
    [SerializeField] private float moveRange = 0.5f;       // 진동 범위

    [Header("왕복 운동 설정")]
    [SerializeField] private MoveDirection primaryDirection = MoveDirection.None;
    [SerializeField] private MoveDirection secondaryDirection = MoveDirection.None;

    private Vector3 initialPosition;

    private void Start()
    {
        initialPosition = transform.localPosition;
    }

    private void Update()
    {
        float oscillationX = 0f;
        float oscillationY = 0f;

        // 1️⃣ 첫 번째 방향 처리
        switch (primaryDirection)
        {
            case MoveDirection.LeftToRight:
                oscillationX = Mathf.Sin(Time.time * childMoveSpeed) * moveRange;
                break;

            case MoveDirection.RightToLeft:
                oscillationX = -Mathf.Sin(Time.time * childMoveSpeed) * moveRange;
                break;

            case MoveDirection.TopToBottom:
                oscillationY = Mathf.Sin(Time.time * childMoveSpeed) * moveRange;
                break;

            case MoveDirection.BottomToTop:
                oscillationY = -Mathf.Sin(Time.time * childMoveSpeed) * moveRange;
                break;
        }

        // 2️⃣ 두 번째 방향 처리
        switch (secondaryDirection)
        {
            case MoveDirection.LeftToRight:
                oscillationX += Mathf.Sin(Time.time * childMoveSpeed) * moveRange;
                break;

            case MoveDirection.RightToLeft:
                oscillationX -= Mathf.Sin(Time.time * childMoveSpeed) * moveRange;
                break;

            case MoveDirection.TopToBottom:
                oscillationY += Mathf.Sin(Time.time * childMoveSpeed) * moveRange;
                break;

            case MoveDirection.BottomToTop:
                oscillationY -= Mathf.Sin(Time.time * childMoveSpeed) * moveRange;
                break;
        }

        // 3️⃣ 최종 위치 설정
        transform.localPosition = new Vector3(initialPosition.x + oscillationX, initialPosition.y + oscillationY, initialPosition.z);
    }
} 