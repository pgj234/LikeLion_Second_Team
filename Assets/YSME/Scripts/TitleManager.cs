using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleManager : MonoBehaviour
{
    public static TitleManager instance { get; private set; }

    [Header("참조")]
    [SerializeField] private Button startButton;
    [SerializeField] private Button creditButton;
    [SerializeField] private Button optionButton;
    [SerializeField] private Button exitButton;

    [Space]
    [SerializeField] private FadeInOut fadeInOut;

    [Space, Header("이동할 씬 이름")]
    [SerializeField] private string startSceneName;
    [SerializeField] private string creditSceneName;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    void Start()
    {
        startButton.onClick.AddListener(() => SceneStart(startSceneName));
        creditButton.onClick.AddListener(() => SceneStart(creditSceneName));
        optionButton.onClick.AddListener(OpenOption);
        exitButton.onClick.AddListener(ExitGame);
    }

    void SceneStart(string sceneName)
    {
        StartCoroutine(SceneStartCoroutine(sceneName));
    }
    IEnumerator SceneStartCoroutine(string sceneName)
    {
        yield return StartCoroutine(fadeInOut.StartFadeOut());
        SceneManager.LoadScene(sceneName);
    }

    void ExitGame()
    {
        StartCoroutine(ExitGameCoroutine());
    }
    IEnumerator ExitGameCoroutine()
    {
        yield return StartCoroutine(fadeInOut.StartFadeOut());
        Application.Quit();
    }

    void OpenOption()
    {
        if (OptionManager.instance != null)
        {
            OptionManager.instance.ActivePausePanel();
        }
    }
}
