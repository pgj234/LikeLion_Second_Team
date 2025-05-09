using UnityEngine;

public class ParticleSpawner : MonoBehaviour
{

    public ParticleSystem particlePrefab; // 2D 파티클 시스템 프리팹
    public Transform spawnPosition; // 생성 위치 (2D)
    public float spawnInterval = 5f; // 5초 간격

    void Start()
    {
        InvokeRepeating("SpawnParticle", 0f, spawnInterval);
    }

    void SpawnParticle()
    {
        // 2D 위치에 파티클 생성
        Instantiate(particlePrefab, spawnPosition.position, Quaternion.identity);
    }
}
