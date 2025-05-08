using UnityEngine;
using System.Collections;

public class PlatformMoveLeft : MonoBehaviour
{
    public float moveDistance = 2f;           // 왼쪽으로 이동할 거리
    public float moveDuration = 1f;           // 이동 시간
    public bool returnToStart = false;        // 다시 원위치로 돌아올지 여부

    private Vector3 startPos;
    private bool isMoving = false;

    void Start()
    {
        startPos = transform.position;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (!isMoving && collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(MovePlatformLeft());
        }
    }

    IEnumerator MovePlatformLeft()
    {
        isMoving = true;

        Vector3 endPos = startPos + Vector3.left * moveDistance;
        float elapsed = 0f;

        while (elapsed < moveDuration)
        {
            transform.position = Vector3.Lerp(startPos, endPos, elapsed / moveDuration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.position = endPos;

        if (returnToStart)
        {
            yield return new WaitForSeconds(1f); // 잠시 대기 후 다시 돌아옴
            elapsed = 0f;

            while (elapsed < moveDuration)
            {
                transform.position = Vector3.Lerp(endPos, startPos, elapsed / moveDuration);
                elapsed += Time.deltaTime;
                yield return null;
            }

            transform.position = startPos;
        }

        isMoving = false;
    }
}
