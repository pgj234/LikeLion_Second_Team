using UnityEngine;

public class PlayerSkill : MonoBehaviour
{
    [Header("스킬 쿨타임")]
    [SerializeField] protected float coolTime;
    protected float coolTimeTimer;

    protected Player player;

    protected virtual void Awake()
    {
        player = GetComponent<Player>();
    }

    protected virtual void Update()
    {
        coolTimeTimer -= Time.deltaTime;
    }

    internal virtual bool CanUseSkill()
    {
        if (0 > coolTimeTimer)
        {
            // 스킬사용
            coolTimeTimer = coolTime;
            return true;
        }

        return false;
    }

    internal virtual void UseSkill()
    {
        // 스킬사용
    }
}