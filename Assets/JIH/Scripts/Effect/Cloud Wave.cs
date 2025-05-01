using UnityEngine;

public class CloudWave : MonoBehaviour
{

    public float amplitude = 0.5f;  // 흔들림의 범위
    public float frequency = 1f;    // 흔들림 속도

    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        float offset = Mathf.Sin(Time.time * frequency) * amplitude;
        transform.position = startPos + new Vector3(offset, 0f, 0f);
    }
}
