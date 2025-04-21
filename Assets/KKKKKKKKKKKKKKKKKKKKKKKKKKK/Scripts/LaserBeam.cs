using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;

public class LaserBeam : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public float maxDistance = 100f;
    public int maxReflections = 5;
    public Transform laserTransform;

    void Update()
    {
        Vector2 origin = laserTransform.position;
        Vector2 direction = laserTransform.right;

        List<Vector3> points = new List<Vector3>();
        points.Add(origin);  // Vector2도 Vector3 리스트에 자동 변환 가능

        for (int i = 0; i < maxReflections; i++)
        {
            RaycastHit2D hit = Physics2D.Raycast(origin, direction, maxDistance);

            //Debug.DrawRay(origin, direction * maxDistance, Color.red); // 씬뷰에서 레이 확인
            //Debug.Log("Hit: " + hit.collider?.name);

            if (hit.collider != null)
            {
                points.Add(hit.point);

                if (hit.collider.GetComponent<LaserReceiver>() != null)
                {
                    hit.collider.GetComponent<LaserReceiver>()?.Activate();
                    break;
                }

                if (hit.collider.CompareTag("Untagged"))
                    break;

                if (hit.collider.CompareTag("Mirror"))
                {
                    direction = Vector2.Reflect(direction, hit.normal);
                    origin = hit.point + direction.normalized * 0.01f;
                    continue;
                }

                break;
            }
            else
            {
                points.Add(origin + direction.normalized * maxDistance);
                break;
            }


        }
        lineRenderer.positionCount = points.Count;
        lineRenderer.SetPositions(points.ToArray());

    }
}
