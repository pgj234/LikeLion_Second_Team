using UnityEngine;

public class ObejctSwitch : MonoBehaviour
{
    [Header("ON 할 오브젝트들")]
    [SerializeField] GameObject[] onObjectArray;

    [Space(10)]
    [Header("OFF 할 오브젝트들")]
    [SerializeField] GameObject[] offObjectArray;

    bool switchOn = false;

    void Start()
    {
        EventManager.instance.OnPlayerRespawned += OnPlayerDied;
    }

    void OnDestroy()
    {
        EventManager.instance.OnPlayerRespawned -= OnPlayerDied;
    }

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
        for (int i = 0; i < onObjectArray.Length; i++)
        {
            onObjectArray[i].SetActive(true);
        }

        for (int i = 0; i < offObjectArray.Length; i++)
        {
            offObjectArray[i].SetActive(false);
        }
    }

    public void OnPlayerDied()
    {
        switchOn = false;
    }
}
