using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager instance;

    internal float xInput { get; private set; }
    internal float xInputRaw { get; private set; }
    internal float yInput { get; private set; }
    internal float jumpInput { get; private set; }

    internal bool dashInput { get; private set; }

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
        xInputRaw = Input.GetAxisRaw("Horizontal");
        yInput = Input.GetAxis("Vertical");
        jumpInput = Input.GetAxisRaw("Jump");

        dashInput = Input.GetKeyDown(KeyCode.LeftShift);
    }
}
