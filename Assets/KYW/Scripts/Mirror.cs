using UnityEngine;

public class Mirror : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        // 특정 태그 가진 물체에 반응
        if (other.CompareTag("Sword"))
        {
            Debug.Log("칼이 닿았어요!");
            Rotate30();
        }
    }
    void Rotate30()
    {
        // 원하는 행동
        Debug.Log("패링");
    }
}
