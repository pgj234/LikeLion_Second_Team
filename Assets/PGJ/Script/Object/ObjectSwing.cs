using UnityEngine;

public class ObjectSwing : MonoBehaviour
{
    [SerializeField] float angle;
    [SerializeField] float speed;

    float swingTime;

    void Start()
    {
        EventManager.instance.OnPlayerRespawned += InitPos;
        InitPos();
    }

    void Update()
    {
        swingTime += Time.deltaTime;

        transform.rotation = Quaternion.Euler(0, 0, Mathf.Sin(swingTime * speed) * angle);
    }

    void OnDestroy()
    {
        EventManager.instance.OnPlayerRespawned -= InitPos;
    }

    void InitPos()
    {
        transform.rotation = Quaternion.identity;
        swingTime = 0;
    }
}
