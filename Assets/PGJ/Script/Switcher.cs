using System.Collections;
using UnityEngine;

public class Switcher : MonoBehaviour
{
    [SerializeField] protected GameObject triggerObj;
    [SerializeField] protected GameObject effectObj;
    [SerializeField] protected float workSpd;

    Vector2 effectOriginalObjLocalPos;

    protected bool switchOn = false;

    protected virtual void Start()
    {
        effectOriginalObjLocalPos = effectObj.transform.localPosition;

        EventManager.instance.OnPlayerRespawned += InitPos;
        InitPos();
    }

    // 필요하면 수행할 것 넣기
    protected virtual void SwitchOn()
    {
        switchOn = true;
    }

    protected virtual void OnTriggerEnter2D(Collider2D col)
    {
        if (true == switchOn)
        {
            return;
        }
    }

    protected virtual void OnDestroy()
    {
        EventManager.instance.OnPlayerRespawned -= InitPos;
    }

    protected virtual void InitPos()
    {
        switchOn = false;
        effectObj.transform.localPosition = effectOriginalObjLocalPos;
    }
}