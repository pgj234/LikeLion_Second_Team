using UnityEngine;

public class Switch_ActiveObj : Switcher
{
    [Header("스위치 작동시 활성화/비활성화 될 Obj 목록")]
    [SerializeField] private GameObject[] activeObj;
    [SerializeField] private GameObject[] deactiveObj;

    protected override void SwitchOn()
    {
        foreach (GameObject obj in activeObj)
        {
            obj.SetActive(true);
        }
        foreach (GameObject obj in deactiveObj)
        {
            obj.SetActive(false);
        }
    }

    protected override void OnTriggerEnter2D(Collider2D col)
    {
        base.OnTriggerEnter2D(col);

        if (col.CompareTag("Player"))
        {
            switchOn = true;
            SwitchOn();
        }
    }
}
