using UnityEngine;

public class MagmaShotSwitch : MonoBehaviour
{
    [Header("마그마 발사대 ON 할 것들")]
    [SerializeField] MagmaShot[] onMagmaShotArray;

    [Space(10)]
    [Header("마그마 발사대 OFF 할 것들")]
    [SerializeField] MagmaShot[] offMagmaShotArray;

    bool switchOn = false;

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            if (false == switchOn)
            {
                switchOn = true;

                SwitchProc();
            }
        }
    }

    void SwitchProc()
    {
        for (int i=0; i<onMagmaShotArray.Length; i++)
        {
            onMagmaShotArray[i].isOn = true;
        }

        for (int i = 0; i<offMagmaShotArray.Length; i++)
        {
            offMagmaShotArray[i].isOn = false;
        }
    }
}
