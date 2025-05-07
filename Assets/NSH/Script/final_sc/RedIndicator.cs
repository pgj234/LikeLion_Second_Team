using UnityEngine;

public class RedIndicator : MonoBehaviour
{
    public float lifeTime = 1.0f;

    void Start()
    {
        Destroy(gameObject, lifeTime); // 1초 후 자동 제거
    }

    void Update()
    {
        
    }
}
