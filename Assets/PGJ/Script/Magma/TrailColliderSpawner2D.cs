using System.Collections.Generic;
using UnityEngine;

public class TrailColliderSpawner2D : MonoBehaviour
{
    [SerializeField] GameObject colliderPrefab;
    [SerializeField] float spacing;           // 콜라이더 간격
    [SerializeField] float lifetime;

    //[SerializeField] Transform colliderObjBasketTr;

    Vector2 lastSpawnPos;

    float angle;

    void Start()
    {
        lastSpawnPos = transform.position;
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
        Destroy(col, lifetime);
    }
}