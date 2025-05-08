using UnityEngine;

public class CollisionSoundOnce : MonoBehaviour
{
    [SerializeField] private SFX_KYW soundToPlay; // 재생할 사운드

    private bool hasPlayed = false; // 사운드가 재생되었는지 체크

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!hasPlayed && collision.CompareTag("Player"))
        {
            SoundManager.instance.PlaySFX(soundToPlay);
            hasPlayed = true;
        }
    }

    // 필요시 사운드 재생 상태를 리셋하는 함수
    public void ResetSoundState()
    {
        hasPlayed = false;
    }
} 