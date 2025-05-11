using UnityEngine;

public class CollisionSoundOnce : MonoBehaviour
{
    public enum SoundType
    {
        Normal,         // 기본 재생
        WithVolume,     // 볼륨 조절
        Random,         // 랜덤 볼륨/피치
        AtPoint         // 위치 기반
    }

    [SerializeField] private SoundType soundType = SoundType.Normal;
    
    // 기본 사운드 설정
    [SerializeField] private SFX_KYW soundToPlay;
    
    // 볼륨 설정
    [SerializeField] private float volume = 1f;
    
    // 랜덤 설정
    [SerializeField] private float minVolume = 0.8f;
    [SerializeField] private float maxVolume = 1.2f;
    [SerializeField] private float minPitch = 0.9f;
    [SerializeField] private float maxPitch = 1.1f;

    private bool hasPlayed = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!hasPlayed && collision.CompareTag("Player"))
        {
            PlaySound();
            hasPlayed = true;
        }
    }

    private void PlaySound()
    {
        switch (soundType)
        {
            case SoundType.Normal:
                SoundManager.instance.PlaySFX(soundToPlay);
                break;
                
            case SoundType.WithVolume:
                SoundManager.instance.PlaySFX(soundToPlay, volume);
                break;
                
            case SoundType.Random:
                SoundManager.instance.PlaySFX(soundToPlay, minVolume, maxVolume, minPitch, maxPitch);
                break;
                
            case SoundType.AtPoint:
                SoundManager.instance.PlaySFXAtPoint(soundToPlay, transform.position, volume);
                break;
        }
    }

    // 필요시 사운드 재생 상태를 리셋하는 함수
    public void ResetSoundState()
    {
        hasPlayed = false;
    }
} 