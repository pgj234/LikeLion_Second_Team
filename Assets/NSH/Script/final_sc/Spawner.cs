using UnityEngine;
using System.Collections;

public class Spawner2D : MonoBehaviour
{
    public GameObject objectToSpawn;
    public GameObject redIndicatorPrefab;
    public float spawnInterval = 2.0f;

    public Vector2[] spawnPositions; // 여러 위치 배열

    void Start()
    {
        StartCoroutine(SpawnLoop());
    }

    IEnumerator SpawnLoop()
    {
        while (true)
        {
            // 랜덤 위치 하나 선택
            Vector2 selectedPosition = spawnPositions[Random.Range(0, spawnPositions.Length)];
            StartCoroutine(SpawnWithIndicator(selectedPosition));
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    IEnumerator SpawnWithIndicator(Vector2 spawnPos)
    {
        Vector2 groundPos = new Vector2(spawnPos.x, -6.5f); // 박스는 아래쪽에 표시
        Instantiate(redIndicatorPrefab, groundPos, Quaternion.identity);

        yield return new WaitForSeconds(1f); // 1초 뒤 오브젝트 생성

        Instantiate(objectToSpawn, spawnPos, Quaternion.identity);
    }
}
