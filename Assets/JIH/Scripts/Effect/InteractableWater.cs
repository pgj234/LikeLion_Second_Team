using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System.Diagnostics.Contracts;
using UnityEditor;
using UnityEngine.UIElements;
using UnityEditor.UIElements;
using System.Runtime.CompilerServices;


[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer), typeof(EdgeCollider2D))]
[RequireComponent(typeof(WaterTriggerHandler))]
public class InteractableWater : MonoBehaviour
{
    [Header("Springs")]
    [SerializeField] private float _spriteConstant = 1.4f;
    [SerializeField] private float _damping = 1.1f;
    [SerializeField] private float _spread = 6.5f;
    [SerializeField, Range(1, 10)] private int _wavePropogationIterations = 8;
    [SerializeField, Range(0f, 20f)] private float _speedMult = 5.5f;

    [Header("Force")]
    public float ForceMultiplier = 0.2f;
    [Range(1f, 50f)] public float MaxForce = 5f;

    [Header("Collision")]
    [SerializeField, Range(1f, 10f)] private float _playerCollisionRadiusMult = 4.15f;


    [Header("Mesh Generation")]
    [Range(2, 500)] public int NumOfXVertices = 70;
    public float Width = 10f;
    public float Height = 4f;
    public Material WaterMaterial;
    private const int NUM_OF_Y_VERTICES = 2;

    [Header("Gizmos")]
    public Color GizmoColor = Color.white;

    private Mesh _mesh;
    private MeshRenderer _meshRenderer;
    private MeshFilter _meshFilter;
    private Vector3[] _vertices;
    private int[] _topVerticesIndex;

    private EdgeCollider2D _coll;

    private Vector3 _lastPosition;
    private Quaternion _lastRotation;
    private Vector3 _lastScale;

    [Header("Sine Wave")]
    [SerializeField] private float _waveSpeed = 4f;
    [SerializeField] private float _waveFlowDirection = 0.5f;
    [SerializeField] private float _waveAmplitude = 0.5f;

    private class WaterPoint
    {
        public float velocity, pos, targetHeight;
    }
    private List<WaterPoint> _waterPoints = new List<WaterPoint>();

