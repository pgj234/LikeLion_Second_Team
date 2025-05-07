using UnityEngine;

public class Object_Stop : MonoBehaviour
{
    [SerializeField] Vector3 destinationVectorPos;

    void Update()
    {
        if (destinationVectorPos == transform.position)
        {
            GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
            enabled = false;
        }
    }
}
