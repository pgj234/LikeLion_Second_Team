using UnityEngine;

public class SwordEffect : MonoBehaviour
{
    public float lifeTime = 0.1f;

    void Start()
    {
        Destroy(gameObject, lifeTime);
    }
}
