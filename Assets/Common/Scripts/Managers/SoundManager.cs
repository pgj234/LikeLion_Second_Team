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
}
