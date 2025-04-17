using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager instance;

    internal float xInput { get; private set; }
    internal float yInput { get; private set; }
    internal float jump { get; private set; }

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

    void Update()
    {
        xInput = Input.GetAxis("Horizontal");
        yInput = Input.GetAxis("Vertical");
        jump = Input.GetAxis("Jump");
    }
}
