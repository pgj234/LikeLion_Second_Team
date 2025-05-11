using UnityEngine;

public class VerticalMove : MonoBehaviour
{    public float speed = 5f; // �Ʒ��� �̵� �ӵ�
    public float lifetime = 5f; // �������� �ð� (5��)

    void Start()
    {
        // 5�� �� ������Ʈ ����
        Destroy(gameObject, lifetime);
        SoundManager.instance.PlaySFX(SFX_JIH.MeteorSound);
    }

    void Update()
    {
        // ������ �Ʒ��� �̵� (y�� ����)
        transform.Translate(Vector2.down * speed * Time.deltaTime);
    }
}
