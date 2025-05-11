using UnityEngine;

public class SoundManager : MonoBehaviour
{
    static public SoundManager instance = null;

    [SerializeField] SoundData_YSME soundData_YSME;
    [SerializeField] SoundData_JIH soundData_JIH;
    [SerializeField] SoundData_KYW soundData_KYW;
    [SerializeField] SoundData_NSH soundData_NSH;
    [SerializeField] SoundData_PGJ soundData_PGJ;

    AudioSource bgmAudioSource;
    AudioSource sfxAudioSource;

    void Awake()
    {
        if (null == instance)
        {
            instance = this;

            bgmAudioSource = transform.Find("BGM").GetComponent<AudioSource>();
            sfxAudioSource = transform.Find("SFX").GetComponent<AudioSource>();

            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayBGM(BGM_PGJ bgm)
    {
        bgmAudioSource.clip = soundData_PGJ.bgmClip[(int)bgm];
        bgmAudioSource.Play();
    }

    public void PlayBGM(BGM_YSME bgm)
    {
        bgmAudioSource.clip = soundData_YSME.bgmClip[(int)bgm];
        bgmAudioSource.Play();
    }

    public void PlayBGM(BGM_NSH bgm)
    {
        bgmAudioSource.clip = soundData_NSH.bgmClip[(int)bgm];
        bgmAudioSource.Play();
    }

    public void PlayBGM(BGM_KYW bgm)
    {
        bgmAudioSource.clip = soundData_KYW.bgmClip[(int)bgm];
        bgmAudioSource.Play();
    }

    public void PlayBGM(BGM_JIH bgm)
    {
        bgmAudioSource.clip = soundData_JIH.bgmClip[(int)bgm];
        bgmAudioSource.Play();
    }



    public void StopBGM()
    {
        bgmAudioSource.Stop();
    }



    public void PlaySFX(SFX_PGJ sfx)
    {
        sfxAudioSource.PlayOneShot(soundData_PGJ.sfxClip[(int)sfx]);
    }

    public void PlaySFX(SFX_YSME sfx)
    {
        sfxAudioSource.PlayOneShot(soundData_YSME.sfxClip[(int)sfx]);
    }

    public void PlaySFX(SFX_NSH sfx)
    {
        sfxAudioSource.PlayOneShot(soundData_NSH.sfxClip[(int)sfx]);
    }

    public void PlaySFX(SFX_KYW sfx)
    {
        sfxAudioSource.PlayOneShot(soundData_KYW.sfxClip[(int)sfx]);
    }

    public void PlaySFX(SFX_JIH sfx)
    {
        sfxAudioSource.PlayOneShot(soundData_JIH.sfxClip[(int)sfx]);
    }

    //볼륨조절추가
    public void PlaySFX(SFX_KYW sfx, float volume)
    {
        sfxAudioSource.PlayOneShot(soundData_KYW.sfxClip[(int)sfx], volume);
    }
    //랜덤 볼륨 / 랜덤 피치 PlaySFX
    // 기본 값은 1.0이고, 이 값을 변경하면 재생 속도가 변하면서 소리의 높낮이도 바뀜.
    // Pitch 값	설명
    // 1.0	원본 소리
    // 0.5	낮고 느리게 (50% 느려짐)
    // 2.0	높고 빠르게 (2배 빠름)
    // 0.0	소리가 거의 안 들림
    // -1.0	반대로 재생됨 (역재생)
    public void PlaySFX(SFX_KYW sfx, float minVolume, float maxVolume, float minPitch, float maxPitch)
    {
        sfxAudioSource.pitch = Random.Range(minPitch, maxPitch);
        float volume = Random.Range(minVolume, maxVolume);
        sfxAudioSource.PlayOneShot(soundData_KYW.sfxClip[(int)sfx], volume);
    }
    //위치 기반 PlaySFX (3D 사운드)
    public void PlaySFXAtPoint(SFX_KYW sfx, Vector3 position, float volume = 1.0f)
    {
        AudioSource.PlayClipAtPoint(soundData_KYW.sfxClip[(int)sfx], position, volume);
    }
}
