using UnityEngine;

public class Next : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Player collided with trigger, loading KYW scene");
            SceneMaster.instance.SceneLoad("KYW");
        }
    }
}
