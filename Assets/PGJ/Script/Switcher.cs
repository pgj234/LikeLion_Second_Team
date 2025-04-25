using System.Collections;
using UnityEngine;

public class Switcher : MonoBehaviour
{
    [SerializeField] protected GameObject triggerObj;
    [SerializeField] protected GameObject effectObj;
    [SerializeField] protected float workSpd;

    protected bool switchOn = false;

    protected virtual void SwitchOn()
    {

    }

    protected virtual void OnTriggerEnter2D(Collider2D col)
    {
        if (true == switchOn)
        {
            return;
        }
    }
}