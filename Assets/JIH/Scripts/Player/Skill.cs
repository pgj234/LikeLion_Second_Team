using UnityEngine;

public class Skill : MonoBehaviour
{
    [SerializeField] protected float cooldown;
    protected float cooldownTimer;


    protected Player player;


    protected virtual void Start()
    {
        if (PlayerManager.instance == null)
        {
            Debug.LogError("PlayerManager.instance is null in Skill.Start");
            return;
        }
        player = PlayerManager.instance.player;
    }


    protected virtual void Update()
    {
        cooldownTimer -= Time.deltaTime;
    }


    public virtual bool CanUseSkill()
    {
        if (cooldownTimer < 0)
        {
            UseSkill();
            cooldownTimer = cooldown;
            return true;
        }


        Debug.Log("Skill is on cooldown");

        return false;
    }


    public virtual void UseSkill()
    {
        //스킬사용
    }



}
