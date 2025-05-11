using UnityEngine;

public class MoveCreditText : MonoBehaviour
{
    [SerializeField] private GameObject moveObject;
    [SerializeField] private float moveSpeed;

    void Update()
    {
        moveObject.transform.position += new Vector3(0, moveSpeed * Time.deltaTime, 0);
    }
}
