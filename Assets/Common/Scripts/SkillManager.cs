using UnityEngine;

public class SkillManager : MonoBehaviour
{
    public static SkillManager instance;

    //internal Dash_Skill dashSkill { get; private set; }

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
