using UnityEngine;

public class ObjectRotate : MonoBehaviour
{
    [SerializeField] bool isClockDir;
    [SerializeField] float speed;

    Quaternion originalRotation;

    void Start()
    {
        originalRotation = transform.rotation;

        EventManager.instance.OnPlayerRespawned += InitPos;
        InitPos();
    }

    void Update()
    {
        if (true == isClockDir)     // 시계 방향
        {
            transform.Rotate(0, 0, -speed * Time.deltaTime);
        }
        else        // 반시계 방향
        {
            transform.Rotate(0, 0, speed * Time.deltaTime);
        }
    }

    void OnDestroy()
    {
        EventManager.instance.OnPlayerRespawned -= InitPos;
    }

    void InitPos()
    {
        transform.rotation = originalRotation;
    }
}