    private void Start()
    {
        _coll = GetComponent<EdgeCollider2D>();

        GenerateMesh();
        CreateWaterPoints();
        // 초기 transform 값 저장
        _lastPosition = transform.position;
        _lastRotation = transform.rotation;
        _lastScale = transform.localScale;
    }
    private void Reset()
    {
        _coll = GetComponent<EdgeCollider2D>();
        _coll.isTrigger = true;
    }
    private void FixedUpdate()
    {
        //update all spring positions
        for (int i = 0; i < _waterPoints.Count - 1; i++)
        {
            WaterPoint point = _waterPoints[i];
            float x = point.pos - point.targetHeight;
            float acceleration = -_spriteConstant * x - _damping * point.velocity;
            point.pos += point.velocity * _speedMult * Time.fixedDeltaTime;

            // 사인파형 변형 추가
            float sineWave = Mathf.Sin(Time.time * _waveSpeed + i * _waveFlowDirection) * _waveAmplitude;
            point.pos += sineWave;

            _vertices[_topVerticesIndex[i]].y = point.pos;
            point.velocity += acceleration * _speedMult * Time.fixedDeltaTime;

            // 디버깅 로그 추가
            if (i == 0) // 첫 번째 점만 로그로 확인
            {
                Debug.Log($"WaterPoint {i}: pos={point.pos}, vertex.y={_vertices[_topVerticesIndex[i]].y}");
            }
        }
        //wave propogation
        for (int j = 0; j < _wavePropogationIterations; j++)
        {
            for (int i = 1; i < _waterPoints.Count - 1; i++)
            {
                float leftDelta = _spread * (_waterPoints[i].pos - _waterPoints[i - 1].pos) * _speedMult * Time.fixedDeltaTime;
                _waterPoints[i - 1].velocity += leftDelta;
                float rightDelta = _spread * (_waterPoints[i].pos - _waterPoints[i + 1].pos) * _speedMult * Time.fixedDeltaTime;
                _waterPoints[i + 1].velocity += rightDelta;
            }
        }
        //update the mesh
        _mesh.vertices = _vertices;
        ResetEdgeCollider(); // 메쉬 업데이트 후 EdgeCollider2D 즉시 갱신
                             // EdgeCollider2D 점 디버깅
        Vector2[] colliderPoints = _coll.points;
        for (int i = 0; i < colliderPoints.Length; i++)
        {
            if (i < NumOfXVertices)
            {
                Vector3 vertex = _vertices[_topVerticesIndex[i]];
            }
        }

    }
    private void Update()
    {
        // transform 변경 감지
        if (transform.position != _lastPosition ||
            transform.rotation != _lastRotation ||
            transform.localScale != _lastScale)
        {
            // transform이 변경되면 메쉬와 물리 상태 갱신
            GenerateMesh();
            CreateWaterPoints();
            ResetEdgeCollider();

            _mesh.vertices = _vertices;
            _meshFilter.mesh = _mesh;

            // 새로운 transform 값 저장
            _lastPosition = transform.position;
            _lastRotation = transform.rotation;
            _lastScale = transform.localScale;
            // 즉시 물리 상태 갱신
            for (int i = 0; i < _waterPoints.Count; i++)
            {
                _waterPoints[i].pos = _vertices[_topVerticesIndex[i]].y;
                _waterPoints[i].velocity = 0f;
            }
        }
    }
    public void Splash(Collider2D collision, float force)
    {
        float radius = collision.bounds.extents.x * _playerCollisionRadiusMult;
        Vector2 center = collision.transform.position;
        for (int i = 0; i < _waterPoints.Count; i++)
        {
            Vector2 vertexWorldPos = transform.TransformPoint(_vertices[_topVerticesIndex[i]]);
            if(IsPointInsideCircle(vertexWorldPos, center, radius))
            {
                _waterPoints[i].velocity = force;
            }
        }
    }
    private bool IsPointInsideCircle(Vector2 point, Vector2 center, float radius)
    {
        float distanceSquared = (point - center).sqrMagnitude;
        return distanceSquared <= radius * radius;

    }

