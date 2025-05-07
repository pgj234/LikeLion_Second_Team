using UnityEngine;

public class PrefabSpawner : MonoBehaviour
{

    public GameObject prefab; // 생성할 프리팹
    public GameObject objectToActivate;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // 플레이어 태그 확인
        {
            objectToActivate.SetActive(true);
            
        }
    }
}
