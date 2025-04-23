using UnityEngine;
using UnityEngine.SceneManagement;

public class InputManager : MonoBehaviour
{
    public static InputManager instance;

    public float xInput { get; private set; }
    public float yInput { get; private set; }
    public bool jumpInput { get; private set; }
    public bool dashInput { get; private set; }
    public bool qInput { get; private set; }
    public bool eInput { get; private set; }
    public bool fInput { get; private set; }
    public bool fInputReleased { get; private set; }
    public bool rInput { get; private set; }

    public bool jumpPressed { get; private set; }    
    public bool jumpHold { get; private set; }      
    public bool jumpReleased { get; private set; }  

    public bool DashPressed { get; private set; }

    public bool upHold { get; private set; }  // 위 버튼을 누르고 있는 상태

    internal bool leftClick { get; private set; }
    internal bool rightClick { get; private set; }

    private bool inputEnabled = true;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (inputEnabled)
        {
            xInput = Input.GetAxisRaw("Horizontal");
            yInput = Input.GetAxisRaw("Vertical");
            jumpInput = Input.GetKeyDown(KeyCode.Space);
            dashInput = Input.GetKeyDown(KeyCode.LeftShift);
            qInput = Input.GetKeyDown(KeyCode.Q);
            eInput = Input.GetKeyDown(KeyCode.E);
            fInput = Input.GetKeyDown(KeyCode.F);
            fInputReleased = Input.GetKeyUp(KeyCode.F);
            rInput = Input.GetKeyDown(KeyCode.R);

            jumpPressed = Input.GetButtonDown("Jump");    // 한 번만 true
            jumpHold = Input.GetButton("Jump");           // 누르고 있는 동안 true
            jumpReleased = Input.GetButtonUp("Jump");     // 떼는 순간 true

            DashPressed = Input.GetKeyDown(KeyCode.LeftShift);

            upHold = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow);  // 위 버튼을 누르고 있는 상태

            //좌우클릭 했는지
            leftClick = Input.GetMouseButtonDown(0);
            rightClick = Input.GetMouseButtonDown(1);

            Test();
        }
    }

    private void Test()
    {
        // 숫자키 1 입력 처리
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            // Player의 Damaged 함수 호출
            Player player = FindObjectOfType<Player>();
            if (player != null)
            {
                player.Damaged(1); // 1 데미지
            }
        }

        // 숫자키 2 입력 처리 - 카메라 쉐이크 테스트
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {

        }

        // F1~F5 키를 누르면 해당 씬으로 이동
        if (Input.GetKeyDown(KeyCode.F1))
        {
            SceneManager.LoadScene("JIH");
        }
        else if (Input.GetKeyDown(KeyCode.F2))
        {
            SceneManager.LoadScene("YSME");
        }
        else if (Input.GetKeyDown(KeyCode.F3))
        {
            SceneManager.LoadScene("PGJ");
        }
        else if (Input.GetKeyDown(KeyCode.F4))
        {
            SceneManager.LoadScene("KYW");
        }
        else if (Input.GetKeyDown(KeyCode.F5))
        {
            SceneManager.LoadScene("NSH");
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
        yInput = 0f;
        jumpPressed = false;
        jumpHold = false;
        jumpReleased = false;
        jumpInput = false;
        dashInput = false;
        DashPressed = false;
        leftClick = false;
        rightClick = false;
        qInput = false;
        eInput = false;
        fInput = false;
        rInput = false;
        upHold = false;
        return;
    }
    public bool IsInputEnabled()
    {
        return inputEnabled;
    }
}