    public void ResetEdgeCollider()
    {
        //_coll = GetComponent<EdgeCollider2D>();
        //Vector2[] newPoints = new Vector2[2];
        //Vector2 firstPoint = new Vector2(_vertices[_topVerticesIndex[0]].x, _vertices[_topVerticesIndex[0]].y);
        //newPoints[0] = firstPoint;
        //Vector2 secondPoint = new Vector2(_vertices[_topVerticesIndex[_topVerticesIndex.Length - 1]].x, _vertices[_topVerticesIndex[_topVerticesIndex.Length - 1]].y);
        //newPoints[1] = secondPoint;
        //_coll.offset = Vector2.zero;
        //_coll.points = newPoints;
        _coll = GetComponent<EdgeCollider2D>();
        Vector2[] newPoints = new Vector2[NumOfXVertices]; // 모든 상단 정점을 사용

        for (int i = 0; i < NumOfXVertices; i++)
        {
            // _vertices는 로컬 좌표이므로 스케일링만 고려
            Vector3 scaledVertex = _vertices[_topVerticesIndex[i]];
            // EdgeCollider2D는 로컬 좌표를 사용하므로 스케일링 반영 불필요
            newPoints[i] = new Vector2(scaledVertex.x, scaledVertex.y);
        }

        _coll.offset = Vector2.zero;
        _coll.points = newPoints;

    }
    public void GenerateMesh()
    {
        _mesh = new Mesh();
        _vertices = new Vector3[NumOfXVertices * NUM_OF_Y_VERTICES];
        _topVerticesIndex = new int[NumOfXVertices];

        for (int y = 0; y < NUM_OF_Y_VERTICES; y++)
        {
            for (int x = 0; x < NumOfXVertices; x++)
            {
                float xPos = (x / (float)(NumOfXVertices - 1)) * Width - Width / 2;
                float yPos = (y / (float)(NUM_OF_Y_VERTICES - 1)) * Height - Height / 2;
                _vertices[y * NumOfXVertices + x] = new Vector3(xPos, yPos, 0);
                if (y == NUM_OF_Y_VERTICES - 1)
                {
                    _topVerticesIndex[x] = y * NumOfXVertices + x;
                }
            }
        }

        int[] triangles = new int[(NumOfXVertices - 1) * (NUM_OF_Y_VERTICES - 1) * 6];
        int index = 0;
        for (int y = 0; y < NUM_OF_Y_VERTICES - 1; y++)
        {
            for (int x = 0; x < NumOfXVertices - 1; x++)
            {
                int bottomLeft = y * NumOfXVertices + x;
                int bottomRight = bottomLeft + 1;
                int topLeft = bottomLeft + NumOfXVertices;
                int topRight = topLeft + 1;

                triangles[index++] = bottomLeft;
                triangles[index++] = topLeft;
                triangles[index++] = bottomRight;

                triangles[index++] = bottomRight;
                triangles[index++] = topLeft;
                triangles[index++] = topRight;
            }
        }

        Vector2[] uvs = new Vector2[_vertices.Length];
        for (int i = 0; i < _vertices.Length; i++)
        {
            uvs[i] = new Vector2((_vertices[i].x + Width / 2) / Width, (_vertices[i].y + Height / 2) / Height);
        }

        if (_meshRenderer == null)
            _meshRenderer = GetComponent<MeshRenderer>();
        if (_meshFilter == null)
            _meshFilter = GetComponent<MeshFilter>();

        _meshRenderer.material = WaterMaterial;
        _mesh.vertices = _vertices;
        _mesh.triangles = triangles;
        _mesh.uv = uvs;

        _mesh.RecalculateNormals();
        _mesh.RecalculateBounds();
        _meshFilter.mesh = _mesh;

        CreateWaterPoints();
        ResetEdgeCollider();

    }

    private void CreateWaterPoints()
    {
        _waterPoints.Clear();
        for (int i = 0; i < _topVerticesIndex.Length; i++)
        {
            _waterPoints.Add(new WaterPoint
            {
                pos = _vertices[_topVerticesIndex[i]].y,
                targetHeight = _vertices[_topVerticesIndex[i]].y,
                velocity = 0f // 초기화 시 velocity도 0으로 설정
            });
        }
    }


}

[CustomEditor(typeof(InteractableWater))]
public class InteractableWaterEditor : Editor
{
    private InteractableWater _water;

    private void OnEnable()
    {
        _water = (InteractableWater)target;
    }

    public override VisualElement CreateInspectorGUI()
    {
        VisualElement root = new VisualElement();
        InspectorElement.FillDefaultInspector(root, serializedObject, this);
        root.Add(new VisualElement { style = { height = 10 } });

        Button generateMeshButton = new Button(() => _water.GenerateMesh()) { text = "Generate Mesh" };
        root.Add(generateMeshButton);
        Button placeEdgeColliderButton = new Button(() => _water.ResetEdgeCollider()) { text = "Place Edge Collider" };
        root.Add(placeEdgeColliderButton);
        return root;
    }

    private void ChangeDimenstions(ref float width, ref float height, float calculateWidthMax, float calculateHeightMax)
    {
        width = Mathf.Max(0.1f, Mathf.Abs(calculateWidthMax));
        height = Mathf.Max(0.1f, Mathf.Abs(calculateHeightMax));
    }

