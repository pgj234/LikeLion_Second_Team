using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMaster : MonoBehaviour
{
    public static SceneMaster instance = null;

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
    }

    internal void SceneLoad(string _sceneName)
    {
        SceneManager.LoadScene(_sceneName);
    }

    internal Scene CurrentSceneGet()
    {
        return SceneManager.GetActiveScene();
    }
}
