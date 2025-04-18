using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager instance;

    internal float xInput { get; private set; }
    internal float yInput { get; private set; }

    public bool jumpPressed { get; private set; }    
    public bool jumpHeld { get; private set; }      
    public bool jumpReleased { get; private set; }  

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
        if (!inputEnabled)
        {
            xInput = 0f;
            yInput = 0f;

            jumpPressed = false;
            jumpHeld = false;
            jumpReleased = false;

            leftClick = false;
            rightClick = false;
            return;
        }//인풋안받을때는 초기화

        xInput = Input.GetAxisRaw("Horizontal");
        yInput = Input.GetAxisRaw("Vertical");

        jumpPressed = Input.GetButtonDown("Jump");    // 한 번만 true
        jumpHeld = Input.GetButton("Jump");           // 누르고 있는 동안 true
        jumpReleased = Input.GetButtonUp("Jump");     // 떼는 순간 true

        //좌우클릭 했는지
        leftClick = Input.GetMouseButtonDown(0);
        rightClick = Input.GetMouseButtonDown(1);
    }

    public void InputStop()
    {
        inputEnabled = false;
    }

    public void InputStart()
    {
        inputEnabled = true;
    }

    public bool IsInputEnabled()
    {
        return inputEnabled;
    }
}
