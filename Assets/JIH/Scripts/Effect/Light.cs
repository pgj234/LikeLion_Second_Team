using UnityEngine;

public class Light : LightCollisionState
{
    public GameObject particlePrefab; // 인스펙터에서 할당할 파티클 프리팹
    private GameObject particleInstance; // 생성된 파티클 인스턴스
    private bool isLightOn = false;

    public override void Start()
    {
        base.Start();
    }

    public override void Update()
    {
        base.Update();

        Light lightComponent = GetComponent<Light>();
        if (lightComponent != null && lightComponent.enabled && !isLightOn)
        {
            isLightOn = true;
            SpawnParticle();
        }
        else if (lightComponent != null && !lightComponent.enabled && isLightOn)
        {
            isLightOn = false;
            StopParticle();
        }
    }

    public override void OnCollisionEnter(Collision collision)
    {
        base.OnCollisionEnter(collision);
    }

    private void SpawnParticle()
    {
        if (particlePrefab != null && particleInstance == null)
        {
            // 파티클 프리팹을 현재 오브젝트 위치에 생성
            particleInstance = Instantiate(particlePrefab, transform.position, transform.rotation);
            ParticleSystem ps = particleInstance.GetComponent<ParticleSystem>();
            if (ps != null)
            {
                ps.Play(); // 파티클 재생
            }
        }
        else if (particleInstance != null)
        {
            ParticleSystem ps = particleInstance.GetComponent<ParticleSystem>();
            if (ps != null)
            {
                ps.Play(); // 이미 존재하면 재생
            }
        }
        else
        {
            Debug.LogWarning("파티클 프리팹이 할당되지 않았습니다!");
        }
    }

    private void StopParticle()
    {
        if (particleInstance != null)
        {
            ParticleSystem ps = particleInstance.GetComponent<ParticleSystem>();
            if (ps != null)
            {
                ps.Stop(); // 파티클 멈춤
                // 필요하면 파티클 오브젝트를 삭제하려면 아래 주석 해제
                // Destroy(particleInstance, ps.main.duration);
            }
        }
    }
}
