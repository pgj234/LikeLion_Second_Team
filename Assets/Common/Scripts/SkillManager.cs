using UnityEngine;

public class SkillManager : MonoBehaviour
{
    public static SkillManager instance;

    //internal OutofFluid_Skill outofFluid_Skill { get; private set; }

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

        //outofFluid_Skill = GetComponent<OutofFluid_Skill>();
    }
}
