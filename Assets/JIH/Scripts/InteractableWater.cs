using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System.Diagnostics.Contracts;
using UnityEditor;
using UnityEngine.UIElements;
using UnityEditor.UIElements;
using System.Runtime.CompilerServices;


[RequireComponent(typeof(MeshRenderer), typeof(MeshRenderer), typeof(EdgeCollider2D))]
[RequireComponent(typeof(WaterTriggerHandler))]
public class InteractableWater : MonoBehaviour
{
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

    private void Start()
    {
        GenerateMesh();
        _coll = GetComponent<EdgeCollider2D>();
        _coll.points = new Vector2[NumOfXVertices];
        for (int i = 0; i < NumOfXVertices; i++)
        {
            _coll.points[i] = new Vector2(_vertices[i].x, _vertices[i].y);
        }
    }
    private void Reset()
    {
        _coll = GetComponent<EdgeCollider2D>();
        _coll.isTrigger = true;
    }
    public void ResetEdgeCollider()
    {
        _coll = GetComponent<EdgeCollider2D>();
        Vector2[] newPoints = new Vector2[2];
        Vector2 firstPoint = new Vector2(_vertices[_topVerticesIndex[0]].x,_vertices[_topVerticesIndex[0]].y);
        newPoints[0] = firstPoint;
        Vector2 secondPoint = new Vector2(_vertices[_topVerticesIndex[_topVerticesIndex.Length-1]].x, _vertices[_topVerticesIndex[_topVerticesIndex.Length - 1]].y);
        newPoints[1] = secondPoint;
        _coll.offset = Vector2.zero;
        _coll.points = newPoints;

    }
    public void GenerateMesh()
    {
        _mesh = new Mesh();
        //add vertices
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

        // construct triangles
        int[] triangles = new int[(NumOfXVertices - 1) * (NUM_OF_Y_VERTICES - 1) * 6];
        int index = 0;
        for (int y = 0; y < NUM_OF_Y_VERTICES - 1; y++)
        {
            for (int x = 0; x < NumOfXVertices - 1; x++)
            {
                int topLeft = y * NumOfXVertices + x;
                int topRight = topLeft + 1;
                int bottomLeft = (y + 1) * NumOfXVertices + x;
                int bottomRight = bottomLeft + 1;
                //first triangle
                triangles[index++] = topLeft;
                triangles[index++] = bottomLeft;
                triangles[index++] = bottomRight;
                
                //second triangle
                triangles[index++] = topLeft;
                triangles[index++] = bottomRight;
                triangles[index++] = topRight;
            }
        }

        //UVs
        Vector2[] uvs = new Vector2[_vertices.Length];
        for (int i = 0; i < _vertices.Length; i++)
        {
            uvs[i] = new Vector2((_vertices[i].x + Width/2)/Width, (_vertices[i].y + Height/2) / Height);
        }
        if(_meshRenderer == null)
            _meshRenderer = GetComponent<MeshRenderer>();
        if(_meshFilter == null)
        {
            _meshFilter = GetComponent<MeshFilter>();
        }
        _meshRenderer.material = WaterMaterial;
        _mesh.vertices = _vertices;
        _mesh.triangles = triangles;
        _mesh.uv = uvs;

        _mesh.RecalculateNormals();
        _mesh.RecalculateBounds();
        _meshFilter.mesh = _mesh;

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

        Button generateMeshButton = new Button(() => _water.GenerateMesh())
        {
            text = "Generate Mesh"
        };
        root.Add(generateMeshButton);
        Button placeEdgeColliderButton = new Button(() => _water.ResetEdgeCollider())
        {
            text = "Place Edge Collider"
        };
        root.Add(placeEdgeColliderButton);
        return root;

    }

    private void ChangeDimenstions(ref float width, ref float height, float calculateWidthMax, float calculateHeightMax)
    {
        width = Mathf.Max(0.1f, calculateWidthMax);
        height = Mathf.Max(0.1f, calculateHeightMax);
    }
}

