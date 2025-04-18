using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager instance;

    internal float xInput { get; private set; }
    internal float yInput { get; private set; }
    internal float jump { get; private set; }

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
            jump = 0f;
            leftClick = false;
            rightClick = false;
            return;
        }//인풋안받을때는 초기화

        xInput = Input.GetAxis("Horizontal");
        yInput = Input.GetAxis("Vertical");
        jump = Input.GetAxisRaw("Jump");

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
