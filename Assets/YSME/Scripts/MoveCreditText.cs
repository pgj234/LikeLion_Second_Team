using UnityEngine;

public class MoveCreditText : MonoBehaviour
{
    [SerializeField] private GameObject moveObject;
    [SerializeField] private float moveSpeed;
    public bool isMoving = true;

    void Update()
    {
        if (isMoving == true)
        {
            moveObject.transform.position += new Vector3(0, moveSpeed * Time.deltaTime, 0);
        }
    }
}