    private void OnSceneGUI()
    {
        Handles.color = _water.GizmoColor;
        Handles.matrix = _water.transform.localToWorldMatrix;

        Vector3 center = Vector3.zero;
        Vector3 size = new Vector3(_water.Width, _water.Height, 0.1f);
        Handles.DrawWireCube(center, size);

        Vector3[] corners = new Vector3[4];
        corners[0] = new Vector3(-_water.Width / 2, -_water.Height / 2, 0);
        corners[1] = new Vector3(_water.Width / 2, -_water.Height / 2, 0);
        corners[2] = new Vector3(-_water.Width / 2, _water.Height / 2, 0);
        corners[3] = new Vector3(_water.Width / 2, _water.Height / 2, 0);

        float handleSize = HandleUtility.GetHandleSize(_water.transform.position) * 0.1f;
        Vector3 snap = Vector3.one * 0.1f;

        EditorGUI.BeginChangeCheck();
        Vector3 newBottomLeft = Handles.FreeMoveHandle(corners[0], handleSize, snap, Handles.CubeHandleCap);
        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(_water, "Adjust Water Dimensions");
            float newWidth = corners[1].x - newBottomLeft.x;
            float newHeight = corners[3].y - newBottomLeft.y;
            ChangeDimenstions(ref _water.Width, ref _water.Height, newWidth, newHeight);
            Vector3 worldDelta = _water.transform.InverseTransformPoint(newBottomLeft) - _water.transform.InverseTransformPoint(corners[0]);
            _water.transform.position += worldDelta / 2;
            _water.GenerateMesh(); // 즉시 메쉬 갱신
            _water.ResetEdgeCollider();
        }

        EditorGUI.BeginChangeCheck();
        Vector3 newBottomRight = Handles.FreeMoveHandle(corners[1], handleSize, snap, Handles.CubeHandleCap);
        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(_water, "Adjust Water Dimensions");
            float newWidth = newBottomRight.x - corners[0].x;
            float newHeight = corners[3].y - newBottomRight.y;
            ChangeDimenstions(ref _water.Width, ref _water.Height, newWidth, newHeight);
            Vector3 worldDelta = _water.transform.InverseTransformPoint(newBottomRight) - _water.transform.InverseTransformPoint(corners[1]);
            _water.transform.position += new Vector3((newBottomRight.x - corners[1].x) / 2, (newBottomRight.y - corners[1].y) / 2, 0);
            _water.GenerateMesh();
            _water.ResetEdgeCollider();
        }

        EditorGUI.BeginChangeCheck();
        Vector3 newTopLeft = Handles.FreeMoveHandle(corners[2], handleSize, snap, Handles.CubeHandleCap);
        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(_water, "Adjust Water Dimensions");
            float newWidth = corners[3].x - newTopLeft.x;
            float newHeight = newTopLeft.y - corners[0].y;
            ChangeDimenstions(ref _water.Width, ref _water.Height, newWidth, newHeight);
            Vector3 worldDelta = _water.transform.InverseTransformPoint(newTopLeft) - _water.transform.InverseTransformPoint(corners[2]);
            _water.transform.position += new Vector3((newTopLeft.x - corners[2].x) / 2, (newTopLeft.y - corners[2].y) / 2, 0);
            _water.GenerateMesh();
            _water.ResetEdgeCollider();
        }

        EditorGUI.BeginChangeCheck();
        Vector3 newTopRight = Handles.FreeMoveHandle(corners[3], handleSize, snap, Handles.CubeHandleCap);
        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(_water, "Adjust Water Dimensions");
            float newWidth = newTopRight.x - corners[2].x;
            float newHeight = newTopRight.y - corners[1].y;
            ChangeDimenstions(ref _water.Width, ref _water.Height, newWidth, newHeight);
            Vector3 worldDelta = _water.transform.InverseTransformPoint(newTopRight) - _water.transform.InverseTransformPoint(corners[3]);
            _water.transform.position += new Vector3((newTopRight.x - corners[3].x) / 2, (newTopRight.y - corners[3].y) / 2, 0);
            _water.GenerateMesh();
            _water.ResetEdgeCollider();
        }

        if (GUI.changed)
        {
            serializedObject.Update();
            serializedObject.ApplyModifiedProperties();
        }
        Handles.matrix = Matrix4x4.identity;
    }
}

