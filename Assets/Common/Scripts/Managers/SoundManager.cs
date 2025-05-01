using UnityEngine;

public class SoundManager : MonoBehaviour
{
    static public SoundManager instance = null;

    SoundData_YSME soundData_YSME;
    SoundData_JIH soundData_JIH;
    SoundData_KYW soundData_KYW;
    SoundData_NSH soundData_NSH;
    SoundData_PGJ soundData_PGJ;

    AudioSource audioSource;

    void Awake()
    {
        if (null == instance)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            audioSource = GetComponent<AudioSource>();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayBGM(BGM_PGJ bgm)
    {
        audioSource.clip = soundData_PGJ.bgmClip[(int)bgm];
        audioSource.Play();
    }

    public void PlayBGM(BGM_YSME bgm)
    {
        audioSource.clip = soundData_YSME.bgmClip[(int)bgm];
        audioSource.Play();
    }

    public void PlayBGM(BGM_NSH bgm)
    {
        audioSource.clip = soundData_NSH.bgmClip[(int)bgm];
        audioSource.Play();
    }

    public void PlayBGM(BGM_KYW bgm)
    {
        audioSource.clip = soundData_KYW.bgmClip[(int)bgm];
        audioSource.Play();
    }

    public void PlayBGM(BGM_JIH bgm)
    {
        audioSource.clip = soundData_JIH.bgmClip[(int)bgm];
        audioSource.Play();
    }



    public void StopBGM()
    {
        audioSource.Stop();
    }



    public void PlaySFX(SFX_PGJ sfx)
    {
        audioSource.PlayOneShot(soundData_PGJ.sfxClip[(int)sfx]);
    }

    public void PlaySFX(SFX_YSME sfx)
    {
        audioSource.PlayOneShot(soundData_YSME.sfxClip[(int)sfx]);
    }

    public void PlaySFX(SFX_NSH sfx)
    {
        audioSource.PlayOneShot(soundData_NSH.sfxClip[(int)sfx]);
    }

    public void PlaySFX(SFX_KYW sfx)
    {
        audioSource.PlayOneShot(soundData_KYW.sfxClip[(int)sfx]);
    }

    public void PlaySFX(SFX_JIH sfx)
    {
        audioSource.PlayOneShot(soundData_JIH.sfxClip[(int)sfx]);
    }
}
