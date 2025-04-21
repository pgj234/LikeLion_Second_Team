using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager instance;

    internal float xInput { get; private set; }
    public bool jumpPressed { get; private set; }    
    public bool jumpHold { get; private set; }      
    public bool jumpReleased { get; private set; }  

    public bool DashPressed { get; private set; }

    internal bool leftClick { get; private set; }
    internal bool rightClick { get; private set; }

    private bool inputEnabled = true;

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
        if (inputEnabled)
        {
            xInput = Input.GetAxisRaw("Horizontal");

            jumpPressed = Input.GetButtonDown("Jump");    // 한 번만 true
            jumpHold = Input.GetButton("Jump");           // 누르고 있는 동안 true
            jumpReleased = Input.GetButtonUp("Jump");     // 떼는 순간 true

            DashPressed = Input.GetKeyDown(KeyCode.LeftShift);

            //좌우클릭 했는지
            leftClick = Input.GetMouseButtonDown(0);
            rightClick = Input.GetMouseButtonDown(1);
        }
    }

    public void InputStop()
    {
        inputEnabled = false;
    }

    public void InputStart()
    {
        inputEnabled = true;
    }

    public void InputInit()
    {
        xInput = 0f;

        jumpPressed = false;
        jumpHold = false;
        jumpReleased = false;

        DashPressed = false;

        leftClick = false;
        rightClick = false;
        return;
    }
    public bool IsInputEnabled()
    {
        return inputEnabled;
    }
}
