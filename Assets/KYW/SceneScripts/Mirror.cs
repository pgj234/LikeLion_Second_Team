using UnityEngine;
using DG.Tweening;

public class Mirror : MonoBehaviour
{
    private bool isRotating = false;
    public float rotateDuration = 0.3f;
    [SerializeField] private float rotationAngle = 45f;  // 회전 각도

    private void OnTriggerEnter2D(Collider2D other)
    {
        // 회전 중이면 무시
        if (isRotating) return;

        if (other.CompareTag("Sword"))
        {
            Debug.Log("칼이 닿았어요!");
            Rotate();
        }
    }

    void Rotate()
    {
        isRotating = true;

        transform
            .DORotate(new Vector3(0f, 0f, transform.eulerAngles.z + rotationAngle), rotateDuration)
            .SetEase(Ease.OutBack)
            .OnComplete(() =>
            {
                isRotating = false; // 다시 충돌 가능!
            });
    }
}
