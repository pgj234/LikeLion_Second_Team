using System.Collections;
using UnityEngine;

public class Switch_MoveObj : Switcher
{
    [SerializeField] Vector2 moveTargetLocalPos;

    protected override void SwitchOn()
    {
        base.SwitchOn();

        StartCoroutine(MoveProc());
    }

    protected override void OnTriggerEnter2D(Collider2D col)
    {
        base.OnTriggerEnter2D(col);

        if (col.gameObject.CompareTag("PlayerGhost"))
        {
            switchOn = true;
            
            SwitchOn();
        }
    }

    IEnumerator MoveProc()
    {
        while (true)
        {
            effectObj.transform.Translate(moveTargetLocalPos * workSpd * Time.deltaTime);
            
            yield return null;
        }
    }
}