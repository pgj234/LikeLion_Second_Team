using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static EventManager instance = null;

    public PlayerEvents playerEvents;
    public EnemyEvents EnemyEvents;

    void Awake()
    {
        if (null == instance)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);

        playerEvents = new PlayerEvents();
        EnemyEvents = new EnemyEvents();
    }
}
