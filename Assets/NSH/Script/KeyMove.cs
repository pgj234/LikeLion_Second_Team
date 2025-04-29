using UnityEngine;

public class KeyMove : MonoBehaviour
{
    public float floatSpeed = 1f;       // 위아래로 움직이는 속도
    public float floatHeight = 0.5f;    // 움직이는 높이

    private Vector3 startPos;

    void Start()
    {
        startPos = transform.localPosition;
    }

    void Update()
    {
        float newY = Mathf.Sin(Time.time * floatSpeed) * floatHeight;
        transform.localPosition = startPos + new Vector3(0f, newY, 0f);
    }
}
