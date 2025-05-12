using System.Collections.Generic;
using UnityEngine;

public class LaserBeam : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public float maxDistance = 100f;
    public int maxReflections = 10;
    public Transform laserTransform;

    void Update()
    {
        Vector2 origin = laserTransform.position;
        Vector2 direction = laserTransform.right;

        List<Vector3> points = new List<Vector3>();
        points.Add(origin);

        for (int i = 0; i < maxReflections; i++)
        {
            RaycastHit2D hit = Physics2D.Raycast(origin, direction, maxDistance);


            Debug.DrawRay(origin, direction * maxDistance, Color.red);

            if (hit.collider != null)
            {
                // LaserReceiver나 Mirror인 경우에만 처리
                if (hit.collider.GetComponent<LaserReceiver>() != null)
                {
                    points.Add(hit.point);
                    hit.collider.GetComponent<LaserReceiver>()?.Activate();
                    break;
                }
                else if (hit.collider.GetComponent<Mirror>() != null)
                {
                    points.Add(hit.point);
                    direction = Vector2.Reflect(direction, hit.normal);
                    origin = hit.point + direction.normalized * 0.01f;
                    continue;
                }
                // 다른 물체는 무시하고 계속 진행
                origin = hit.point + direction.normalized * 0.01f;
                continue;
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
