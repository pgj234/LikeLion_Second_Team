using UnityEngine;

public class ProjectileMove : MonoBehaviour
{
    [SerializeField] float lifeTime = 15;

    float speed;

    internal void Init(Vector2 _upDir, float _speed)
    {
        transform.up = -_upDir;
        speed = _speed;
    }

    void Update()
    {
        transform.Translate(-transform.up * speed * Time.deltaTime, Space.World);

        lifeTime -= Time.deltaTime;

        if (lifeTime < 0)
        {
            Destroy(gameObject);
        }
    }
}
