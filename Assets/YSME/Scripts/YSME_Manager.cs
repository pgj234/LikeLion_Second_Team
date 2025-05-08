using UnityEngine;

public class YSME_Manager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SoundManager.instance.PlayBGM(BGM_YSME.BGM1);
    }

}
