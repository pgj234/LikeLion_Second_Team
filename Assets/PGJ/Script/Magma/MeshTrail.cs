using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer), typeof(MeshCollider))]
public class MeshTrail : MonoBehaviour
{
    public float trailWidth = 0.2f;
    public float pointSpacing = 0.1f;
    public float trailLifetime = 2f;

    private List<Vector3> points = new List<Vector3>();
    private Mesh trailMesh;
    private MeshCollider meshCollider;
    private MeshFilter meshFilter;

    private float distanceSinceLastPoint = 0f;

    private void Start()
    {
        trailMesh = new Mesh();
        meshFilter = GetComponent<MeshFilter>();
        meshCollider = GetComponent<MeshCollider>();
    }

    private void Update()
    {
        Vector3 currentPosition = transform.position;

        if (points.Count == 0 || Vector3.Distance(points[points.Count - 1], currentPosition) > pointSpacing)
        {
            points.Add(currentPosition);
        }

        // Remove old points
        float timeNow = Time.time;
        while (points.Count > 2 && Vector3.Distance(points[0], currentPosition) > trailLifetime)
        {
            points.RemoveAt(0);
        }

        GenerateTrailMesh();
    }

    void GenerateTrailMesh()
    {
        if (points.Count < 2) return;

        Vector3[] vertices = new Vector3[points.Count * 2];
        int[] triangles = new int[(points.Count - 1) * 6];
        Vector2[] uvs = new Vector2[vertices.Length];

        for (int i = 0; i < points.Count; i++)
        {
            Vector3 forward = Vector3.zero;
            if (i < points.Count - 1)
                forward += (points[i + 1] - points[i]).normalized;
            if (i > 0)
                forward += (points[i] - points[i - 1]).normalized;

            Vector3 left = Vector3.Cross(forward.normalized, Vector3.up) * trailWidth * 0.5f;

            vertices[i * 2] = points[i] + left;
            vertices[i * 2 + 1] = points[i] - left;

            uvs[i * 2] = new Vector2(0, i / (float)points.Count);
            uvs[i * 2 + 1] = new Vector2(1, i / (float)points.Count);

            if (i < points.Count - 1)
            {
                int start = i * 6;
                int vi = i * 2;

                triangles[start] = vi;
                triangles[start + 1] = vi + 2;
                triangles[start + 2] = vi + 1;

                triangles[start + 3] = vi + 1;
                triangles[start + 4] = vi + 2;
                triangles[start + 5] = vi + 3;
            }
        }

        trailMesh.Clear();
        trailMesh.vertices = vertices;
        trailMesh.triangles = triangles;
        trailMesh.uv = uvs;
        trailMesh.RecalculateNormals();

        meshFilter.mesh = trailMesh;
        meshCollider.sharedMesh = null; // 꼭 null로 초기화 후 재할당
        meshCollider.sharedMesh = trailMesh;
    }
}