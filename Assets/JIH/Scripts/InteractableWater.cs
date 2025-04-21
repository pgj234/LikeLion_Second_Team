using UnityEngine;
using System.Collections.Generic;
using System.Collections;
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

    private void Reset()
    {
        _coll = GetComponent<EdgeCollider2D>();
        _coll.isTrigger = true;
    }

    public void GenerateMEsh()
    {
        _mesh = new Mesh();
        //add vertices
        _vertices = new Vector3[NumOfXVertices * NUM_OF_Y_VERTICES];
        _topVerticesIndex = new int[NumOfXVertices];
        for(int y = 0; y<NUM_OF_Y_VERTICES; y++)
        {
            for (int x = 0; x < NumOfXVertices; x++)
            {
                float xPos = (x/ (float)(NumOfXVertices -1)) * Width - Width / 2;
                float yPos = (y / (float)(NUM_OF_Y_VERTICES - 1)) * Height - Height / 2;
                _vertices[y* NumOfXVertices + x] = new Vector3(xPos, yPos, 0);
                if(y == NUM_OF_Y_VERTICES - 1)
                {
                    _topVerticesIndex[x] = y * NumOfXVertices + x;
                }
            }
        }
    }

    //construct 

}
