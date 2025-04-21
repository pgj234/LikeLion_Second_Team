using UnityEngine;

public class InteractableWater : MonoBehaviour
{
    [Header("Mesh Generation")]
    [Range(2, 500)] public int NumOfVertices = 70;
    public float Width = 10f;
    public float Height = 4f;
}
