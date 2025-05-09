using UnityEngine;

public class AutoDestroy : MonoBehaviour
{
    public float lifetime = 4f; // 몇 초 뒤에 삭제할지 설정

    void Start()
    {
        Destroy(gameObject, lifetime);
    }
}
