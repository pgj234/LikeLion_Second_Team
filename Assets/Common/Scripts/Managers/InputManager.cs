using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InputManager : MonoBehaviour
{
    public static InputManager instance;

    Player player;

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
    public bool ParryPressed { get; set; }
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

    void Start()
    {
        player = FindObjectOfType<Player>();
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
            ParryPressed = Input.GetKey(KeyCode.H);
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
            if (player != null)
            {
                player.Damaged(1); // 1 데미지
            }
        }

        // 숫자키 2 입력 처리 - 카메라 쉐이크 테스트
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {

        }

        // F1~F5 키를 누르면 해당 씬으로 이동 (Team9 씬 일때는 로딩과 함께 지역 이동)
        if (Input.GetKeyDown(KeyCode.F1))
        {
            if (0 == string.Compare(SceneManager.GetActiveScene().name, "Team9"))
            {
                FinalSceneLoad();
            }
            else
            {
                SceneManager.LoadScene("PGJ");
            }
        }
        else if (Input.GetKeyDown(KeyCode.F2))
        {
            if (0 == string.Compare(SceneManager.GetActiveScene().name, "Team9"))
            {
                SceneMaster.instance.cineCam.GetComponent<CinemachineConfiner2D>().enabled = false;
                player.transform.localPosition = new Vector2(118, 31.5f);
                Invoke("CinemachineON", 1);
            }
            else
            {
                SceneManager.LoadScene("JIH");
            }
        }
        else if (Input.GetKeyDown(KeyCode.F3))
        {
            if (0 == string.Compare(SceneManager.GetActiveScene().name, "Team9"))
            {
                SceneMaster.instance.cineCam.GetComponent<CinemachineConfiner2D>().enabled = false;
                player.transform.localPosition = new Vector2(227, 39.2f);
                Invoke("CinemachineON", 1);
            }
            else
            {
                SceneManager.LoadScene("KYW");
            }
        }
        else if (Input.GetKeyDown(KeyCode.F4))
        {
            if (0 == string.Compare(SceneManager.GetActiveScene().name, "Team9"))
            {
                SceneMaster.instance.cineCam.GetComponent<CinemachineConfiner2D>().enabled = false;
                player.transform.localPosition = new Vector2(329, 42.2f);
                Invoke("CinemachineON", 1);
            }
            else
            {
                SceneManager.LoadScene("NSH");
            }
        }
        else if (Input.GetKeyDown(KeyCode.F5))
        {
            if (0 == string.Compare(SceneManager.GetActiveScene().name, "Team9"))
            {
                SceneMaster.instance.cineCam.GetComponent<CinemachineConfiner2D>().enabled = false;
                player.transform.localPosition = new Vector2(552, 94);
                Invoke("CinemachineON", 1);
            }
            else
            {
                SceneManager.LoadScene("YSME");
            }
        }
        else if (Input.GetKeyDown(KeyCode.F12))
        {
            FinalSceneLoad();
        }
    }

    void CinemachineON()
    {
        SceneMaster.instance.cineCam.GetComponent<CinemachineConfiner2D>().enabled = true;
        SceneMaster.instance.cineCam.gameObject.SetActive(false);
        SceneMaster.instance.cineCam.gameObject.SetActive(true);
    }

    void FinalSceneLoad()
    {
        SceneManager.LoadScene("Team9");
        SceneManager.LoadScene("Final_2 1111", LoadSceneMode.Additive);
        SceneManager.LoadScene("JIH_Final 1111", LoadSceneMode.Additive);
        SceneManager.LoadScene("KYW_FinalLava1111", LoadSceneMode.Additive);
        SceneManager.LoadScene("PGJ_Final 1111", LoadSceneMode.Additive);
        SceneManager.LoadScene("YSME_Final 1111", LoadSceneMode.Additive);
    }

    [ContextMenu("인풋멈추기")]
    public void InputStop()
    {
        inputEnabled = false;
        InputInit();
    }
    [ContextMenu("인풋시작")]
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
        ParryPressed = false;
        return;
    }
    public bool IsInputEnabled()
    {
        return inputEnabled;
    }

    public void SetXInput(float x)
    {
        xInput = x;
    }
    public void SetYInput(float y)
    {
        yInput = y;
    }
    public void SetFInput(bool input)
    {
        fInput = input;
    }
    public void SetFReleaseInput(bool input)
    {
        fInputReleased = input;
    }
}
