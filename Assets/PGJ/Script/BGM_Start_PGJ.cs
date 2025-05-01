using UnityEngine;

public class BGM_Start_PGJ : MonoBehaviour
{
    void Start()
    {
        SoundManager.instance.StopBGM();
        SoundManager.instance.PlayBGM(BGM_PGJ.Bgm);
    }
}
