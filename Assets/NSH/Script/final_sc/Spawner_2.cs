using UnityEngine;
using System.Collections;

public class Spawner_2 : MonoBehaviour
{
    public GameObject objectToSpawn;
    public GameObject redIndicatorPrefab;
    public float spawnInterval = 4.0f;

    public Transform spawnPoint;
    public float groundY = -3.5f;

    public float delayBeforeWarning = 1f;  // 경고가 나타나기 전 딜레이
    public float warningDuration = 1f;     // 경고 유지 시간

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
        // 1. 경고 등장 전 잠시 대기
        yield return new WaitForSeconds(delayBeforeWarning);

        // 2. 경고 표시
        Vector2 groundPos = new Vector2(spawnPos.x, groundY);
        GameObject indicator = Instantiate(redIndicatorPrefab, groundPos, Quaternion.identity);

        // 3. 경고 유지 시간 후 제거
        yield return new WaitForSeconds(warningDuration);
        Destroy(indicator);

        // 4. 장애물 생성
        Instantiate(objectToSpawn, spawnPos, Quaternion.identity);
    }
}
