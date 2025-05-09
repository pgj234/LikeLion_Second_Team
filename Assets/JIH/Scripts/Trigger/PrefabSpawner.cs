using UnityEngine;

public class PrefabSpawner : MonoBehaviour
{

    public GameObject prefab; // 생성할 프리팹
    public GameObject objectToActivate; // 활성화할 오브젝트 (선택적)
    private GameObject spawnedPrefab; // 생성된 프리팹 참조

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (objectToActivate != null)
                objectToActivate.SetActive(true);

            // 프리팹 생성 (원하는 위치와 회전)
            if (prefab != null)
                spawnedPrefab = Instantiate(prefab, transform.position, Quaternion.identity);
        }
    }

    // 생성된 프리팹을 비활성화하는 함수 (필요 시 호출)
    public void DeactivateSpawnedPrefab()
    {
        if (spawnedPrefab != null)
            spawnedPrefab.SetActive(false);
    }
}
