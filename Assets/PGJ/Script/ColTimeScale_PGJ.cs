using UnityEngine;

public class ColTimeScale_PGJ : MonoBehaviour
{
    [SerializeField]
    float timeScale;

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            Time.timeScale = timeScale;
        }
    }
}
