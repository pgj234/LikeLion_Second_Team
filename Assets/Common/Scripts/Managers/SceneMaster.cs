using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMaster : MonoBehaviour
{
    public static SceneMaster instance = null;
    internal CinemachineCamera cineCam;

    void Awake()
    {
        if (null == instance)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        cineCam = GameObject.Find("CinemachineCamera")?.GetComponent<CinemachineCamera>();
    }

    internal void SceneLoad(string _sceneName)
    {
        SceneManager.LoadScene(_sceneName);

        if (0 == string.Compare(_sceneName, "PGJ"))
        {
            cineCam = GameObject.Find("CinemachineCamera").GetComponent<CinemachineCamera>();
        }
    }

    internal Scene CurrentSceneGet()
    {
        return SceneManager.GetActiveScene();
    }
}