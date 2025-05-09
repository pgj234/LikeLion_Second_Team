using UnityEngine;

public class PrefabSpawnOff : MonoBehaviour
{

    public PrefabSpawner spawner; // PrefabSpawner 스크립트 참조

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // PrefabSpawner의 생성된 프리팹 비활성화
            if (spawner != null)
                spawner.DeactivateSpawnedPrefab();
        }
    }
}
