using System.Collections.Generic;
using UnityEngine;

public class TrailColliderSpawner2D : MonoBehaviour
{
    [SerializeField] GameObject colliderPrefab;
    [SerializeField] float spacing;           // 콜라이더 간격
    [SerializeField] float lifetime;

    Vector2 lastSpawnPos;

    float angle;

    void Start()
    {
        EventManager.instance.OnPlayerRespawned += Destroy;

        lastSpawnPos = transform.position;
    }

    internal void Destroy()
    {
        Destroy(gameObject);
    }

    void OnDestroy()
    {
        EventManager.instance.OnPlayerRespawned -= Destroy;
    }

    void Update()
    {
        Vector2 currentPos = transform.position;
        if (Vector2.Distance(currentPos, lastSpawnPos) >= spacing)
        {
            SpawnCollider(currentPos);

            lastSpawnPos = currentPos;
        }
    }

    void SpawnCollider(Vector2 pos)
    {
        Vector2 direction = pos - lastSpawnPos;
        angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.Euler(0, 0, angle);

        GameObject col = Instantiate(colliderPrefab, pos, rotation);
        col.GetComponent<MagmaTrailCollider>().StartProc(lifetime);
    }
}