using UnityEngine;
using UnityEngine.Playables;

public class TimelineTrigger : MonoBehaviour
{
 
    public PlayableDirector timeline; // 타임라인의 Playable Director

    // 충돌 감지 (Trigger 사용)
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // 플레이어 태그 확인
        {
            timeline.Play(); // 타임라인 재생
        }
    }

    // 물리적 충돌 감지 (선택적)
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            timeline.Play();
        }
    }
}
