using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour
{
    public GameObject objectToSpawn;
    public GameObject redIndicatorPrefab;
    public float spawnInterval = 2.0f;

    public Transform spawnPoint;     // 장애물이 떨어질 고정 위치
    public float groundY = -3.5f;    // 바닥 위치

    void Start()
    {
        StartCoroutine(SpawnLoop());
    }

    IEnumerator SpawnLoop()
    {
        while (true)
        {
            Vector2 spawnPos = spawnPoint.position;
            StartCoroutine(SpawnWithIndicator(spawnPos));
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    IEnumerator SpawnWithIndicator(Vector2 spawnPos)
    {
        Vector2 groundPos = new Vector2(spawnPos.x, groundY);
        Instantiate(redIndicatorPrefab, groundPos, Quaternion.identity);

        yield return new WaitForSeconds(1f);

        Instantiate(objectToSpawn, spawnPos, Quaternion.identity);
    }
}
