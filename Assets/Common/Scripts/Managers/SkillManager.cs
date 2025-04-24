using UnityEngine;

public class SkillManager : MonoBehaviour
{
    public static SkillManager instance;

    [Header("��ų ���")]
    [field: SerializeField] public DashSkill DashSkill { get; private set; }

    void Awake()
    {
        if (null != instance)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }

        DontDestroyOnLoad(gameObject);
    }
}