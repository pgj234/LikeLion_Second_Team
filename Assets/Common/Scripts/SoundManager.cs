using UnityEngine;

public enum BGM
{
    Main,
    Stage_1,
    Stage_2,
    Stage_3,
    Stage_4,
    Stage_5,
    Stage_6,
    None
}

public enum SFX
{
    EnemyDie
}

public class SoundManager : MonoBehaviour
{
    static public SoundManager instance = null;

    AudioSource audioSource;

    [SerializeField] AudioClip[] bgmClipArray;

    [Space(20)]
    [SerializeField] AudioClip[] sfxClipArray;

    void Awake()
    {
        if (null == instance)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        audioSource = GetComponent<AudioSource>();
    }

    public void PlayBGM(BGM bgm)
    {
        audioSource.clip = bgmClipArray[(int)bgm];
        audioSource.Play();
    }

    public void StopBGM()
    {
        audioSource.Stop();
    }

    public void PlaySFX(SFX sfx)
    {
        audioSource.PlayOneShot(sfxClipArray[(int)sfx]);
    }
}
