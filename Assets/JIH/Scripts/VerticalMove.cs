using UnityEngine;

public class VerticalMove : MonoBehaviour
{

    public float speed = 5f; // 아래로 이동 속도
    public float lifetime = 5f; // 삭제까지 시간 (5초)

    void Start()
    {
        // 5초 후 오브젝트 삭제
        Destroy(gameObject, lifetime);
        SoundManager.instance.PlaySFX(SFX_JIH.MeteorSound);
    }

    void Update()
    {
        // 위에서 아래로 이동 (y축 감소)
        transform.Translate(Vector2.down * speed * Time.deltaTime);
    }
}
