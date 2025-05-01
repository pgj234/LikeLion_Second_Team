using System.Collections;
using UnityEngine;

public class Switch_MoveObj : Switcher
{
    [SerializeField] bool isGhost;
    [SerializeField] Vector2 moveTargetLocalPos;

    protected override void Start()
    {
        base.Start();
    }

    protected override void SwitchOn()
    {
        base.SwitchOn();

        // 필요하면 수행할 것 넣기
    }

    protected override void OnTriggerEnter2D(Collider2D col)
    {
        base.OnTriggerEnter2D(col);

        if (true == isGhost)
        {
            if (col.gameObject.CompareTag("PlayerGhost"))
            {
                SwitchOn();
                StartCoroutine(MoveProc());
            }
        }
        else
        {
            if (col.gameObject.CompareTag("Player"))
            {
                SwitchOn();
                StartCoroutine(MoveProc());
            }
        }
    }

    IEnumerator MoveProc()
    {
        //SoundManager.instance.PlaySFX(SFX.);

        while (true)
        {
            effectObj.transform.position = Vector2.MoveTowards(effectObj.transform.position, moveTargetLocalPos, workSpd * Time.deltaTime);
            
            yield return null;

            if (moveTargetLocalPos == (Vector2)effectObj.transform.position)
            {
                break;
            }
        }
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
    }

    protected override void InitPos()
    {
        base.InitPos();
    }
}